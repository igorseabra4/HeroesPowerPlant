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

        public override void Draw(SharpRenderer renderer)
        {
            base.Draw(renderer);

            string flagModelName = "S01_PN_HATA0.DFF";

            if (FlagType < 8 && renderer.dffRenderer.DFFModels.ContainsKey(flagModelName))
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

        [Description("Types range from 0 to 15. 8 to 15 are the same as 0 to 7 but without the flag itself.")]
        public byte FlagType
        {
            get => ReadByte(4);
            set => Write(4, value);
        }

        public float FlagAngle
        {
            get => ReadWriteCommon.BAMStoDegrees(ReadShort(10));
            set => Write(10, (short)ReadWriteCommon.DegreesToBAMS(value));
        }

        public float Scale
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }
    }
}
