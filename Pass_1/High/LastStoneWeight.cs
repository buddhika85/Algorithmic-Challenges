using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace AlgorithmsPractice.Pass_1.High;

public class LastStoneWeight
{
    private readonly bool _debug = false; // Set to true to print internal states

    public LastStoneWeight()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Last Stone Weight
     * ------------------------------------------------------------
     *  You are given an array of integers stones where stones[i]
     *  is the weight of the i-th stone.
     *
     *  We repeatedly smash the two heaviest stones together:
     *    - If x == y, both stones are destroyed.
     *    - If x != y, the stone with weight x and y is replaced
     *      by a stone of weight |x - y|.
     *
     *  Continue this process until there is at most one stone left.
     *  Return the weight of the last remaining stone, or 0 if none.
     *
     *  Example:
     *      Input:
     *          stones = [2,7,4,1,8,1]
     *
     *      Output:
     *          1
     *
     *      Explanation:
     *          8 and 7 -> 1
     *          4 and 2 -> 2
     *          2 and 1 -> 1
     *          1 and 1 -> 0
     *          No stones left -> 0
     *
     * ------------------------------------------------------------
     *  Expected Time: 5–10 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, int[] stones, int expected)[]
        {
            (1, "Example case", new[] { 2, 7, 4, 1, 8, 1 }, 1),
            (2, "Single stone", new[] { 5 }, 5),
            (3, "All equal", new[] { 3, 3, 3, 3 }, 0),
            (4, "Two stones", new[] { 10, 4 }, 6),
            (5, "Already zero result", new[] { 1, 1 }, 0)
        };

        foreach (var (num, desc, stones, expected) in testHarness)
        {
            Test(num, desc, stones, expected);
        }
    }

    private void Test(int num, string desc, int[] stones, int expected)
    {
        var sw = Stopwatch.StartNew();
        int actual = Solve(stones);
        sw.Stop();

        bool pass = actual == expected;

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | Expected={expected}, Actual={actual}");
        }
    }

    // ------------------------------------------------------------
    // Solve method: implement logic here
    // use C# min-heap with negative priority - which makes it a max - you always get largest of heap
    // first loop round - we populate max heap
    // second loop round, until max heap count is 1 - we try deque 2 times, which gives us 2 of largest 
    //      we calc difference of them - 1st - 2nd
    //      add the diff back to max heep
    // return last remaining number of max heap
    // Time Complexity:  O(n log n)   (heap operations for n stones)
    // Space Complexity: O(n)         (heap stores up to n stones)
    // ------------------------------------------------------------
    private int Solve(int[] stones)
    {
        var maxHeap = new PriorityQueue<int, int>();        // value --> priority 
        foreach (var stone in stones)
        {
            maxHeap.Enqueue(stone, priority: -stone);
        }

        while (maxHeap.Count > 1)
        {
            var largest = maxHeap.Dequeue();
            var secondLargest = maxHeap.Dequeue();
            var smashResult = largest - secondLargest;
            if (smashResult > 0)
                maxHeap.Enqueue(smashResult, priority: -smashResult);
        }
        return maxHeap.Count == 1 ? maxHeap.Dequeue() : 0;
    }

    // Optional alternative: sort each round (less efficient)
    private int Solve2(int[] stones)
    {
        // TODO: implement alternative solution using sorting in a loop
        return 0;
    }
}
