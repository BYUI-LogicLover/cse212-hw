using code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue items with different priorities and dequeue to get highest priority item
    // Expected Result: Dequeue should return "C" (priority 5), then "B" (priority 3), then "A" (priority 1)
    // Defect(s) Found: Loop in Dequeue uses "index < _queue.Count - 1" which skips the last element
    public void TestPriorityQueue_DequeuePriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 5);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("C", result);
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with the same highest priority, dequeue should follow FIFO
    // Expected Result: Dequeue should return "X" first (same priority as Y, but added first)
    // Defect(s) Found: Comparison uses ">=" instead of ">" so later items with same priority replace earlier ones
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("X", 5);
        priorityQueue.Enqueue("Y", 5);
        priorityQueue.Enqueue("Z", 5);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("X", result, "When priorities are equal, should return item closest to front (FIFO)");
    }

    [TestMethod]
    // Scenario: Dequeue from an empty queue
    // Expected Result: Should throw InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None - empty queue handling works correctly
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format("Unexpected exception of type {0} caught: {1}",
                    e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Dequeue multiple times to verify items are actually removed from the queue
    // Expected Result: Each dequeue should return the current highest priority item and remove it
    // Defect(s) Found: Dequeue never removes the item from the queue (missing RemoveAt call)
    public void TestPriorityQueue_MultipleDequeue()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 3);
        priorityQueue.Enqueue("High", 5);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Test that the last item in the queue can be dequeued when it has highest priority
    // Expected Result: Should return "Last" since it has the highest priority (10)
    // Defect(s) Found: Loop doesn't check the last element due to off-by-one error in loop condition
    public void TestPriorityQueue_LastItemHighestPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 1);
        priorityQueue.Enqueue("Middle", 2);
        priorityQueue.Enqueue("Last", 10);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Last", result, "Should return the last item when it has highest priority");
    }

    [TestMethod]
    // Scenario: Test single item in queue
    // Expected Result: Should dequeue the only item successfully
    // Defect(s) Found: None - single item case works (loop doesn't run, returns index 0)
    public void TestPriorityQueue_SingleItem()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Only", 1);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Only", result);
    }
}
