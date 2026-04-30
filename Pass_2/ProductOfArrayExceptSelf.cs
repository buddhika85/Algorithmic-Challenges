using System.Diagnostics;

namespace AlgorithmsPractice.Pass_2;

public class ProductOfArrayExceptSelf
{
    private readonly bool _debug = false; // Set to true to print internal states

    public ProductOfArrayExceptSelf()
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
     *  Expected Time: 10–15 minutes
     * ------------------------------------------------------------
     */
    /*

    [(2 * 3 * 4), (1 * 3 * 4), (1 * 2 * 4), (1 * 2 * 3)]
    [24, 12, 8, 6]

    so we can create left prodcut array and right product array (can do this inline of left product, to achive O(1) space, but thats next step)

    left = [1, 1, 2, 6]             - 0th is = 1, then, go from left to right input[i - 1] * left[i - 1]
    right = [24 ,12 , 4, 1]         - length - 1 = 1, then, go from right to left input[i + 1] * right[i + 1]

    product = [24, 12, 8, 6]

    if we use inline approach - without using a right

    int rightProdSoFar = 1
    left = [1, 1, 2, 6]             - 0th is = 1, then, go from left to right input[i - 1] * left[i - 1]
    prod = [(1 * 12 * 2), (1 * 4 * 3), (2 * 1 * 4), (6 * 1 * 1)]       --> we go from right to left, rightProductSoFar = rightProductSoFar * input[i + 1], then left[i] = rightProductSoFar * left[i]
         = [24, 12, 8, 6]

    */
    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, int[] nums, int[] expected)[]
        {
            (1, "Example case", new[] { 1, 2, 3, 4 }, new[] { 24, 12, 8, 6 }),
            (2, "Contains zeros", new[] { 1, 0, 3, 4 }, new[] { 0, 12, 0, 0 }),
            (3, "All ones", new[] { 1, 1, 1, 1 }, new[] { 1, 1, 1, 1 }),
            (4, "Mixed values", new[] { -1, 1, 0, -3, 3 }, new[] { 0, 0, 9, 0, 0 }),
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
            Console.WriteLine($"[FAIL] {num}. {desc}");
            Console.WriteLine($"  Expected=[{string.Join(",", expected)}]");
            Console.WriteLine($"  Actual=[{string.Join(",", actual)}]");
        }
    }

    // ------------------------------------------------------------
    // Solve method: implement logic here
    // -- You have to fill this --
    // Best case Time Complexity: --
    // Time is O(n) because you always do two linear passes, never nested loops.
    // Space is O(1) because you reuse the output array and only keep a running right product.
    // ------------------------------------------------------------

    /*
  *  Example:
     *      Input:
     *          nums = [1, 2, 3, 4]
     *
     *      Output:
     *          [24, 12, 8, 6]
   [(2 * 3 * 4), (1 * 3 * 4), (1 * 2 * 4), (1 * 2 * 3)]
   [24, 12, 8, 6]

   so we can create left prodcut array and right product array (can do this inline of left product, to achive O(1) space, but thats next step)

   left = [1, 1, 2, 6]             - 0th is = 1, then, go from left to right input[i - 1] * left[i - 1]
   right = [24 ,12 , 4, 1]         - length - 1 = 1, then, go from right to left input[i + 1] * right[i + 1]

   product = [24, 12, 8, 6]

   if we use inline approach - without using a right

   int rightProdSoFar = 1
   left = [1, 1, 2, 6]             - 0th is = 1, then, go from left to right input[i - 1] * left[i - 1]
   prod = [(1 * 12 * 2), (1 * 4 * 3), (2 * 1 * 4), (6 * 1 * 1)]       --> we go from right to left, rightProductSoFar = rightProductSoFar * input[i + 1], then left[i] = rightProductSoFar * left[i]
        = [24, 12, 8, 6]

   */
    private int[] Solve(int[] nums)
    {
        var numOfItems = nums.Length;
        var result = new int[nums.Length];
        result[0] = 1;

        // calculating the left product
        for (var i = 1; i < numOfItems; i++)
        {
            result[i] = nums[i - 1] * result[i - 1];
        }

        var rightSoFar = 1;
        for (int i = numOfItems - 2; i >= 0; i--)
        {
            rightSoFar *= nums[i + 1];      // rightSoFar * nums[i + 1];
            result[i] *= rightSoFar;        // result[i] * rightSoFar;
        }
        return result;
    }

    // Optional brute-force approach
    private int[] Solve2(int[] nums)
    {
        // TODO: implement alternative solution
        return Array.Empty<int>();
    }
}
