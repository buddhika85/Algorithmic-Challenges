using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High;

public class MeetingRoomsII
{
    private readonly bool _debug = false; // Set to true to print internal states

    public MeetingRoomsII()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Meeting Rooms II
     * ------------------------------------------------------------
     *  Given an array of meeting time intervals where
     *  intervals[i] = [start, end],
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
     *          - Meeting [0,30] overlaps with [5,10]
     *          - Meeting [0,30] overlaps with [15,20]
     *          → Need 2 rooms
     *
     * ------------------------------------------------------------
     *  Expected Time: 10–15 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, int[][] intervals, int expected)[]
        {
            (1, "Example case", new[] { new[] {0,30}, new[] {5,10}, new[] {15,20} }, 2),
            (2, "No overlap", new[] { new[] {1,2}, new[] {3,4}, new[] {5,6} }, 1),
            (3, "All overlap", new[] { new[] {1,4}, new[] {2,5}, new[] {3,6} }, 3),
            (4, "Single meeting", new[] { new[] {10,20} }, 1),
            (5, "Edge touching", new[] { new[] {1,3}, new[] {3,5} }, 1)
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
            Console.WriteLine($"[PASS] {num}. {desc} | Expected={expected} | Time={sw.ElapsedTicks} ticks");
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
      *
      *      Explanation:
      *          - Meeting [0,30] overlaps with [5,10]
      *          - Meeting [0,30] overlaps with [15,20]
      *          → Need 2 rooms
      */
    // we form 2 arrays - which are startTimes & endTimes
    // currentRoomCount = 0
    // maxRoomCount = 0
    // startTimes = [0,5,15]     endTimes  = [30,10,20]
    // sort = [0,5,15]     endTimes  = [10,20,30]
    // while Process all meeting start times --> startIndex < meeting count
    //     if startTimes[sIndex] < endTimes[endIndex]
    //          currentRoomCount++            
    //     else
    //          currentRoomCount--
    //     maxRoomCount = max(currentRoomCount, maxRoomCount)
    // return maxRoomCount

    // startTimes = [0,5,15]     endTimes  = [30,10,20]
    // sort --> startTimes= [0,5,15]     endTimes  = [10,20,30]

    // startIndex = 0, endIndex = 0
    // while start index pointing value < end index pointing value
    // 0 < 10  - true - currentCount = 1, maxCount = 1, startIndex = 1, endIndex = 0
    // 5 < 10  - true - currentCount = 2, maxCount = 2, startIndex = 2, endIndex = 0
    // 15 < 10 - false - currentCount = 1, maxCount = 2, startIndex = 2, endIndex = 1
    // 15 < 20 - true - currentCount = 2, maxCount = 2, startIndex = 3, endIndex = 1
    // startIndex = 3 --> exiting loop


    // Best case Time Complexity: O(1)  --> only 1 meeting
    // Average case Time Complexity: O(n)  --> time grows with N - number of meetings
    // Worst case Time Complexity: O(n)  --> time grows with N - number of meetings
    // Space Complexity: -- O(n)   --> we need 2 addtional arrays with same N size
    // ------------------------------------------------------------

    /*
We separate start and end times into two arrays and sort them.
We use two pointers:
- startIndex moves when a meeting starts
- endIndex moves when a meeting ends

If the next meeting starts before the earliest ending meeting finishes,
we need a new room. Otherwise, a room gets freed.

We track the maximum number of rooms in use at any time.
*/
    private int Solve(int[][] intervals)
    {

        if (intervals.Length == 1)      // 1 meeting 1 room - best case
            return 1;

        var meetingCount = intervals.Length;
        var startTimes = new int[meetingCount];
        var endTimes = new int[meetingCount];

        // O(n)
        for (int i = 0; i < intervals.Length; i++)
        {
            startTimes[i] = intervals[i][0];
            endTimes[i] = intervals[i][1];
        }

        // O(n log n)
        Array.Sort(startTimes);

        // O(n log n)
        Array.Sort(endTimes);


        var startIndex = 0;
        var endIndex = 0;
        var currentRoomCount = 0;
        var maximumRoomCount = 0;

        // O(n)
        while (startIndex < meetingCount)
        {
            if (startTimes[startIndex] < endTimes[endIndex])
            {
                currentRoomCount++;
                startIndex++;
            }
            else
            {
                currentRoomCount--;
                endIndex++;
            }
            maximumRoomCount = Math.Max(maximumRoomCount, currentRoomCount);
        }

        return maximumRoomCount;
    }

    // Optional brute-force approach
    private int Solve2(int[][] intervals)
    {
        // TODO: implement alternative solution
        return 0;
    }
}
