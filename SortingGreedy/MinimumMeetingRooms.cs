namespace AlgorithmsPractice.SortingGreedy;

public class MinimumMeetingRooms
{
    public MinimumMeetingRooms()
    {
        var testData = new List<(int[][] intervals, int expected)>
        {
            (new int[][] { new[] {0, 30}, new[] {5, 10}, new[] {15, 20} }, 2),
            (new int[][] { new[] {7, 10}, new[] {2, 4} }, 1),
            (new int[][] { new[] {1, 5}, new[] {6, 10} }, 1),
            (new int[][] { new[] {1, 5}, new[] {2, 6}, new[] {3, 7} }, 3),
            (Array.Empty<int[]>(), 0),
            (null, 0)
        };

        foreach (var (intervals, expected) in testData)
        {
            Console.WriteLine($"Result: {Solve(intervals)} (expected: {expected})");
        }
    }

    /*
        Problem: Minimum Number of Meeting Rooms

        Description:
            Given an array of meeting time intervals where each interval 
            is represented as [start, end], determine the minimum number 
            of conference rooms required.

        Examples:
            Input:  [[0,30],[5,10],[15,20]]
            Output: 2

            Input:  [[7,10],[2,4]]
            Output: 1

        Approach (Sorting + Greedy):         [[0,30],[5,10],[15,20]]
            - Extract all start times and end times separately.
                - start 0,5,15
                - end 10, 20, 30
            - Sort both arrays.
            - Use two pointers:
                * If next meeting starts before the earliest ending meeting finishes,
                  we need a new room.
                * Otherwise, reuse a room (move end pointer).
            - Track the maximum number of rooms needed.

        Time Complexity:
            O(n log n) due to sorting.
    */

    public int Solve(int[][]? intervals)
    {
        // Edge case: no meetings → no rooms needed
        if (intervals is null || intervals.Length == 0)
            return 0;

        int n = intervals.Length;

        // Extract start and end times into separate arrays
        var starts = new int[n];
        var ends = new int[n];

        for (int i = 0; i < n; i++)
        {
            starts[i] = intervals[i][0];
            ends[i] = intervals[i][1];
        }

        // Sort start times and end times independently
        Array.Sort(starts);
        Array.Sort(ends);

        int roomsInUse = 0;
        int maxRooms = 0;

        int iStart = 0; // pointer for next meeting start
        int iEnd = 0;   // pointer for earliest meeting end

        // Process all meetings in order of start time
        while (iStart < n)
        {
            // If the next meeting starts before the earliest one ends,
            // we need a new room.
            if (starts[iStart] < ends[iEnd])
            {
                roomsInUse++;
                maxRooms = Math.Max(maxRooms, roomsInUse);
                iStart++;
            }
            else
            {
                // Otherwise, a meeting has ended → free a room
                roomsInUse--;
                iEnd++;
            }
        }

        return maxRooms;
    }
}
