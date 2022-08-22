using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0001_Spring : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = DefaultTransformMatrix(MathUtil.Pi);
            CreateBoundingBox();
        }

        [Description("Defaults to 5.0")]
        public float Power { get; set; }
        [Description("In frames")]
        public short NoControlTime { get; set; }
        public float GuideLine { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Power = reader.ReadSingle();
            NoControlTime = reader.ReadInt16();
            reader.ReadInt16();
            GuideLine = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Power);
            writer.Write(NoControlTime);
            writer.Write((short)0);
            writer.Write(GuideLine);
        }
    }
}
