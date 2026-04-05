namespace AlgorithmsPractice.HeapProblems;

public class KthLargestElement
{
    public KthLargestElement()
    {
        var testData = new (int testCaseNum, int[] nums, int k, int expected)[]
        {
            (1, new int[] { 3, 2, 1, 5, 6, 4 }, 2, 5),          // 2nd largest is 5
            (2, new int[] { 3, 2, 3, 1, 2, 4, 5, 5, 6 }, 4, 4), // 4th largest is 4
            (3, new int[] { 1 }, 1, 1),                        // single element
            (4, new int[] { 7, 10, 4, 3, 20, 15 }, 3, 10),     // classic example  - 20,15,10,7,4,3
            (5, new int[] { 5, 5, 5, 5 }, 2, 5),               // duplicates
        };

        foreach (var (testCaseNum, nums, k, expected) in testData)
        {
            var result = Solve(nums, k);
            var status = result == expected ? "Passed" : "FAILED";
            Console.WriteLine($"{testCaseNum}. {status}\t=> k={k}, result={result}, expected={expected}");
        }
    }

    /*
        Problem: Kth Largest Element in an Array

        Description:
            Given an integer array nums and an integer k,
            return the k-th largest element in the array.

            Note:
                - The k-th largest means sorted in descending order.
                - Example: [3,2,1,5,6,4], k=2 → 5

        Examples:
            Input:  nums = [3,2,1,5,6,4], k = 2
            Output: 5

            Input:  nums = [3,2,3,1,2,4,5,5,6], k = 4
            Output: 4

        Approach:
            - Use a min-heap (PriorityQueue<int,int> in C#).
            - Keep only k elements in the heap.
            - If heap size exceeds k, pop the smallest.
            - At the end, the root of the heap is the k-th largest.

        Expected Time Complexity:
            O(n log k)
            
            from the loop we get O(n),
            and priority queues insert, remove is always O(log k) because it never grows beyond k
            so, Total = n iterations × log k per iteration = 𝑂(𝑛 log 𝑘)
    */

    // new int[] { 3, 2, 1, 5, 6, 4 }, 2, 5),          // 2nd largest is 5
    // 3,2,1 - Deq 1 - 3,2,5 - Deq 2 - 3,5,6 - Deq 3 - 5,6,4 - Deq 4 - 5,6, end of items - peek 5
    public int Solve(int[] nums, int k)
    {
        //return nums.OrderByDescending(x => x).Skip(k - 1).First();

        // when you Dequeue, always smallest of the queue gets removed
        var minPriorityQueue = new PriorityQueue<int, int>();       // num , priority
        foreach (var item in nums)
        {
            minPriorityQueue.Enqueue(item, item);

            // remove min when it exceeds maximum count to maintain
            if (minPriorityQueue.Count > k)
                minPriorityQueue.Dequeue();
        }

        // return the smallest of the queue == kth largets of the nums
        return minPriorityQueue.Peek();
    }
}
