namespace AlgorithmsPractice.SortingAndGreedy;

public class ProductOfArrayExceptSelf
{
    public ProductOfArrayExceptSelf()
    {
        var testData = new (int testCaseNum, int[] nums, int[] expected)[]
        {
            (
                1,
                new int[] { 1, 2, 3, 4 },
                new int[] { 24, 12, 8, 6 }
            ),
            (
                2,
                new int[] { -1, 1, 0, -3, 3 },
                new int[] { 0, 0, 9, 0, 0 }
            ),
            (
                3,
                new int[] { 5 },
                new int[] { 1 }
            )
        };

        foreach (var (testCaseNum, nums, expected) in testData)
        {
            var result = Solve(nums);
            var testCaseResult = AreEqual(result, expected) ? "Passed" : "FAILED";
            Console.WriteLine($"{testCaseNum}. {testCaseResult}");
        }
    }

    bool AreEqual(int[] result, int[] expected)
    {
        if (result.Length != expected.Length)
            return false;

        for (int i = 0; i < result.Length; i++)
        {
            if (result[i] != expected[i])
                return false;
        }

        return true;
    }

    /*
        Problem: Product of Array Except Self

        Description:
            Given an integer array nums, return an array answer such that
            answer[i] is the product of all elements of nums except nums[i].

        Time Complexity:
            O(n)


        new int[] { 1, 2, 3, 4 },
        new int[] { 24, 12, 8, 6 }

        
        left  product - go left to right = [1,1,2,6] 
        right product - go right to left = [24,12,4,1]
        product = [24, 12, 8, 6]        
    */
    public int[] Solve(int[] nums)
    {
        var count = nums.Length;
        if (count == 1)
            return [1];


        var leftProduct = new List<int>(count);
        var rightProduct = new List<int>(count);

        // O(n)
        // Build left product
        for (int i = 0; i < count; i++)
        {

        }

        return Array.Empty<int>();
    }
}
