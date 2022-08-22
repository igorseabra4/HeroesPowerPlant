using HeroesPowerPlant.Shared.Utilities;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1181_Celestial : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale + 1f) * DefaultTransformMatrix();

            CreateBoundingBox();
        }

        public override void Draw(SharpRenderer renderer)
        {
            if (isSelected)
                renderData.Color = renderer.selectedObjectColor;
            else
                renderData.Color = Vector4.One;

            renderer.Device.SetCullModeNone();
            renderer.Device.SetDepthStateNone();
            renderer.Device.SetBlendStateAdditive();
            renderer.Device.ApplyRasterState();
            renderer.Device.UpdateAllStates();
            base.Draw(renderer);
        }

        public int CelestialType { get; set; }
        public float SpeedX { get; set; }
        public float SpeedY { get; set; }
        public float SpeedZ { get; set; }
        public float Scale { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            CelestialType = reader.ReadInt32();
            SpeedX = reader.ReadSingle();
            SpeedY = reader.ReadSingle();
            SpeedZ = reader.ReadSingle();
            Scale = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(CelestialType);
            writer.Write(SpeedX);
            writer.Write(SpeedY);
            writer.Write(SpeedZ);
            writer.Write(Scale);
        }
    }
}
