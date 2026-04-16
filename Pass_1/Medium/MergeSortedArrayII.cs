using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High;

public class MergeSortedArrayII
{
    private readonly bool _debug = false; // Set to true to print internal states

    public MergeSortedArrayII()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Merge Sorted Array
     * ------------------------------------------------------------
     *  You are given two integer arrays nums1 and nums2, sorted in
     *  non-decreasing order, and two integers m and n, representing
     *  the number of elements in nums1 and nums2 respectively.
     *
     *  nums1 has a length of m + n, where the last n elements are
     *  set to 0 and should be ignored. Merge nums2 into nums1 as
     *  one sorted array.
     *
     *  The final sorted array should be stored inside nums1.
     *
     *  Example:
     *      Input:
     *          nums1 = [1,2,3,0,0,0], m = 3
     *          nums2 = [2,5,6],     n = 3
     *
     *      Output:
     *          nums1 = [1,2,2,3,5,6]
     *
     * ------------------------------------------------------------
     *  Expected Time: 10–15 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, int[] nums1, int m, int[] nums2, int n, int[] expected)[]
        {
            (1, "Example case", new[] { 1,2,3,0,0,0 }, 3, new[] { 2,5,6 }, 3, new[] { 1,2,2,3,5,6 }),
            (2, "All nums2 smaller", new[] { 4,5,6,0,0,0 }, 3, new[] { 1,2,3 }, 3, new[] { 1,2,3,4,5,6 }),
            (3, "All nums2 larger", new[] { 1,2,3,0,0,0 }, 3, new[] { 4,5,6 }, 3, new[] { 1,2,3,4,5,6 }),
            (4, "nums1 empty", new[] { 0,0,0 }, 0, new[] { 2,5,6 }, 3, new[] { 2,5,6 }),
            (5, "nums2 empty", new[] { 1,2,3 }, 3, Array.Empty<int>(), 0, new[] { 1,2,3 }),
            (6, "Interleaving", new[] { 2,4,6,0,0,0 }, 3, new[] { 1,3,5 }, 3, new[] { 1,2,3,4,5,6 })
        };

        foreach (var (num, desc, nums1, m, nums2, n, expected) in testHarness)
        {
            Test(num, desc, nums1, m, nums2, n, expected);
        }
    }

    private void Test(int num, string desc, int[] nums1, int m, int[] nums2, int n, int[] expected)
    {
        var sw = Stopwatch.StartNew();
        Solve(nums1, m, nums2, n);
        sw.Stop();

        bool pass = nums1.Length == expected.Length &&
                    nums1.Zip(expected, (a, b) => a == b).All(x => x);

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | Expected=[{string.Join(",", expected)}], Actual=[{string.Join(",", nums1)}]");
        }
    }

    /*
    [2,4,6,0,0,0 ]   m = 3    [ 1,3,5 ]    n = 3         result = [ 1,2,3,4,5,6 ]

    i = m - 1 = 3 - 1 = 2                -- this is num1s last valid index
    j = n - 1 = 3 - 1 = 2                -- this is num2s biggest value index, the last 1 as of now
    k = num1 Length - 1 = 6 -1 = 5       -- this num1s biggest slots index we can fill as of now

      i         j         k       num1s i < num2s j           result
    --2       --2       --5       6 < 5                       [2,4,6,0,0,6]
      1       --2       --4       4 < 5                       [2,4,6,0,5,6]
    --1         1       --3       4 < 3                       [2,4,6,4,5,6]
      0       --1       --2       2 < 3                       [2,4,3,4,5,6]
    --0         0       --1       2 < 1                       [2,2,3,4,5,6]
     -1         0       --0       ran out of num 1            [1,2,3,4,5,6]
    */
    // ------------------------------------------------------------
    // Solve method: implement logic here
    // we will use 3 pointers
    // num1sLastValid
    // num2sLastValid
    // nextBiggestOfNum1
    // we awlays move from right to left, larger to smaller
    // [2,4,6,0,0,0 ]   m = 3    [ 1,3,5 ]    n = 3         result = [ 1,2,3,4,5,6 ]
    // i = m - 1 = 3 - 1 = 2                -- this is num1s last valid index
    // j = n - 1 = 3 - 1 = 2                -- this is num2s biggest value index, the last 1 as of now
    // k = num1 Length - 1 = 6 -1 = 5       -- this num1s biggest slots index we can fill as of now
    // while j >= 0                         -- while we check all elements of num 1
    //      if i > 0 && num1s i < num2s j
    //          num1 k = num2 j     put num2s j element in num1s k slot
    //          --j         // check left element in the next round
    //          --k         // fill next biggest in next bigger slot next round
    //      else if i > 0
    //          num1 k = num1 i     put num1s i element in num1s k slot
    //          --i         // check left element in the next round
    //          --k         // fill next biggest in next bigger slot next round
    //      else
    //          -- this means we have run out of elements in num1
    //          num1 k = num2 j         // so just put num2s j element in num1s k slot
    //          --j         // check left element in the next round
    //          --k         // fill next biggest in next bigger slot next round 
    // summary
    /*
    We always place the largest remaining element at the end.
    We never overwrite nums1’s valid region.
    We only write to nums1[k].
    */
    // Best case Time Complexity: O(m + n)      
    // Average case Time Complexity: O(m + n) 
    // Worst case Time Complexity: O(m + n) 
    // Space Complexity: O(1)       
    // 3:51
    // ------------------------------------------------------------
    private void Solve(int[] nums1, int m, int[] nums2, int n)
    {
        var i = m - 1;      // num1s last valid
        var j = n - 1;      // num2s last valid
        var k = nums1.Length - 1;       // biggest slot we can fill now

        while (j >= 0)           // we have add all elements of num2 in proper places of num1
        {
            if (i >= 0 && nums1[i] < nums2[j])
            {
                // num 2 element is bigger
                nums1[k] = nums2[j];
                --j;        // we considerd j, consider immediate left in next iteration
            }
            else if (i >= 0)
            {
                // num1 element is bigger is larger
                nums1[k] = nums1[i];
                --i;        // we considerd i, consider immediate left in next iteration
            }
            else
            {
                // we have run out elements to compare in num1
                nums1[k] = nums2[j];
                --j;        // we considerd j, consider immediate left in next iteration
            }

            --k;        // try filling next biggest in next iteration
        }
    }

    // Optional brute-force approach
    private void Solve2(int[] nums1, int m, int[] nums2, int n)
    {
        // TODO: implement alternative solution
    }
}
