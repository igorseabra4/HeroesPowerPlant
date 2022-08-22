using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000F_DashRamp : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = DefaultTransformMatrix(MathUtil.Pi);
            CreateBoundingBox();
        }

        [Description("Defaults to 5.0")]
        public float SpeedHorizontal { get; set; }
        [Description("Defaults to 5.0")]
        public float SpeedVertical { get; set; }
        [Description("In frames")]
        public short NoControlTime { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            SpeedHorizontal = reader.ReadSingle();
            SpeedVertical = reader.ReadSingle();
            NoControlTime = reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(SpeedHorizontal);
            writer.Write(SpeedVertical);
            writer.Write(NoControlTime);
        }
    }
}