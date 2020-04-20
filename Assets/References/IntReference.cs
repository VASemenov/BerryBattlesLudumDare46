using UnityEngine;

namespace References
{
    [CreateAssetMenu(menuName = "References/Int")]
    public class IntReference : ScriptableObject
    {

        public int constantValue;
        public bool useConstant = false;
        public int defaultValue;
        public bool setConstAsDefault = true;
        public int value;
    
    
        
        
        private void OnEnable()
        {
            if (setConstAsDefault) defaultValue = constantValue;
            value = defaultValue;
        }

        public int Value
        {
            get => useConstant ? constantValue : this.value;
            set => this.value = value > 0 ? value : 0;
        }
    }
}
