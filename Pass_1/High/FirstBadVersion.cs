using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High;

public class FirstBadVersion
{
    private readonly bool _debug = false; // Set to true to print internal states

    public FirstBadVersion()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: First Bad Version
     * ------------------------------------------------------------
     *  You are a product manager and currently leading a team to
     *  develop a new product. Unfortunately, the latest version
     *  fails the quality check. Since each version is built upon
     *  the previous one, all versions after a bad version are bad.
     *
     *  Given n versions [1, 2, ..., n], find the first bad one.
     *
     *  You are provided with a helper API:
     *      bool IsBadVersion(int version)
     *
     *  Your task is to implement a method that returns the first
     *  bad version using the fewest number of calls to the API.
     *
     *  Example:
     *      Input:
     *          n = 5, bad = 4
     *
     *      Output:
     *          4
     *
     *      Explanation:
     *          versions: 1 2 3 4 5
     *                     G G G B B
     *
     * ------------------------------------------------------------
     *  Expected Time: 5–10 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, int n, int bad, int expected)[]
        {
            (1, "Example case", 5, 4, 4),
            (2, "Bad at start", 5, 1, 1),
            (3, "Bad in middle", 10, 6, 6),
            (4, "Bad at end", 7, 7, 7)
        };

        foreach (var (num, desc, n, bad, expected) in testHarness)
        {
            Test(num, desc, n, bad, expected);
        }
    }

    private void Test(int num, string desc, int n, int bad, int expected)
    {
        _badVersion = bad; // Set global bad version for test

        var sw = Stopwatch.StartNew();
        int actual = Solve(n);
        sw.Stop();

        bool pass = actual == expected;

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | n={n}, bad={bad} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | n={n}, bad={bad} | Expected={expected}, Actual={actual}");
        }
    }

    // ------------------------------------------------------------
    // Mock API: IsBadVersion
    // ------------------------------------------------------------
    private int _badVersion;
    private bool IsBadVersion(int version) => version >= _badVersion;
    /*
    (int n, int bad)[]

    (5, 4),

    G G G B B
    1 2 3 4 5

    start = 1       end = 5         mid = 3     isBad = false    --> go right                                           minBad = -1         largestGood = 3
    start = 4       end = 5         mid = 4     isBad = true     --> we need to check left find minimal possible bad    minBad = 4          

    if minBad = largestGood + 1 --> means we found 

    G G G G G G G G B  B
    1 2 3 4 5 6 7 8 9 10

    start = 1       end = 10         mid = 5     isBad = false    --> go right                                           minBad = -1         largestGood = 5
    start = 6       end = 10         mid = 8     isBad = false    --> go right                                           minBad = -1         largestGood = 8
    start = 9       end = 10         mid = 9     isBad = true     --> we need to check left find minimal possible bad    minBad = 9  
    start = 9       end = 10         mid = 9     isBad = true     --> we need to check left find minimal possible bad    minBad = 9  


    G G G G G G G G G  B
    1 2 3 4 5 6 7 8 9 10
    start = 1       end = 10         mid = 5     isBad = false    --> go right                                           minBad = -1        
    start = 6       end = 10         mid = 8     isBad = false    --> go right                                           minBad = -1  
    start = 9       end = 10         mid = 9     isBad = true     -->                                                    minBad = 9    


    G G G G G B
    1 2 3 4 5 6 7 8 9 10
    start = 1       end = 10         mid = 5     isBad = false    --> go right                                           minBad = -1        
    start = 6       end = 10         mid = 8     isBad = true    -->  go left                                            minBad = 8  
    start = 6       end =  7         mid = 6     isBad = true     --> go left                                            minBad = 6   
    start = 5       end =  7         mid = 6     isBad = true     --> go left                                            minBad = 6    
    start = 7       end =  7         mid = 7     isBad = true     --> go left                                            minBad = 6 

    G G G G G G B
    1 2 3 4 5 6 7

    start = 1       end = 7         mid = 4     isBad = false    --> go right                                           minBad = -1    
    start = 5       end = 7         mid = 6     isBad = false    --> go right                                           minBad = -1 
    start = 5       end = 7         mid = 6     isBad = false    --> go right                                           minBad = -1 

    if minBad = largestGood + 1 --> means we found 
    */
    // ------------------------------------------------------------
    // Solve method: implement logic here
    // -- I have to fill this --
    //
    // Best case Time Complexity: O(1)  (bad version is mid on first check)
    // Average case Time Complexity: O(log n)
    // Worst case Time Complexity: O(log n)
    // Space Complexity: O(1)
    //
    // Hint:
    // Use binary search to minimize calls to IsBadVersion.
    // ------------------------------------------------------------
    private int Solve(int n)
    {
        var startVersion = 1;
        var endVersion = n;
        var earliestBadVersionFound = -1;

        while (startVersion <= endVersion)
        {
            var mid = (startVersion + endVersion) / 2;
            if (IsBadVersion(mid))
            {
                earliestBadVersionFound = mid;
                // check left for smaller
                endVersion = mid - 1;
            }
            else
            {
                // means bad version range is on right
                startVersion = mid + 1;
            }
        }

        return earliestBadVersionFound;
    }

    // Optional brute-force approach
    private int Solve2(int n)
    {
        // TODO: implement linear scan
        return -1;
    }
}
