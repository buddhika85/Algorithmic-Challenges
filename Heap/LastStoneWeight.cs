namespace AlgorithmsPractice.HeapProblems;

public class LastStoneWeight
{
    public LastStoneWeight()
    {
        var testData = new (int testCaseNum, int[] stones, int expected)[]
        {
            (1, new int[] { 2, 7, 4, 1, 8, 1 }, 1),   // classic example → final stone = 1
            (2, new int[] { 1 }, 1),                 // single stone
            (3, new int[] { 3, 3 }, 0),              // both smash → 0
            (4, new int[] { 10, 4, 2, 10 }, 2),      // 10-10=0, 4-2=2 → final = 2
            (5, new int[] { 9, 3, 2, 10 }, 0),       // 10-9=1, 3-2=1, 1-1=0
        };

        foreach (var (testCaseNum, stones, expected) in testData)
        {
            var result = Solve(stones);
            var status = result == expected ? "Passed" : "FAILED";
            Console.WriteLine($"{testCaseNum}. {status}\t=> result={result}, expected={expected}");
        }
    }

    /*
        Problem: Last Stone Weight

        Description:
            You are given an array of integers stones where each stone has a positive weight.

            Every turn:
                - Take the two heaviest stones.
                - Smash them together.
                - If they are equal, both are destroyed.
                - If they are different, the smaller one is destroyed and the larger one
                  becomes (difference of weights).

            Continue until at most one stone remains.

            Return the weight of the last remaining stone, or 0 if none remain.

        Examples:
            Input:  [2,7,4,1,8,1]
            Output: 1

            Explanation:
                8 and 7 → smash → 1     [2,4,1,1,1]
                4 and 2 → smash → 2     [1,1,1,2]
                2 and 1 → smash → 1     [1,1,1]
                1 and 1 → smash → 0     [1,0]
                Final = 1   

        Approach:
            - Use a max-heap (PriorityQueue<int,int> with negative priority).
            - Always extract the two largest stones.
            - If they differ, push the difference back into the heap.
            - Continue until ≤ 1 stone remains.

        Expected Time Complexity:
            O(n log n)
            - Each stone is inserted once → O(n log n)
            - Each smash operation involves 2 pops + optional push → O(log n)
            - Total = n iterations × log n per iteration = 𝑂(𝑛 log n)

            The smallest priority value comes out first.
    */

    public int Solve(int[] stones)
    {
        // put all in a max heap
        var maxPriorityQueue = new PriorityQueue<int, int>();

        foreach (var item in stones)
        {
            maxPriorityQueue.Enqueue(item, -item);
        }

        // while there is 2 stones to smash
        while (maxPriorityQueue.Count >= 2)
        {
            var remains = maxPriorityQueue.Dequeue() - maxPriorityQueue.Dequeue();
            if (remains > 0)
                maxPriorityQueue.Enqueue(remains, -remains);
        }

        return maxPriorityQueue.Count > 0 ? maxPriorityQueue.Peek() : 0;
    }
}
