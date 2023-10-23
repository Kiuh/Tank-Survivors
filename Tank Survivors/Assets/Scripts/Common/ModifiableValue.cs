using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    public enum ModificationPriority
    {
        Low,
        Medium,
        High,
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

        [SerializeField]
        [InspectorReadOnly]
        private T cachedValue;

        [SerializeField]
        [InspectorReadOnly]
        private bool isDirty = true;

        private List<ValueModification<T>> modifications = new();
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
            if (isDirty)
            {
                T value = SourceValue;
                foreach (
                    ValueModification<T> modification in modifications.OrderBy(x => x.Priority)
                )
                {
                    value = modification.Func(value);
                }
                cachedValue = value;
                isDirty = false;
            }
            return cachedValue;
        }

        public void AddModification(ValueModification<T> modification)
        {
            modifications.Add(modification);
            isDirty = true;
        }

        public void RemoveModification(ValueModification<T> modification)
        {
            _ = modifications.Remove(modification);
            isDirty = true;
        }
    }
}
