using System;
using UnityEngine;

namespace Shared.Settings
{
    public abstract class Setting<T> : ScriptableObject where T: notnull
    {
        [SerializeField]
        protected T _value;
        [SerializeField]
        protected string _name;
        [SerializeField,TextArea]
        protected string _hint;

        public T Value
        {
            get => _value;
            set
            {
                var old = _value;
                _value = value;
                OnValueChanged?.Invoke(old, _value);
            }
        }

        public string Name => _name;
        public string Hint => _hint;

        public Action<T, T> OnValueChanged;

    }
}