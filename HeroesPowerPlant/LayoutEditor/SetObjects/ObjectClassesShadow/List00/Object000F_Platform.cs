using System;
using System.ComponentModel;
using System.IO;

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

        public EPlatformType PlatformType { get; set; }
        [Description("Path Types require a nearby spline w/ Setting3=8, Setting4=1")]
        public EMovementType MovementType { get; set; }

        [Description("Time it takes to move to translation position")]
        public float TravelTime_Float
        {
            get => BitConverter.ToSingle(BitConverter.GetBytes(TravelTime_Int), 0);
            set => TravelTime_Int = BitConverter.ToInt32(BitConverter.GetBytes(value), 0);
        }

        [Description("Same field as above, but sometimes an Int for Linear type\nUsually a float though.")]
        public int TravelTime_Int { get; set; }

        public float WaitTime { get; set; }
        public float Float10 { get; set; }
        public float Float14 { get; set; }

        public float SplineID
        {
            get => TranslationX;
            set => TranslationX = value;
        }

        public float TranslationX { get; set; }
        public float TranslationY { get; set; }
        public float TranslationZ { get; set; }
        public float Float24 { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            PlatformType = (EPlatformType)reader.ReadInt32();
            MovementType = (EMovementType)reader.ReadInt32();
            TravelTime_Int = reader.ReadInt32();
            WaitTime = reader.ReadSingle();
            Float10 = reader.ReadSingle();
            Float14 = reader.ReadSingle();
            TranslationX = reader.ReadSingle();
            TranslationY = reader.ReadSingle();
            TranslationZ = reader.ReadSingle();
            Float24 = reader.ReadSingle();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)PlatformType);
            writer.Write((int)MovementType);
            writer.Write(TravelTime_Int);
            writer.Write(WaitTime);
            writer.Write(Float10);
            writer.Write(Float14);
            writer.Write(TranslationX);
            writer.Write(TranslationY);
            writer.Write(TranslationZ);
            writer.Write(Float24);
        }
    }
}
