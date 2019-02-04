using UnityEngine;

namespace Values
{
    public class LimitedFloat
    {
        //Methods
        //-Initialization
        public LimitedFloat(float inMinimum = 0.0f, float inMaximum = float.MaxValue)
        {
            _minimum = inMinimum;
            _maximum = inMaximum;

            //TODO: Put assert if Max > Min
        }

        public static LimitedFloat CreateWithMaximumOnly(float inMaximum, bool inZeroBased = true) {
            return new LimitedFloat(inZeroBased ? 0.0f : float.MinValue, inMaximum);
        }

        public static LimitedFloat CreateWithMinimumOnly(float inMinimum, bool inZeroBased = true) {
            return new LimitedFloat(inMinimum, inZeroBased ? 0.0f : float.MaxValue);
        }

        //-Accessors
        public float getMinimum() { return _minimum; }
        public float getMaximum() { return _maximum; }
        public float getRange() { return getMaximum() - getMinimum(); }

        public float getValue() { return _value; }
        public float setValue(float inNewValue) { return _value = Mathf.Clamp(inNewValue, _minimum, _maximum); }
        public float changeValue(float inValueDelta)
        {
            float theOldValue = getValue();
            float theNewValue = setValue(theOldValue + inValueDelta);
            return theNewValue - theOldValue;
        }

        public float getValueFromMinimum() { return getValue() - getMinimum(); }
        public float getValuePercentFromMinimum() { return getValueFromMinimum()/getRange(); }

        public float getValueFromMaximum() { return getMaximum() - getValue(); }
        public float getValuePercenFromMaximum() { return getValueFromMaximum()/getRange(); }

        //Fields
        private float _value;
        private float _minimum;
        private float _maximum;
    }
}


