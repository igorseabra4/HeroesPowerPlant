using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum MiscSettingUnderlyingType
    {
        Null,
        Int,
        Short,
        Byte,
        Float
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class MiscSettingAttribute : Attribute
    {
        public static MiscSettingUnderlyingType GetUnderlyingType(Type propertyType, MiscSettingUnderlyingType underlyingType)
        {
            if (underlyingType == MiscSettingUnderlyingType.Null)
            {
                if (propertyType.IsEnum)
                    propertyType = Enum.GetUnderlyingType(propertyType);

                if (propertyType.Equals(typeof(bool)))
                    throw new Exception();
                if (propertyType.Equals(typeof(int)))
                    return MiscSettingUnderlyingType.Int;
                if (propertyType.Equals(typeof(float)))
                    return MiscSettingUnderlyingType.Float;
                if (propertyType.Equals(typeof(short)))
                    return MiscSettingUnderlyingType.Short;
                if (propertyType.Equals(typeof(byte)))
                    return MiscSettingUnderlyingType.Byte;
            }

            return underlyingType;
        }

        public int Order { get; set; }
        public MiscSettingUnderlyingType UnderlyingType { get; set; }
        public int PadAfter { get; set; }

        public MiscSettingAttribute(int order = -1, MiscSettingUnderlyingType underlyingType = MiscSettingUnderlyingType.Null, int padAfter = 0)
        {
            Order = order;
            UnderlyingType = underlyingType;
            PadAfter = padAfter;
        }
    }
}