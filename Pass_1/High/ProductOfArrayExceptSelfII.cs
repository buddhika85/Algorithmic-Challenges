using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High;

public class ProductOfArrayExceptSelfII
{
    private readonly bool _debug = false; // Set to true to print internal states

    public ProductOfArrayExceptSelfII()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Product of Array Except Self
     * ------------------------------------------------------------
     *  Given an integer array nums, return an array answer such that
     *  answer[i] is equal to the product of all the elements of nums
     *  except nums[i].
     *
     *  - You must write an algorithm that runs in O(n) time.
     *  - You must not use division.
     *
     *  Example:
     *      Input:
     *          nums = [1, 2, 3, 4]
     *
     *      Output:
     *          [24, 12, 8, 6]
     *
     *      Explanation:
     *          answer[0] = 2 * 3 * 4 = 24
     *          answer[1] = 1 * 3 * 4 = 12
     *          answer[2] = 1 * 2 * 4 = 8
     *          answer[3] = 1 * 2 * 3 = 6
     *
     * ------------------------------------------------------------
     *  Expected Time: 5–10 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, int[] nums, int[] expected)[]
        {
            (1, "Example case", new[] { 1, 2, 3, 4 }, new[] { 24, 12, 8, 6 }),
            (2, "Contains zero", new[] { 1, 2, 0, 4 }, new[] { 0, 0, 8, 0 }),
            (3, "All ones", new[] { 1, 1, 1, 1 }, new[] { 1, 1, 1, 1 }),
            (4, "Single element", new[] { 5 }, new[] { 1 }),
            (5, "Mixed positives", new[] { 2, 3, 4, 5 }, new[] { 60, 40, 30, 24 })
        };

        foreach (var (num, desc, nums, expected) in testHarness)
        {
            Test(num, desc, nums, expected);
        }
    }

    private void Test(int num, string desc, int[] nums, int[] expected)
    {
        var sw = Stopwatch.StartNew();
        int[] actual = Solve(nums);
        sw.Stop();

        bool pass = actual.Length == expected.Length &&
                    actual.Zip(expected, (a, b) => a == b).All(x => x);

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | Expected=[{string.Join(",", expected)}], Actual=[{string.Join(",", actual)}]");
        }
    }

    /*  Example:
         *      Input:
         *          nums = [1, 2, 3, 4]
         *
         *      Output:
         *          [24, 12, 8, 6]
         *
         *      Explanation:
         *          answer[0] = 2 * 3 * 4 = 24
         *          answer[1] = 1 * 3 * 4 = 12
         *          answer[2] = 1 * 2 * 4 = 8
         *          answer[3] = 1 * 2 * 3 = 6

         so, we are going to use 2 addtional arrays of same n size -

one will store left side product for each element,
other will store right side product for each element,

then we multiple those 2 arrays together and return

[1, 2, 3, 4]

leftProduct =  [1, (1 * 1), (1 * 1 * 2), (1 * 1 * 2 * 3)]  =   [1, 1, 2, 6]
rightProduct = [(1 * 4 * 3 * 2), (1 * 4 * 3), (1 * 4), 1] = [24,12,4,1]
leftProduct * rightProduct = [24,12,8,6]


This is O(n) --> we need I beleive 3 loops, all running n times (number of items in the array), no nsted loops, so O(n)

we will preform multiplication on same left array (leftProduct * rightProduct =) so this would lead to O(1)
[1, 2, 3, 4]

leftProduct =  [1, (1 * 1), (1 * 1 * 2), (1 * 1 * 2 * 3)]  =   [1, 1, 2, 6]
rightProduct = [(1 * 4 * 3 * 2), (1 * 4 * 3), (1 * 4), 1] = [24,12,4,1]

[1, 1, 2, 6]
[1 * (12rp * 2)RP, 1 * (4rp * 3)RP, 2 * (1rp * 4)RP, 6 * 1RP]

leftProduct * rightProduct = [24,12,8,6]

         */
    // ------------------------------------------------------------
    // Solve method: implement logic here
    // we use the output array as the left product array
    // This is allowed because the output array does not count as extra space.
    //
    // Best case Time Complexity: O(n) --> number of computations increase with size of the array
    // Average case Time Complexity:  O(n)
    // Worst case Time Complexity: O(n)
    // Space Complexity: O(1)       --> we do not create any array other than output array
    // ------------------------------------------------------------
    private int[] Solve(int[] nums)
    {
        var itemsCount = nums.Length;
        var outputArray = new int[itemsCount];

        for (var i = 0; i < itemsCount; i++)
        {
            if (i == 0)
                outputArray[i] = 1;
            else
                outputArray[i] = outputArray[i - 1] * nums[i - 1];
        }

        var runningProduct = 1;

        for (int i = itemsCount - 1; i >= 0; i--)
        {
            if (i == itemsCount - 1)
                outputArray[i] = outputArray[i] * runningProduct;
            else
            {
                runningProduct = runningProduct * nums[i + 1];
                outputArray[i] = outputArray[i] * runningProduct;
            }
        }

        return outputArray;
    }

    // Optional brute-force approach (O(n^2))
    private int[] Solve2(int[] nums)
    {
        // TODO: implement alternative solution
        return Array.Empty<int>();
    }
}
