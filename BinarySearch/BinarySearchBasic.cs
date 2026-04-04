namespace AlgorithmsPractice.BinarySearch;

public class BinarySearchBasic
{
    public BinarySearchBasic()
    {
        var testData = new (int[] nums, int target, int expected)[]
        {
            (new int[] { }, 5, -1),                       // empty array
            (new int[] { 1 }, 1, 0),                      // single element found
            (new int[] { 1 }, 2, -1),                     // single element not found
            (new int[] { 1, 3, 5, 7, 9 }, 3, 1),          // middle-left
            (new int[] { 1, 3, 5, 7, 9 }, 9, 4),          // last element
            (new int[] { 1, 3, 5, 7, 9 }, 4, -1),         // not found
        };

        foreach (var (nums, target, expected) in testData)
        {
            var result = Solve(nums, target);
            Console.WriteLine($"[{string.Join(",", nums)}], target={target} => {result} (expected: {expected})");
        }
    }

    /*
        Problem: Binary Search (Classic)

        Description:
            Given a sorted array of integers and a target value,
            return the index of the target if it exists.
            If it does not exist, return -1.

        Examples:
            Input:  nums = [1,3,5,7,9], target = 3
            Output: 1

            Input:  nums = [1,3,5,7,9], target = 4
            Output: -1

        Approach:
            - Use two pointers: left = 0, right = nums.Length - 1
            - While left <= right:
                - Compute mid = left + (right - left) / 2
                - If nums[mid] == target → return mid
                - If nums[mid] < target → search right half (left = mid + 1)
                - Else → search left half (right = mid - 1)
            - If not found → return -1

        Expected Time Complexity:
            O(log n)
            8:53 - 8:59
    */

    public int Solve(int[] nums, int target)
    {
        // nums.Sort(); --> nums is already sorted in binary search questions
        var left = 0;
        var right = nums.Length - 1;

        while (left <= right)
        {
            var middle = (left + right) / 2;
            if (nums[middle] == target)
                return middle;
            if (nums[middle] > target)
            {
                // target should be with in left portion
                right = middle - 1;
            }
            else
            {
                // target should be with in right portion
                left = middle + 1;
            }
        }

        return -1;          // did not find
    }
}
