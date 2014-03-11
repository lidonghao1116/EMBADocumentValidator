﻿using System.Xml;

namespace EMBA.DocumentValidator
{
    public class IntegerValidator : IFieldValidator
    {
        private int mMinValue;
        private int mMaxValue;
        private bool mMinInclusive;
        private bool mMaxInclusive;

        public IntegerValidator(XmlElement XmlNode)
        {
            XmlHelper xml = new XmlHelper(XmlNode);

            mMinValue = xml.GetInteger("MinValue", 0);
            mMaxValue = xml.GetInteger("MaxValue", int.MaxValue);

            mMinInclusive = xml.GetBoolean("MinValue/@Inclusive", true);
            mMaxInclusive = xml.GetBoolean("MaxValue/@Inclusive", true);
        }

        public bool Validate(string Value)
        {
            bool result = false;

            int val;

            if (!int.TryParse(Value, out val)) return result;

            if (mMinInclusive)
            {
                if (val < mMinValue) return result;
            }
            else
            {
                if (val <= mMinValue) return result;
            }

            if (mMaxInclusive)
            {
                if (val > mMaxValue) return result;
            }
            else
            {
                if (val >= mMaxValue) return result;
            }

            return true;
        }

        public string Correct(string Value)
        {
            return "";
        }

        public string ToString(string Description)
        {
            Description = Description.Replace("%MaxValue", mMaxValue.ToString());
            Description = Description.Replace("%MinValue", mMinValue.ToString());

            return Description;
        }
    }
}