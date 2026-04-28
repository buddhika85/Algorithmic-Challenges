using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_2;

public class MinStack
{
    private readonly bool _debug = false; // Set to true to print internal states

    public MinStack()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Min Stack
     * ------------------------------------------------------------
     *  Design a stack that supports the following operations
     *  in O(1) time:
     *
     *      - Push(x): Push element x onto stack.
     *      - Pop(): Remove the element on top of the stack.
     *      - Top(): Get the top element.
     *      - GetMin(): Retrieve the minimum element in the stack.
     *
     *  You must implement the stack yourself.
     *
     *  Example:
     *      Input:
     *          push(2)
     *          push(0)
     *          push(3)
     *          push(0)
     *          getMin() -> 0
     *          pop()
     *          getMin() -> 0
     *          pop()
     *          getMin() -> 0
     *          pop()
     *          getMin() -> 2
     *
     * ------------------------------------------------------------
     *  Expected Time: 10–15 minutes
     * ------------------------------------------------------------
     */

    // ------------------------------------------------------------
    // Internal data structures
    // -- You have to fill these --
    // ------------------------------------------------------------

    // TODO: declare your internal stack(s) here
    private Stack<(int Number, int CurrMin)> _minStack = new();            // number, minimal as of now


    private bool IsEmpty()
    {
        return _minStack.Count == 0;
    }


    // ------------------------------------------------------------
    // Core API methods
    // -- You have to fill these --
    // ------------------------------------------------------------
    public void Push(int x)
    {
        if (IsEmpty())
        {
            _minStack.Push((x, x));
            return;
        }

        var (Number, CurrMin) = _minStack.Peek();
        _minStack.Push((x, Math.Min(CurrMin, x)));
    }

    public void Pop()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Cannot Pop, Min Stack is Empty");

        var (Number, CurrMin) = _minStack.Pop();

        if (_debug)
        {
            Console.WriteLine($"Pop() - {Number} min {CurrMin}");
        }
    }

    public int Top()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Cannot Top, Min Stack is Empty");

        return _minStack.Peek().Number;
    }

    public int GetMin()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Cannot Get Min, Min Stack is Empty");

        return _minStack.Peek().CurrMin;
    }

    // ------------------------------------------------------------
    // Complexity:
    // push: O(1) — constant‑time stack push and constant‑time min comparison
    // pop: O(1) — constant‑time stack pop
    // top: O(1) — just peek
    // getMin: O(1) — min is stored with the top element
    // space: O(n) — storing one extra integer per pushed value
    // ------------------------------------------------------------


    // ------------------------------------------------------------
    // Test Harness
    // ------------------------------------------------------------
    private void RunAllTests()
    {
        Console.WriteLine("Running MinStack tests...\n");

        Test(1, "Basic push/pop/min sequence", () =>
        {
            var st = new MinStackInternal();
            st.Push(2);
            st.Push(0);
            st.Push(3);
            st.Push(0);

            return new[]
            {
                st.GetMin(), // 0
                st.PopReturnMin(), // pop 0 -> min 0
                st.PopReturnMin(), // pop 3 -> min 0
                st.PopReturnMin(), // pop 0 -> min 2
            };
        },
        expected: new[] { 0, 0, 0, 2 });
    }

    private void Test(int num, string desc, Func<int[]> action, int[] expected)
    {
        var sw = Stopwatch.StartNew();
        int[] actual = action();
        sw.Stop();

        bool pass = actual.Length == expected.Length &&
                    actual.Zip(expected, (a, b) => a == b).All(x => x);

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc}");
            Console.WriteLine($"  Expected=[{string.Join(",", expected)}]");
            Console.WriteLine($"  Actual=[{string.Join(",", actual)}]");
        }
    }

    // ------------------------------------------------------------
    // Helper wrapper for testing (not part of the real solution)
    // ------------------------------------------------------------
    private class MinStackInternal
    {
        private readonly MinStack _stack = new();

        public void Push(int x) => _stack.Push(x);
        public void Pop() => _stack.Pop();
        public int GetMin() => _stack.GetMin();

        public int PopReturnMin()
        {
            _stack.Pop();
            return _stack.GetMin();
        }
    }
}
