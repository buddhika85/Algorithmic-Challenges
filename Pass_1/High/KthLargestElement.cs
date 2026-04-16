using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace AlgorithmsPractice.Pass_1.High;

public class KthLargestElement
{
    private readonly bool _debug = false; // Set to true to print internal states

    public KthLargestElement()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Kth Largest Element in an Array
     * ------------------------------------------------------------
     *  Given an integer array nums and an integer k, return the
     *  kth largest element in the array.
     *
     *  Note:
     *      - The kth largest element is the kth element in the
     *        sorted order, not the kth distinct element.
     *      - You must solve it in O(n log k) using a Min-Heap.
     *
     *  Example:
     *      Input:
     *          nums = [3,2,1,5,6,4]
     *          k = 2
     *
     *      Output:
     *          5
     *
     *      Explanation:
     *          The sorted array is [1,2,3,4,5,6]
     *          The 2nd largest element is 5.
     *
     * ------------------------------------------------------------
     *  Expected Time: 5–10 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, int[] nums, int k, int expected)[]
        {
            (1, "Example case", new[] { 3, 2, 1, 5, 6, 4 }, 2, 5),
            (2, "Single element", new[] { 10 }, 1, 10),
            (3, "Already sorted", new[] { 1, 2, 3, 4, 5 }, 1, 5),
            (4, "Reverse sorted", new[] { 9, 7, 5, 3, 1 }, 3, 5),
            (5, "Duplicates", new[] { 3, 3, 3, 3 }, 2, 3)
        };

        foreach (var (num, desc, nums, k, expected) in testHarness)
        {
            Test(num, desc, nums, k, expected);
        }
    }

    private void Test(int num, string desc, int[] nums, int k, int expected)
    {
        var sw = Stopwatch.StartNew();
        int actual = Solve(nums, k);
        sw.Stop();

        bool pass = actual == expected;

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | k={k} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | k={k} | Expected={expected}, Actual={actual}");
        }
    }

    // ------------------------------------------------------------
    // Solve method: implement logic here
    // we will use C# Priority Queue (which is Min Heap and gives least prioritized element when you deque.) 
    // and we will not allow it to grow more than k.
    // at the end we will deque kth largest. 
    // Best case Time Complexity: O(n log k)      --> since its binary tree  
    // Average case Time Complexity: O(n log k)      --> since its binary tree  
    // Worst case Time Complexity: O(n log k)      --> since its binary tree    
    // Space Complexity: O(k + 1)                   --> we allow binary to add max k + 1 items            
    // ------------------------------------------------------------
    private int Solve(int[] nums, int k)
    {
        var minHeap = new PriorityQueue<int, int>();        // value --> priority
        foreach (var item in nums)
        {
            minHeap.Enqueue(item, priority: item);
            if (minHeap.Count > k)      // avoiding growing to k+1
            {
                minHeap.Dequeue();      // remove smallest
            }
        }
        return minHeap.Dequeue();
    }

    // Optional brute-force approach (sorting)
    private int Solve2(int[] nums, int k)
    {
        // TODO: implement alternative solution using sorting
        return -1;
    }
}
