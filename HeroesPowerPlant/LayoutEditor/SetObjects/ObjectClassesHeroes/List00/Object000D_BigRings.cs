using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000D_BigRings : SetObjectHeroes
    {
        public enum EType : short
        {
            Speed = 0,
            FlyA = 1,
            FlyB = 2,
            PowerS = 3,
            PowerL = 4
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = DefaultTransformMatrix(MathUtil.Pi);
            CreateBoundingBox();
        }

        public EType RingsType { get; set; }
        [Description("In frames")]
        public short AdditionalControlTime { get; set; }
        [Description("Defaults to 5.0")]
        public float Speed { get; set; }
        public float Offset { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            RingsType = (EType)reader.ReadInt16();
            AdditionalControlTime = reader.ReadInt16();
            Speed = reader.ReadSingle();
            Offset = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((short)RingsType);
            writer.Write(AdditionalControlTime);
            writer.Write(Speed);
            writer.Write(Offset);
        }
    }
}