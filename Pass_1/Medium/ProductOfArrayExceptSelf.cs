using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.Medium
{
    public class ProductOfArrayExceptSelf
    {
        private readonly bool _debug = true;

        public ProductOfArrayExceptSelf()
        {
            RunAllTests();
        }

        /*
         * ------------------------------------------------------------
         *  Problem: Product of Array Except Self
         * ------------------------------------------------------------
         *  Given an integer array nums, return an array answer such that
         *  answer[i] is the product of all elements of nums except nums[i].
         *
         *  Constraints:
         *      - No division allowed
         *      - Must run in O(n) time
         *      - Must use O(1) extra space (output array doesn't count)
         *
         *  Example:
         *      Input:  [1,2,3,4]
         *      Output: [24,12,8,6]
         *
         * ------------------------------------------------------------
         *  Expected Time: 10–15 minutes
         * ------------------------------------------------------------
         */

        private void RunAllTests()
        {
            var tests = new (int num, string desc, int[] nums, int[] expected)[]
            {
                (
                    1,
                    "Example case",
                    new int[] { 1, 2, 3, 4 },
                    new int[] { 24, 12, 8, 6 }
                ),
                // (
                //     2,
                //     "Contains zero",
                //     new int[] { -1, 1, 0, -3, 3 },
                //     new int[] { 0, 0, 9, 0, 0 }
                // ),
                // (
                //     3,
                //     "Single element",
                //     new int[] { 5 },
                //     new int[] { 1 }
                // ),
                // (
                //     4,
                //     "Two elements",
                //     new int[] { 2, 3 },
                //     new int[] { 3, 2 }
                // )
            };

            foreach (var (num, desc, nums, expected) in tests)
            {
                Test(num, desc, nums, expected);
            }
        }

        private void Test(int num, string desc, int[] nums, int[] expected)
        {
            var sw = Stopwatch.StartNew();
            var actual = Solve2(nums);
            sw.Stop();

            bool pass = ArraysEqual(actual, expected);

            if (pass)
            {
                Console.WriteLine($"[PASS] {num}. {desc} | Time={sw.ElapsedTicks} ticks");
            }
            else
            {
                Console.WriteLine($"[FAIL] {num}. {desc}");
                Console.WriteLine($"       Input:    [{string.Join(",", nums)}]");
                Console.WriteLine($"       Expected: [{string.Join(",", expected)}]");
                Console.WriteLine($"       Actual:   [{string.Join(",", actual)}]");
            }
        }

        private bool ArraysEqual(int[] a, int[] b)
        {
            if (a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }

            return true;
        }

        // ------------------------------------------------------------
        // Solve method: implement logic here
        //  *  Example:
        //  *       Input:  [1, 2, 3, 4]
        //  *       Output: [24, 12, 8, 6]
        // we need to build left product array, and right product array, and then we need multiply each of them - 3 loops
        // example - 
        // left products index zero is 1, right products last index is zero 
        // leftProduct = [1,,,]         rightProduct = [,,,1]

        // left to right loop
        // leftProduct[i] = originalArray[i - 1] * leftProduct[i - 1]
        // leftProduct[1] = originalArray[0] * leftProduct[0] = 1 * 1 = 1 => [1,1,,]
        // leftProduct[2] = originalArray[1] * leftProduct[1] = 2 * 1 = 2 => [1,1,2,]
        // leftProduct[3] = originalArray[2] * leftProduct[2] = 3 * 2 = 6 => [1,1,2,6]

        // right to left loop
        // rightProduct[i] = originalArray[i + 1] * rightProduct[i + 1]
        // rightProduct[2] = originalArray[3] * rightProduct[3] = 4 * 1 = 4 => [,,4,1]
        // rightProduct[1] = originalArray[2] * rightProduct[2] = 3 * 4 = 12 => [,12,4,1]
        // rightProduct[0] = originalArray[1] * rightProduct[1] = 2 * 12 = 24 => [24,12,4,1]

        // multiply 
        // left product index * right product index = [1,1,2,6] * [24,12,4,1] = [24, 12, 8, 6]


        // Time complexity: O(n)  - 3 loops which runs n (array count) times = O(3n) ==> simplfied to O(n)
        // Space complexity: O(n) - we need 2 additional arrays of same size = O(2n) ==> simplfied to O(n)
        // ------------------------------------------------------------
        private int[] Solve(int[] nums)
        {
            if (nums is null || nums.Length == 0)
                return Array.Empty<int>();

            var leftProduct = new int[nums.Length];
            var rightProduct = new int[nums.Length];
            var leftRightProduct = new int[nums.Length];

            // set first & last elements to 1 in product arrays
            leftProduct[0] = 1;
            rightProduct[nums.Length - 1] = 1;

            // build left product
            for (var i = 1; i < nums.Length; i++)
            {
                leftProduct[i] = nums[i - 1] * leftProduct[i - 1];
            }

            // build right product
            for (var i = nums.Length - 2; i >= 0; i--)
            {
                rightProduct[i] = nums[i + 1] * rightProduct[i + 1];
            }

            // build product of left product & right product
            for (int i = 0; i < nums.Length; i++)
            {
                leftRightProduct[i] = leftProduct[i] * rightProduct[i];
            }

            return leftRightProduct;
        }

        // Optimizing space complexity to O(1)
        // leftProduct[i] = originalArray[i - 1] * leftProduct[i - 1]
        // leftProduct[1] = originalArray[0] * leftProduct[0] = 1 * 1 = 1 => [1,1,,]
        // leftProduct[2] = originalArray[1] * leftProduct[1] = 2 * 1 = 2 => [1,1,2,]
        // leftProduct[3] = originalArray[2] * leftProduct[2] = 3 * 2 = 6 => [1,1,2,6]

        // right to left loop
        // rightProduct[i] = originalArray[i + 1] * rightProduct[i + 1]
        // rightProduct[2] = originalArray[3] * rightProduct[3] = 4 * 1 = 4 => [,,4,1]
        // rightProduct[1] = originalArray[2] * rightProduct[2] = 3 * 4 = 12 => [,12,4,1]
        // rightProduct[0] = originalArray[1] * rightProduct[1] = 2 * 12 = 24 => [24,12,4,1]

        // multiply 
        //  *  Example:
        //  *       Input:  [1, 2, 3, 4]
        //  *       Output: [24, 12, 8, 6]

        // left product index * right product index = [1,1,2,6] * [24,12,4,1] = [24, 12, 8, 6]
        private int[] Solve2(int[] nums)
        {
            if (nums is null || nums.Length == 0)
                return Array.Empty<int>();

            var product = new int[nums.Length];


            // set first & last elements to 1 in product arrays
            product[0] = 1;

            // build left product
            for (var i = 1; i < nums.Length; i++)
            {
                product[i] = nums[i - 1] * product[i - 1];
            }

            if (_debug)
                Console.WriteLine($"{string.Join(',', product)}");

            // [1,1,2,6]
            // var rightRunningProduct = 1;
            // product[i] = product[i] * rightRunningProduct;
            // rightRunningProduct = rightRunningProduct * nums[i];
            // build right product
            var rightRunningProduct = 1;
            for (var i = nums.Length - 1; i >= 0; i--)
            {
                product[i] = product[i] * rightRunningProduct;
                rightRunningProduct = rightRunningProduct * nums[i];
            }

            return product;     // [24, 12, 8, 6]
        }
    }
}
