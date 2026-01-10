using System.Collections.Generic;

public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // Step 1: Create an array of doubles with the size of 'length'
        double[] multiples = new double[length];

        // Step 2: Use a loop to iterate from 0 to length - 1
        for (int i = 0; i < length; i++)
        {
            // Step 3: In each iteration, calculate the multiple by multiplying 'number' with (i + 1)
            multiples[i] = number * (i + 1);
        }

        // Step 4: After the loop, return the populated array
        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // Step 1: Calculate the effective rotation amount
        int n = data.Count - amount;

        // Step 2: Extract the last 'amount' elements
        List<int> tail = data.GetRange(n, amount);

        // Step 3: Remove the last 'amount' elements from the original list
        List<int> head = data.GetRange(0, n);

        // Step 4: Clear the original list and add the tail followed by the head
        data.Clear();

        // Step 5: Add the extracted tail elements to the front
        data.AddRange(tail);

        // Step 6: Add the remaining head elements to the end
        data.AddRange(head);
    }
}