using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue multiple items with different priorities, then dequeue.
    //           The item with the highest priority should be returned first.
    // Expected Result: Dequeue returns "C" (priority 3), then "B" (priority 2), then "A" (priority 1)
    // Defect(s) Found: 1) Loop skips the last element (uses Count-1 instead of Count).
    //                  2) Item is never actually removed from the queue after dequeue.
    public void TestPriorityQueue_DequeueHighestPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 3);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("C", result, "Should dequeue the item with highest priority (3)");
    }

    [TestMethod]
    // Scenario: Enqueue multiple items where some have the same highest priority.
    //           The item closest to the front of the queue should be dequeued first.
    // Expected Result: Dequeue returns "A" first (it's closer to the front with priority 3),
    //                  then "C" (also priority 3 but added later), then "B" (priority 1)
    // Defect(s) Found: Uses >= comparison which selects later items instead of earlier ones (FIFO violation).
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 3);
        priorityQueue.Enqueue("B", 1);
        priorityQueue.Enqueue("C", 3);

        var first = priorityQueue.Dequeue();
        Assert.AreEqual("A", first, "Should dequeue first item with highest priority (FIFO)");

        var second = priorityQueue.Dequeue();
        Assert.AreEqual("C", second, "Should dequeue second item with same priority");

        var third = priorityQueue.Dequeue();
        Assert.AreEqual("B", third, "Should dequeue remaining item");
    }

    [TestMethod]
    // Scenario: Dequeue from an empty queue.
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None - this functionality works correctly.
    public void TestPriorityQueue_EmptyQueueThrowsException()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Expected InvalidOperationException was not thrown.");
        }
        catch (InvalidOperationException ex)
        {
            Assert.AreEqual("The queue is empty.", ex.Message);
        }
    }

    [TestMethod]
    // Scenario: Enqueue a single item and dequeue it. Then verify the queue is empty.
    // Expected Result: Dequeue returns the single item, subsequent dequeue throws exception.
    // Defect(s) Found: Item not removed from queue - second dequeue doesn't throw exception.
    public void TestPriorityQueue_SingleItemEnqueueDequeue()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("OnlyItem", 5);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("OnlyItem", result);

        // Queue should now be empty
        Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Verify the last item in the queue can be dequeued when it has the highest priority.
    // Expected Result: The last item "Z" with priority 10 should be dequeued.
    // Defect(s) Found: Loop boundary bug - last element is never checked (Count-1 issue).
    public void TestPriorityQueue_LastItemHighestPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("Z", 10);  // Last item has highest priority

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Z", result, "Should dequeue last item when it has highest priority");
    }
}