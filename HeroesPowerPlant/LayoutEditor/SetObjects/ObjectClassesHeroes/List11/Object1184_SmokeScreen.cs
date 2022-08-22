using HeroesPowerPlant.Shared.Utilities;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1184_SmokeScreen : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = IsUpsideDown ? Matrix.RotationY(MathUtil.Pi) : Matrix.Identity *
                DefaultTransformMatrix();

            CreateBoundingBox();
        }

        public int ObjectType { get; set; }
        public float Speed { get; set; }
        public bool IsUpsideDown { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            ObjectType = reader.ReadInt32();
            Speed = reader.ReadSingle();
            IsUpsideDown = reader.ReadByteBool();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(ObjectType);
            writer.Write(Speed);
            writer.Write((byte)(IsUpsideDown ? 1 :0));
        }
    }
}
