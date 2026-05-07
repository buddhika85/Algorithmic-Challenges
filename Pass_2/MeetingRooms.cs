using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_2;

public class MeetingRooms
{
    private readonly bool _debug = false; // Set to true to print internal states

    public MeetingRooms()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Meeting Rooms II
     * ------------------------------------------------------------
     *  Given an array of meeting time intervals where
     *  intervals[i] = [start_i, end_i],
     *  return the minimum number of conference rooms required.
     *
     *  Example:
     *      Input:
     *          intervals = [[0, 30], [5, 10], [15, 20]]
     *
     *      Output:
     *          2
     *
     *      Explanation:
     *          Overlaps require two rooms.
     *
     * ------------------------------------------------------------
     *  Expected Time: 10–15 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, int[][] intervals, int expected)[]
        {
            (1, "Example case", new[] { new[] {0, 30}, new[] {5, 10}, new[] {15, 20} }, 2),
            (2, "No overlap", new[] { new[] {1, 2}, new[] {3, 4}, new[] {5, 6} }, 1),
            (3, "All overlap", new[] { new[] {1, 5}, new[] {2, 6}, new[] {3, 7} }, 3),
            (4, "Single meeting", new[] { new[] {10, 20} }, 1),
            (5, "Empty input", Array.Empty<int[]>(), 0)
        };

        foreach (var (num, desc, intervals, expected) in testHarness)
        {
            Test(num, desc, intervals, expected);
        }
    }

    private void Test(int num, string desc, int[][] intervals, int expected)
    {
        var sw = Stopwatch.StartNew();
        int actual = Solve(intervals);
        sw.Stop();

        bool pass = actual == expected;

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | Rooms={actual} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | Expected={expected}, Actual={actual}");
        }
    }

    // ------------------------------------------------------------
    // Solve method: implement logic here
    /*  Example:
         *      Input:
         *          intervals = [[0, 30], [5, 10], [15, 20]]
         *
         *      Output:
         *          2

        0,5,15          10,20,30

        start < end     -- room count
        0 < 10          -- 1 
        5 < 10          -- 2
        15 >= 10        -- 1


(3, "All overlap", new[] { new[] {1, 5}, new[] {2, 6}, new[] {3, 7} }, 3),

1,2,3         5,6,7

1 < 5 = 1
2 < 5 = 2
3 < 5 = 3

         */
    // Best case Time Complexity: O(n log n)   -- sorting consumes higher complexity - so  Array.Sort in .NET uses introspective sort, which is still O(n log n).
    // Average case Time Complexity: O(n log n)   
    // Worst case Time Complexity: O(n log n)   
    // Space Complexity: O(n)   -- we use 2 additonal arrays of same n size
    // ------------------------------------------------------------
    private int Solve(int[][] intervals)
    {
        var meetingCount = intervals.Length;

        var startTimes = new int[meetingCount];
        var endTimes = new int[meetingCount];

        for (int i = 0; i < intervals.Length; i++)
        {
            var meeting = intervals[i];
            startTimes[i] = meeting[0];
            endTimes[i] = meeting[1];
        }

        // sort
        Array.Sort(startTimes);     // O(n log n) 
        Array.Sort(endTimes);       // O(n log n) 

        var currRoomCount = 0;
        var maxRoomCount = 0;
        var currStartTimeIndex = 0;
        var currEndTimeIndex = 0;

        while (currStartTimeIndex < meetingCount)
        {
            if (startTimes[currStartTimeIndex] < endTimes[currEndTimeIndex])
            {
                // meeting is going on - need a new room
                ++currRoomCount;
                ++currStartTimeIndex;
            }
            else
            {
                // meeting has ended - room is free
                --currRoomCount;
                ++currEndTimeIndex;
            }
            maxRoomCount = Math.Max(maxRoomCount, currRoomCount);
        }

        return maxRoomCount;
    }

    // Optional brute-force approach
    private int Solve2(int[][] intervals)
    {
        // TODO: implement alternative solution
        return 0;
    }
}
