using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0081_TriggerSE : SetObjectManagerHeroes
    {
        private BoundingSphere sphereBound;

        public override bool TriangleIntersection(Ray r, string[][] modelNames, int miscSettingByte, float initialDistance, out float distance)
        {
            switch (TriggerShape)
            {
                case TriggerSEShape.Sphere:
                    return r.Intersects(ref sphereBound, out distance);
                case TriggerSEShape.Cube:
                    return TriangleIntersection(r, Program.MainForm.renderer.cubeTriangles, Program.MainForm.renderer.cubeVertices, initialDistance, out distance, 0.25f);
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
                case TriggerSEShape.Sphere:
                    sphereBound = new BoundingSphere(Position, Radius);
                    transformMatrix = Matrix.Scaling(Radius * 2);
                    break;
                case TriggerSEShape.Cube:
                    transformMatrix = Matrix.Scaling(ScaleX * 2, ScaleY * 2, ScaleZ * 2);
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
                case TriggerSEShape.Sphere:
                    renderer.DrawSphereTrigger(transformMatrix, isSelected);
                    break;
                case TriggerSEShape.Cube:
                    renderer.DrawCubeTrigger(transformMatrix, isSelected);
                    break;
                default:
                    DrawCube(renderer, isSelected);
                    break;
            }
        }

        public int SE_ID
        {
            get => ReadInt(4);
            set => Write(4, value);
        }

        public enum SE_CALL : byte
        {
            SE_CALL = 0,
            SE_CALL_VP = 1,
            SE_LOOP = 2,
            SE_LOOP_VP = 3,
            SE_SCLOOP = 4,
            SE_LOOP_VP_ = 5
        }

        public SE_CALL CallType
        {
            get => (SE_CALL)ReadByte(8);
            set => Write(8, (byte)value);
        }

        public bool Doppler
        {
            get => ReadByte(9) != 0;
            set => Write(9, value ? 1 : 0);
        }

        public byte Volume
        {
            get => ReadByte(10);
            set => Write(10, value);
        }

        public enum TriggerSEShape : byte
        {
            Sphere = 0,
            Cube = 1,
        }

        public TriggerSEShape TriggerShape
        {
            get => (TriggerSEShape)ReadByte(11);
            set { Write(11, (byte)value); CreateTransformMatrix(Position, Rotation); }
        }
        
        public short Time
        {
            get => ReadShort(12);
            set { Write(12, value); CreateTransformMatrix(Position, Rotation); }
        }

        public short Radius
        {
            get => ReadShort(14);
            set { Write(14, value); CreateTransformMatrix(Position, Rotation); }
        }

        public short ScaleX
        {
            get => ReadShort(14);
            set { Write(14, value); CreateTransformMatrix(Position, Rotation); }
        }

        public short ScaleY
        {
            get => ReadShort(16);
            set { Write(16, value); CreateTransformMatrix(Position, Rotation); }
        }

        public short ScaleZ
        {
            get => ReadShort(18);
            set { Write(18, value); CreateTransformMatrix(Position, Rotation); }
        }
    }
}