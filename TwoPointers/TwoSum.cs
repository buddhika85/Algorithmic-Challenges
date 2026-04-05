namespace AlgorithmsPractice.TwoPointers;

public class TwoSum
{
    public TwoSum()
    {
        var testData = new (int testCaseNum, int[] nums, int target, int[] expected)[]
        {
            (1, new int[] { 2, 7, 11, 15 }, 9, new int[] { 0, 1 }),
            (2, new int[] { 3, 2, 4 }, 6, new int[] { 1, 2 }),
            (3, new int[] { 3, 3 }, 6, new int[] { 0, 1 }),
            (4, new int[] { 1, 5, 3, 7 }, 8, new int[] { 1, 2 }),
            (5, new int[] { 1, 2, 3 }, 7, Array.Empty<int>()), // no solution
        };

        foreach (var (testCaseNum, nums, target, expected) in testData)
        {
            var result = Solve(nums, target);
            var status = result.SequenceEqual(expected) ? "Passed" : "FAILED";
            Console.WriteLine($"{testCaseNum}. {status}\t=> result=[{string.Join(",", result)}], expected=[{string.Join(",", expected)}]");
        }
    }

    /*
        Problem: Two Sum

        Description:
            Given an array of integers nums and an integer target,
            return indices of the two numbers such that they add up to target.

            You may assume each input has exactly one solution,
            and you may not use the same element twice.

        Approach:
            - Use a HashMap (Dictionary<int,int>) to store value → index.
            - For each number, compute complement = target - num.
            - If complement exists in map, return indices.
            - Otherwise, add current number to map.

        Time Complexity:
            O(n) — single pass with O(1) average lookup.
    */

    public int[] Solve(int[] nums, int target)
    {
        // dictionary to store each num as key and their indexes as values
        var numIndexes = new Dictionary<int, List<int>>();

        for (var i = 0; i < nums.Length; i++)
        {
            var howManyMore = target - nums[i];
            // if howManyMore available return the indexes
            if (numIndexes.TryGetValue(howManyMore, out List<int>? indexes))
            {
                return [indexes[0], i];
            }

            // add to dictionary
            if (numIndexes.ContainsKey(nums[i]))
            {
                numIndexes[nums[i]].Add(i);
            }
            else
            {
                numIndexes[nums[i]] = [i];
            }
        }

        return Array.Empty<int>();
    }


    public int[] Solve2(int[] nums, int target)
    {
        var map = new Dictionary<int, int>(); // value → index

        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];

            if (map.TryGetValue(complement, out int index))
                return new int[] { index, i };

            map[nums[i]] = i;
        }

        return Array.Empty<int>();
    }

}
