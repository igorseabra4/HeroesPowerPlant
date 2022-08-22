using HeroesPowerPlant.Shared.Utilities;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0015_SpikeBall : SetObjectHeroes
    {
        public enum ESpikeBallType : int
        {
            SingleBall = 0,
            DoubleBall = 1
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        public ESpikeBallType SpikeBallType { get; set; }
        public float RotateSpeed { get; set; }
        public float Scale { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            SpikeBallType = (ESpikeBallType)reader.ReadInt32();
            RotateSpeed = reader.ReadSingle();
            Scale = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((int)SpikeBallType);
            writer.Write(RotateSpeed);
            writer.Write(Scale);
        }
    }
}