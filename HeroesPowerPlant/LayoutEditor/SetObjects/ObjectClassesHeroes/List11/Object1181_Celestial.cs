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

        public int CelestialType
        {
            get => ReadInt(4);
            set => Write(4, value);
        }

        public float SpeedX
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float SpeedY
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float SpeedZ
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float Scale
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }
    }
}
