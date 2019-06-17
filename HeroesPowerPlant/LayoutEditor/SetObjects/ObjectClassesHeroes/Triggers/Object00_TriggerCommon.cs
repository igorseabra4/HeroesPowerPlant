using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum TriggerCommonShape : int
    {
        Sphere = 0,
        Cylinder = 1,
        Cube = 2,
        CylinderXZ = 3,
    }

    public class Object00_TriggerCommon : SetObjectManagerHeroes
    {
        private BoundingSphere sphereBound;

        public override bool TriangleIntersection(Ray r, string[][] modelNames, int miscSettingByte, float initialDistance, out float distance)
        {
            switch (TriggerShape)
            {
                case TriggerCommonShape.Sphere:
                    return r.Intersects(ref sphereBound, out distance);
                case TriggerCommonShape.Cube:
                    return TriangleIntersection(r, Program.MainForm.renderer.cubeTriangles, Program.MainForm.renderer.cubeVertices, initialDistance, out distance, 0.25f);
                case TriggerCommonShape.Cylinder:
                case TriggerCommonShape.CylinderXZ:
                    return TriangleIntersection(r, Program.MainForm.renderer.cylinderTriangles, Program.MainForm.renderer.cylinderVertices, initialDistance, out distance);
                default:
                    return base.TriangleIntersection(r, modelNames, miscSettingByte, initialDistance, out distance);
            }
        }

        public override BoundingBox CreateBoundingBox(string[][] modelNames, int miscSettingByte)
        {
            return new BoundingBox(-Vector3.One / 2, Vector3.One / 2);
        }

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            switch (TriggerShape)
            {
                case TriggerCommonShape.Sphere:
                    sphereBound = new BoundingSphere(Position, Radius_ScaleX);
                    transformMatrix = Matrix.Scaling(Radius_ScaleX * 2);
                    break;
                case TriggerCommonShape.Cube:
                    transformMatrix = Matrix.Scaling(Radius_ScaleX * 2, Height_ScaleY * 2, ScaleZ * 2);
                    break;
                case TriggerCommonShape.Cylinder:
                case TriggerCommonShape.CylinderXZ:
                    transformMatrix = Matrix.Scaling(Radius_ScaleX * 2, Height_ScaleY * 2, Radius_ScaleX * 2);
                    break;
            }

            transformMatrix = transformMatrix
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.Translation(Position);
        }

        public override void Draw(SharpRenderer renderer, string[][] modelNames, int miscSettingByte, bool isSelected)
        {
            switch (TriggerShape)
            {
                case TriggerCommonShape.Sphere:
                    renderer.DrawSphereTrigger(transformMatrix, isSelected);
                    break;
                case TriggerCommonShape.Cube:
                    renderer.DrawCubeTrigger(transformMatrix, isSelected);
                    break;
                case TriggerCommonShape.Cylinder:
                case TriggerCommonShape.CylinderXZ:
                    renderer.DrawCylinderTrigger(transformMatrix, isSelected);
                    break;
                default:
                    DrawCube(renderer, isSelected);
                    break;
            }
        }

        public TriggerCommonShape TriggerShape
        {
            get => (TriggerCommonShape)ReadInt(4);
            set { Write(4, (int)value); CreateTransformMatrix(Position, Rotation); }
        }

        public float Radius_ScaleX
        {
            get => ReadFloat(8);
            set { Write(8, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float Height_ScaleY
        {
            get => ReadFloat(12);
            set { Write(12, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float ScaleZ
        {
            get => ReadFloat(16);
            set { Write(16, value); CreateTransformMatrix(Position, Rotation); }
        }
    }
}