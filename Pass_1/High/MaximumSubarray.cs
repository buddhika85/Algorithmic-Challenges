using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High;

public class MaximumSubarray
{
    private readonly bool _debug = false; // Set to true to print internal states

    public MaximumSubarray()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Maximum Subarray (Kadane's Algorithm)
     * ------------------------------------------------------------
     *  Given an integer array nums, find the contiguous subarray
     *  (containing at least one number) which has the largest sum
     *  and return its sum.
     *
     *  Example:
     *      Input:
     *          nums = [-2,1,-3,4,-1,2,1,-5,4]
     *
     *      Output:
     *          6
     *
     *      Explanation:
     *          The subarray [4,-1,2,1] has the largest sum = 6.
     *
     * ------------------------------------------------------------
     *  Expected Time: 5–10 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, int[] nums, int expected)[]
        {
            (1, "Example case", new[] { -2,1,-3,4,-1,2,1,-5,4 }, 6),
            (2, "All positives", new[] { 1, 2, 3, 4 }, 10),
            (3, "All negatives", new[] { -3, -2, -5, -1 }, -1),
            (4, "Single element", new[] { 5 }, 5),
            (5, "Mixed values", new[] { 1, -2, 3, 5, -1 }, 8),
            (6, "Simple", new [] {-2, 1, -3}, +1)
        };

        foreach (var (num, desc, nums, expected) in testHarness)
        {
            Test(num, desc, nums, expected);
        }
    }

    private void Test(int num, string desc, int[] nums, int expected)
    {
        var sw = Stopwatch.StartNew();
        int actual = Solve(nums);
        sw.Stop();

        bool pass = actual == expected;

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | Result={actual} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | Expected={expected}, Actual={actual}");
        }
    }

    // ------------------------------------------------------------
    // Solve method: implement logic here
    //  0  1   2  3   4  5  6   7  8
    // -2, 1, -3, 4, -1, 2, 1, -5, 4      -> 6
    //  windowStart = 0
    //  windowEnd = 0
    //  maxTotal = -2

    //  while(windowEnd < arraysLength)

    /*
    if currTotal < maxTotal && windowStart < windowEnd
        shrink
    else
        expand

    windowEnd       windowStart         currentTotal            maxTotal
    0               0                   -2                      -2          --> expand
    1               0                   -2+1 = -1               -1          --> expand
    2               0                   -1+-3 = -4              -1          --> shrink
    2               1                   -4+2 = -2               -1          --> shrink
    2               2                   -2-1 = -3               -1          --> expand --> because we cannot shrink any more to a valid window, as window start will pass window end if so
    3               2                   -3-4 = +1               +1          --> expand
    4               2                   +1-1 = 0                +1          --> shrink
    4               3                   0+3 = +3                +3          --> expand
    5               3                   +3+2 = +5               +5          --> expand
    6               3                   +5+1 = +6               +6          --> expand
    7               3                   +6-5 = +5               +6          --> shrink
    7               4                   +1-4 = -3               +6          --> shrink
    7               5                   -3+1 = -2               +6          --> shrink
    7               6                   -2-2 = -4               +6          --> shrink
    7               7                   -4-1 = -5               +6          --> expand --> because we cannot shrink any more to a valid window, as window start will pass window end if so
    8               7                   -5+4 = -1               +6          --> shrink
    8               8                   -1+5 = +4               +6          --> expand --> because we cannot shrink any more to a valid window, as window start will pass window end if so
    9--> terminate
    */



    // Best case Time Complexity: 
    // Average case Time Complexity: 
    // Worst case Time Complexity: 
    // Space Complexity: 
    // ------------------------------------------------------------
    private int Solve(int[] nums)
    {
        /*
“If continuing the sum is worse than restarting, restart.”
     0  1   2   
    -2, 1, -3

    element         currentMax                           MaxSofar
    -2              -2                                      -2
    1               1 or 1+-2 = 1 or -1 = 1                 -1
    -3              -3 or -1+-3 = -3 or -4 = -3             -3         

 0  1   2  3   4  5  6   7  8
-2, 1, -3, 4, -1, 2, 1, -5, 4      -> 6         [4,-1,2,1]

element             currMax                             MaxSoFar
-2                  -2                                  -2
1                   -2 + 1 or 1 = 1                      1
-3                  1 + -3 or -3 = -2                    1
4                   -2 + 4 or 4 = 4                      4
-1                  4 + -1 or -1 = 3                     4
2                   3 + 2 or 2 = 5                       5
1                   5 + 1 or 1 = 6                       6
-5                  6 + -5 or -5 = -1                    6
4                   -1 + 4 = 3                           6                        

        */

        var maxToHere = nums[0];
        var maxSoFar = maxToHere;

        for (int i = 1; i < nums.Length; i++)
        {
            maxToHere = Math.Max(maxToHere + nums[i], nums[i]);
            maxSoFar = Math.Max(maxSoFar, maxToHere);
        }
        return maxSoFar;
    }

    // Optional brute-force approach
    private int Solve2(int[] nums)
    {
        // TODO: implement alternative O(n^2) solution
        return 0;
    }
}
