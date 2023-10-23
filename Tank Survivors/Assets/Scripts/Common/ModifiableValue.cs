using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ModifiableValue<T>
    {
        [SerializeField]
        private T sourceValue;

        private List<ValueModification<T>> modifications = new();
        public List<ValueModification<T>> Modifications => modifications;
        public T SourceValue
        {
            get => sourceValue;
            set => sourceValue = value;
        }

        public ModifiableValue(T sourceValue)
        {
            SourceValue = sourceValue;
        }

        public T GetModifiedValue()
        {
            T value = SourceValue;
            foreach (ValueModification<T> modification in modifications.OrderBy(x => x.Priority))
            {
                value = modification.Func(value);
            }
            return value;
        }
    }
}
