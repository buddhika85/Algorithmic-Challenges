using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.Medium;

public class ReverseLinkedList
{
    private readonly bool _debug = false; // Set to true to print internal states

    public ReverseLinkedList()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Reverse Linked List
     * ------------------------------------------------------------
     *  Given the head of a singly linked list, reverse the list,
     *  and return the new head.
     *
     *  Example:
     *      Input:
     *          1 -> 2 -> 3 -> 4 -> 5 -> null
     *
     *      Output:
     *          5 -> 4 -> 3 -> 2 -> 1 -> null
     *
     * ------------------------------------------------------------
     *  Expected Time: 2–5 minutes (warm-up)
     * ------------------------------------------------------------
     */

    // Basic singly linked list node
    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int value)
        {
            val = value;
            next = null;
        }
    }

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, ListNode head, ListNode expected)[]
        {
            (1, "Example case", BuildList(new[] { 1, 2, 3, 4, 5 }), BuildList(new[] { 5, 4, 3, 2, 1 })),
            (2, "Single element", BuildList(new[] { 7 }), BuildList(new[] { 7 })),
            (3, "Two elements", BuildList(new[] { 1, 2 }), BuildList(new[] { 2, 1 })),
            (4, "Empty list", null, null)
        };

        foreach (var (num, desc, head, expected) in testHarness)
        {
            Test(num, desc, head, expected);
        }
    }

    private void Test(int num, string desc, ListNode head, ListNode expected)
    {
        var sw = Stopwatch.StartNew();
        ListNode actual = Solve(head);
        sw.Stop();

        bool pass = CompareLists(actual, expected);

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc}");
            Console.Write("Expected: ");
            PrintList(expected);
            Console.Write("Actual:   ");
            PrintList(actual);
        }
    }


    /*
         * ------------------------------------------------------------
         *  Problem: Reverse Linked List
         * ------------------------------------------------------------
         *  Given the head of a singly linked list, reverse the list,
         *  and return the new head.
         *
         *  Example:
         *      Input:
         *          1 -> 2 -> 3 -> 4 -> 5 -> null
         *
         *      Output:
         *          5 -> 4 -> 3 -> 2 -> 1 -> null
         *
         * ------------------------------------------------------------
         *  Expected Time: 2–5 minutes (warm-up)
         * ------------------------------------------------------------
         */

    /*

    var curr = head;
    var prev = null;

    while(curr != null)
    {
        var temp = curr.next;
        curr.next = prev;
        prev = curr;
        curr = temp;
    }

    return prev;

    */

    // ------------------------------------------------------------
    // Solve method: implement logic here
    // Ill use 2 pointers, curr and prev. and curr will initialy point to header and prev will be null. and will run a while loop until we hit null (tails next).
    // with in loop Ill take next element to a temp.
    // then Ill assign currs next to prev. 
    // then Ill prepare for next iteration. prev = curr and curr will become next item which is on temp.
    //
    // Best case Time Complexity: O(n) --> as number of computations increase with number of nodes in Linked List.
    // Average case Time Complexity: O(n)
    // Worst case Time Complexity: O(n)
    // Space Complexity: O(1) --> in place reversal, no additional DSs used.
    // ------------------------------------------------------------
    private ListNode Solve(ListNode head)
    {
        var curr = head;
        ListNode prev = null!;

        while (curr != null)
        {
            var temp = curr.next;
            curr.next = prev;
            prev = curr;
            curr = temp;
        }

        return prev;
    }

    // Optional brute-force approach (not needed for this problem)
    private ListNode Solve2(ListNode head)
    {
        // TODO: implement alternative solution
        return null;
    }

    // ---------------- Helper Methods ----------------

    private ListNode BuildList(int[] values)
    {
        if (values == null || values.Length == 0)
            return null;

        var head = new ListNode(values[0]);
        var current = head;

        for (int i = 1; i < values.Length; i++)
        {
            current.next = new ListNode(values[i]);
            current = current.next;
        }

        return head;
    }

    private bool CompareLists(ListNode a, ListNode b)
    {
        while (a != null && b != null)
        {
            if (a.val != b.val)
                return false;

            a = a.next;
            b = b.next;
        }

        return a == null && b == null;
    }

    private void PrintList(ListNode head)
    {
        var curr = head;
        while (curr != null)
        {
            Console.Write(curr.val + " -> ");
            curr = curr.next;
        }
        Console.WriteLine("null");
    }
}
