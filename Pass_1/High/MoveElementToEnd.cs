using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High;

public class MoveElementToEnd
{
    private readonly bool _debug = false; // Set to true to print internal states

    public MoveElementToEnd()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Move Element To End
     * ------------------------------------------------------------
     *  You're given an array of integers and an integer toMove.
     *  Move all instances of toMove to the end of the array.
     *
     *  - The function must mutate the input array in-place.
     *  - The order of the other integers does NOT need to be preserved.
     *  - Return the modified array.
     *
     *  Example:
     *      Input:
     *          array = [2, 1, 2, 2, 2, 3, 4, 2]
     *          toMove = 2
     *
     *      Output:
     *          [1, 3, 4, 2, 2, 2, 2, 2]
     *
     * ------------------------------------------------------------
     *  Expected Time: 5–10 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, int[] array, int toMove, int[] expected)[]
        {
            (1, "Example case", new[] { 2, 1, 2, 2, 2, 3, 4, 2 }, 2, new[] { 1, 3, 4, 2, 2, 2, 2, 2 }),
            (2, "No elements to move", new[] { 1, 3, 4 }, 2, new[] { 1, 3, 4 }),
            (3, "All elements to move", new[] { 5, 5, 5 }, 5, new[] { 5, 5, 5 }),
            (4, "Mixed values", new[] { 0, 1, 0, 3, 12 }, 0, new[] { 1, 3, 12, 0, 0 }),
        };

        foreach (var (num, desc, array, toMove, expected) in testHarness)
        {
            Test(num, desc, array, toMove, expected);
        }
    }

    private void Test(int num, string desc, int[] array, int toMove, int[] expected)
    {
        var sw = Stopwatch.StartNew();
        int[] actual = Solve(array, toMove);
        sw.Stop();

        bool pass = actual.Length == expected.Length &&
                    actual.Zip(expected, (a, b) => a == b).All(x => x);

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | toMove={toMove} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | toMove={toMove} | Expected=[{string.Join(",", expected)}], Actual=[{string.Join(",", actual)}]");
        }
    }

    // ------------------------------------------------------------
    // Solve method: implement logic here
    // use 2 pointers - left = 0, right = length - 1
    // move left and right pointers inward in a loop. see below.
    // Best case Time Complexity: O(n)
    // Average case Time Complexity: O(n)
    // Worst case Time Complexity: O(n)
    // Space Complexity: O(1)       same array manipulated, not additional DSs created
    // ------------------------------------------------------------
    private int[] Solve(int[] array, int toMove)
    {
        var left = 0;
        var right = array.Length - 1;

        while (left < right)
        {
            if (array[left] == toMove)
            {
                if (array[right] != toMove)
                {
                    var temp = array[left];
                    array[left] = array[right];
                    array[right] = temp;
                    ++left;
                }
                else
                {
                    --right;
                }
            }
            else
            {
                ++left;
            }
        }
        return array;
    }

    // Optional - using list as algo expert
    private List<int> Solve2(List<int> array, int toMove)
    {
        var left = 0;
        var right = array.Count - 1;

        while (left < right)
        {
            if (array[left] == toMove)
            {
                if (array[right] != toMove)
                {
                    var temp = array[left];
                    array[left] = array[right];
                    array[right] = temp;
                    ++left;
                }
                else
                {
                    --right;
                }
            }
            else
            {
                ++left;
            }
        }
        return array;
    }
}
