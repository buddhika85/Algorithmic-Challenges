namespace AlgorithmsPractice.SortingAndGreedy;

public class MeetingRooms
{
    public MeetingRooms()
    {
        var testData = new (int testCaseNum, int[][] intervals, int expected)[]
        {
            (
                1,
                new int[][]
                {
                    new[] { 0, 30 },
                    new[] { 5, 10 },
                    new[] { 15, 20 }
                },
                2
            ),
            (
                2,
                new int[][]
                {
                    new[] { 7, 10 },
                    new[] { 2, 4 }
                },
                1
            ),
            (
                3,
                new int[][]
                {
                    new[] { 1, 5 },
                    new[] { 8, 9 },
                    new[] { 8, 9 }
                },
                2
            )
        };

        foreach (var (testCaseNum, intervals, expected) in testData)
        {
            var result = Solve(intervals);
            var testCaseResult = result == expected ? "Passed" : "FAILED";
            Console.WriteLine($"{testCaseNum}. {testCaseResult} (Expected: {expected}, Got: {result})");
        }
    }

    /*
        Problem: Meeting Rooms II

        Description:
            Given an array of meeting intervals where each interval is [start, end],
            return the minimum number of meeting rooms required.
  new int[][]
                {
                    new[] { 0, 30 },
                    new[] { 5, 10 },
                    new[] { 15, 20 }
                },
        0,5,15      10,20,30
        s = 0
        e = 0

        while start Index value < starts.Length

        0 < 10  --> T --> s = 1, e = 0, roomsInUse = 1, maxRooms = 1
        5 < 10  --> T --> s = 2, e = 0, roomsInUse = 2, maxRooms = 2
        15 < 10 --> F --> s = 2, e = 1, roomsInUse = 1, maxRooms = 2
        15 < 20 --> T --> s = 3, e = 2, roomsInUse = 2, maxRooms = 2

        exit loop because start index = 3 < starts.length

    11:35 - 11:53
    */

    public int Solve(int[][] intervals)
    {
        var meetingsCount = intervals.Length;
        var startTimes = new int[meetingsCount];
        var endTimes = new int[meetingsCount];

        var currentMeetingCount = 0;
        var maximumRoomCount = 0;
        var startIndex = 0;
        var endIndex = 0;

        // O(n)
        for (var i = 0; i < meetingsCount; i++)
        {
            startTimes[i] = intervals[i][0];
            endTimes[i] = intervals[i][1];
        }

        // O(n log n)
        Array.Sort(startTimes);
        Array.Sort(endTimes);

        // O(n)
        while (startIndex < meetingsCount)
        {
            if (startTimes[startIndex] < endTimes[endIndex])
            {
                currentMeetingCount++;
                startIndex++;
            }
            else
            {
                currentMeetingCount--;
                endIndex++;
            }

            maximumRoomCount = Math.Max(currentMeetingCount, maximumRoomCount);
        }

        // Total = O(n log n) > O(n) , So O(n log n)
        return maximumRoomCount;
    }



}
