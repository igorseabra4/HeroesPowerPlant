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

        [MiscSetting]
        public int CelestialType { get; set; }
        [MiscSetting]
        public float SpeedX { get; set; }
        [MiscSetting]
        public float SpeedY { get; set; }
        [MiscSetting]
        public float SpeedZ { get; set; }
        [MiscSetting]
        public float Scale { get; set; }
    }
}
