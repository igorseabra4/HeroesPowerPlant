using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0181_SeaPole : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            if (modelNames == null || modelNames.Length == 0 || Type % 8 >= modelNames.Length || !Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(modelNames[Type % 8]))
                return BoundingBox.FromPoints(Program.MainForm.renderer.cubeVertices.ToArray());
            
            return BoundingBox.FromPoints(Program.MainForm.renderer.dffRenderer.DFFModels[modelNames[Type % 8]].vertexListG.ToArray());
        }

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix = Matrix.Scaling(Scale)
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.Translation(Position);
        }

        public override void Draw(SharpRenderer renderer, string[] modelNames, bool isSelected)
        {
            if (Type < 8 && Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(modelNames[8]))
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

                Program.MainForm.renderer.dffRenderer.DFFModels[modelNames[8]].Render(renderer.Device);
            }

            if (Type < 16)
                Draw(renderer, modelNames[Type % 8], isSelected);
            else
                DrawCube(renderer, isSelected);
        }

        public byte Type
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
            set { Write(12, value); CreateTransformMatrix(Position, Rotation); }
        }
    }
}
