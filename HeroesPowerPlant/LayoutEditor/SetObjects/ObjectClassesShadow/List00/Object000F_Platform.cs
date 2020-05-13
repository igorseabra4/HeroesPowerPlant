using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000F_Platform : SetObjectShadow {
        // FootingMovable(type, pause(sec), InitPos(0.0-1.0), pauseDamage)

        public PlatformBlockType PlatformType {
            get => (PlatformBlockType)ReadInt(0);
            set => Write(0, (int)value);
        }

        [Description("Path Types require a nearby spline w/ Setting3=8, Setting4=1\nTranslationX becomes splineID")]
        public PlatformMoveType MovementType {
            get => (PlatformMoveType)ReadInt(4);
            set => Write(4, (int)value);
        }

        [Description("Time it takes to move to translation position")]
        public float TravelTime_Float {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("Same field as above, but sometimes an Int for Linear type\nUsually a float though.")]
        public int TravelTime_Int {
            get => ReadInt(8);
            set => Write(8, value);
        }

        public float WaitTime { //pause(sec)
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float float_10 {
            get => ReadFloat(0x10);
            set => Write(0x10, value);
        }

        public float float_14 {
            get => ReadFloat(0x14);
            set => Write(0x14, value);
        }

        [Description("For Path Types this is splineID, as a float")]
        public float TranslationX_OR_SplineID {
            get => ReadFloat(0x18);
            set => Write(0x18, value);
        }

        public float TranslationY {
            get => ReadFloat(0x1C);
            set => Write(0x1C, value);
        }

        public float TranslationZ {
            get => ReadFloat(0x20);
            set => Write(0x20, value);
        }

        public float float_24 {
            get => ReadFloat(0x24);
            set => Write(0x24, value);
        }
    }

    public enum PlatformBlockType {
        Type0,
        Type1,
        Type2,
        Type3
    }

    public enum PlatformMoveType {
        Linear=0, //Normal
        Path=1, //Path
        LoopPath=2, //Loop Path
        OneWayTranslationOnLinkID=3, //One Way
        Lerp=4, // Normal (sync)
        PathSync=5, // Path (sync)
        Slerp=6 // Normal (pause)
    }
}
