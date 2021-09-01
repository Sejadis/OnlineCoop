using System;
using Shared.Abilities;

namespace Shared
{
    [Serializable]
    public class Condition
    {
        public float CompareValue;
        public ConditionComparer Comparer;
        public TargetEffectConditionType conditionTypeValue;

        public static bool Compare(float value1, float value2, ConditionComparer comparer)
        {
            return comparer switch
            {
                ConditionComparer.GreaterThan => value1 > value2,
                ConditionComparer.SmallerThan => value1 < value2,
                ConditionComparer.Equal => Math.Abs(value1 - value2) < 0.0001f,
                _ => throw new ArgumentOutOfRangeException(nameof(comparer), comparer, null)
            };
        }

        public bool Evaluate(float values)
        {
            var value2 = conditionTypeValue switch
            {
                TargetEffectConditionType.HitCount => values,
                _ => throw new ArgumentOutOfRangeException()
            };

            return Compare(CompareValue, value2, Comparer);
        }
    }
}