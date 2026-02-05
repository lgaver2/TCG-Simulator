using System;
using UnityEngine;

namespace StaticUtils
{
    public static class MathUtils
    {
        public static bool Compare<T>(T a, T b, ComparisonOperator op) where T : IComparable<T>
        {
            int result = a.CompareTo(b);
            switch (op)
            {
                case ComparisonOperator.Equals: return result == 0;
                case ComparisonOperator.NotEquals: return result != 0;
                case ComparisonOperator.GreaterOrEqual: return result >= 0;
                case ComparisonOperator.LessOrEqual: return result <= 0;
                case ComparisonOperator.GreaterThan: return result > 0;
                case ComparisonOperator.LessThan: return result < 0;
            }

            return false;
        }
    }

    public enum ComparisonOperator
    {
        [InspectorName("Equal")] Equals,

        [InspectorName("Not Equal")] NotEquals,

        [InspectorName("Greater")] GreaterThan,

        [InspectorName("Less")] LessThan,

        [InspectorName("Greater or Equal")] GreaterOrEqual,

        [InspectorName("Less or Equal")] LessOrEqual
    }
}