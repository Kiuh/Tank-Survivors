using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Common
{
    public enum ModificationPriority
    {
        Low = 0,
        Medium = 1,
        High = 2,
    }

    public class ValueModification<T>
    {
        public Func<T, T> Func { get; set; }
        public ModificationPriority Priority;

        public ValueModification(Func<T, T> func, ModificationPriority priority)
        {
            Func = func;
            Priority = priority;
        }
    }

    [Serializable]
    [InlineProperty(LabelWidth = 120)]
    public class ModifiableValue<T>
    {
        [SerializeField]
        private T sourceValue;

        private List<ValueModification<T>> modifications = new();
        public List<ValueModification<T>> Modifications
        {
            get
            {
                modifications ??= new List<ValueModification<T>>();
                isDirty = true;
                return modifications;
            }
        }
        public T SourceValue
        {
            get => sourceValue;
            set
            {
                isDirty = true;
                sourceValue = value;
            }
        }

        public ModifiableValue()
        {
            SourceValue = default;
            isDirty = true;
        }

        public ModifiableValue(T sourceValue)
        {
            SourceValue = sourceValue;
            isDirty = true;
        }

        private bool isDirty = true;
        private bool notFirstCalculate = false;
        private T cachedValue;

        public T Value => GetModifiedValue();

        public T GetModifiedValue()
        {
            if (!isDirty && notFirstCalculate)
            {
                return cachedValue;
            }

            notFirstCalculate = true;
            T value = SourceValue;
            foreach (
                ValueModification<T> modification in Modifications.OrderBy(x => (int)x.Priority)
            )
            {
                value = modification.Func(value);
            }
            cachedValue = value;
            isDirty = false;
            return value;
        }
    }
}
