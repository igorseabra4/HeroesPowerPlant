using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0181_SeaPole : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        private const string flagModelName = "S01_PN_HATA0.DFF";

        public override void Draw(SharpRenderer renderer)
        {
            base.Draw(renderer);

            if (ObjectType < 8 && renderer.dffRenderer.DFFModels.ContainsKey(flagModelName))
            {
                SetRendererStates(renderer);

                renderData.worldViewProjection = Matrix.Scaling(Scale)
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.RotationY(MathUtil.DegreesToRadians(FlagAngle))
                * Matrix.Translation(Position) * renderer.viewProjection;

                renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);

                renderer.dffRenderer.DFFModels[flagModelName].Render(renderer.Device);
            }
        }

        [MiscSetting, Description("Types range from 0 to 15. 8 to 15 are the same as 0 to 7 but without the flag itself.")]
        public byte ObjectType { get; set; }
        [MiscSetting]
        public float FlagAngle { get; set; }
        [MiscSetting]
        public float Scale { get; set; }
    }
}
