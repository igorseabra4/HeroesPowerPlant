using System;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000F_Platform : SetObjectShadow
    {
        public enum EPlatformType
        {
            Type0,
            Type1,
            Type2,
            Type3
        }

        public enum EMovementType
        {
            Linear = 0, //Normal
            Path = 1, //Path
            LoopPath = 2, //Loop Path
            OneWayTranslationOnLinkID = 3, //One Way
            Lerp = 4, // Normal (sync)
            PathSync = 5, // Path (sync)
            Slerp = 6 // Normal (pause)
        }

        // FootingMovable(type, pause(sec), InitPos(0.0-1.0), pauseDamage)

        [MiscSetting(1)]
        public EPlatformType PlatformType { get; set; }
        [MiscSetting(2), Description("Path Types require a nearby spline w/ Setting3=8, Setting4=1")]
        public EMovementType MovementType { get; set; }

        [Description("Time it takes to move to translation position")]
        public float TravelTime_Float
        {
            get => BitConverter.ToSingle(BitConverter.GetBytes(TravelTime_Int), 0);
            set => TravelTime_Int = BitConverter.ToInt32(BitConverter.GetBytes(value), 0);
        }

        [MiscSetting(3), Description("Same field as above, but sometimes an Int for Linear type\nUsually a float though.")]
        public int TravelTime_Int { get; set; }

        [MiscSetting(4)]
        public float WaitTime { get; set; }
        [MiscSetting(5)]
        public float Float10 { get; set; }
        [MiscSetting(6)]
        public float Float14 { get; set; }

        public float SplineID
        {
            get => TranslationX;
            set => TranslationX = value;
        }

        [MiscSetting(7)]
        public float TranslationX { get; set; }
        [MiscSetting(8)]
        public float TranslationY { get; set; }
        [MiscSetting(9)]
        public float TranslationZ { get; set; }
        [MiscSetting(10)]
        public float Float24 { get; set; }
    }
}
