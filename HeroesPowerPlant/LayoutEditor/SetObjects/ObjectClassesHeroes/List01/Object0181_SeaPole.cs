using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0181_SeaPole : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            base.CreateTransformMatrix();
            transformMatrix = Matrix.Scaling(Scale) * transformMatrix;
        }

        public override void Draw(SharpRenderer renderer)
        {
            base.Draw(renderer);

            string flagModelName = "S01_PN_HATA0.DFF";

            if (FlagType < 8 && renderer.dffRenderer.DFFModels.ContainsKey(flagModelName))
            {
                renderData.worldViewProjection = Matrix.Scaling(Scale)
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.RotationY(MathUtil.DegreesToRadians(FlagAngle))
                * Matrix.Translation(Position) * renderer.viewProjection;

                renderData.Color = isSelected ? renderer.selectedObjectColor : Vector4.One;

                renderer.Device.SetFillModeDefault();
                renderer.Device.SetCullModeDefault();
                renderer.Device.SetBlendStateAlphaBlend();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();

                renderer.Device.UpdateData(renderer.tintedBuffer, renderData);
                renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.tintedBuffer);
                renderer.tintedShader.Apply();

                renderer.dffRenderer.DFFModels[flagModelName].Render(renderer.Device);
            }
        }
        
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
