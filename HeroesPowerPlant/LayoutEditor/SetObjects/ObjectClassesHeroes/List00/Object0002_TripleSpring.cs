using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0002_TripleSpring : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f, 1f, 1f) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        [Description("Defaults to 5.0")]
        public float Power { get; set; }
        [Description("In frames")]
        public float Scale { get; set; }
        public short NoControlTime { get; set; }
        public EHeroesItem Item { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Power = reader.ReadSingle();
            Scale = reader.ReadSingle();
            NoControlTime = reader.ReadInt16();
            Item = (EHeroesItem)reader.ReadByte();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Power);
            writer.Write(Scale);
            writer.Write(NoControlTime);
            writer.Write((byte)Item);
        }
    }
}
