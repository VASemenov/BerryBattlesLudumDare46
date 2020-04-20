using UnityEngine;

namespace References
{
    [CreateAssetMenu(menuName = "References/Float")]
    public class FloatReference : ScriptableObject
    {
        public float constantValue;
        public bool useConstant = false;
        public float defaultValue;
        public bool setConstAsDefault = true;
        private float _value;
    
    
        private void OnEnable()
        {
            if (setConstAsDefault) defaultValue = constantValue;
            this._value = defaultValue;
        }

        public float Value
        {
            get => useConstant ? constantValue : this._value;
            set => this._value = value;
        }
    }
}
