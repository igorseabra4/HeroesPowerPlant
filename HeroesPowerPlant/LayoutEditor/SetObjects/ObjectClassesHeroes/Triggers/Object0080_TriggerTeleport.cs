using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0080_TriggerTeleport : SetObjectManagerHeroes
    {
        private BoundingSphere sphereBound;

        public override bool TriangleIntersection(Ray r, string[][] modelNames, int miscSettingByte, float initialDistance, out float distance)
        {
            return r.Intersects(ref sphereBound, out distance);
        }

        public override BoundingBox CreateBoundingBox(string[][] modelNames, int miscSettingByte)
        {
            return new BoundingBox(-Vector3.One / 2, Vector3.One / 2);
        }

        private Matrix destinationMatrix;

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            sphereBound = new BoundingSphere(Position, Radius);

            destinationMatrix = Matrix.Translation(XDestination * 2, YDestination * 2, ZDestination * 2);

            transformMatrix = Matrix.Scaling(Radius)
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.Translation(Position);
        }

        public override void Draw(SharpRenderer renderer, string[][] modelNames, int miscSettingByte, bool isSelected)
        {
            renderer.DrawSphereTrigger(transformMatrix, isSelected);

            if (isSelected)
            {
                renderData.worldViewProjection = Matrix.Scaling(5) * destinationMatrix * renderer.viewProjection;

                renderData.Color = renderer.selectedColor;

                renderer.Device.SetFillModeDefault();
                renderer.Device.SetCullModeNone();
                renderer.Device.SetBlendStateAlphaBlend();
                renderer.Device.ApplyRasterState();
                renderer.Device.UpdateAllStates();

                renderer.Device.UpdateData(renderer.basicBuffer, renderData);
                renderer.Device.DeviceContext.VertexShader.SetConstantBuffer(0, renderer.basicBuffer);
                renderer.basicShader.Apply();

                renderer.Cube.Draw(renderer.Device);
            }
        }

        public float Radius
        {
            get => ReadFloat(4);
            set { Write(4, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float XDestination
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float YDestination
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float ZDestination
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }
    }
}