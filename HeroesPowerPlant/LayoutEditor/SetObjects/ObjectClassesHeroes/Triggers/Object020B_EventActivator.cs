using SharpDX;
using System.Collections.Generic;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum EventActivatorType : byte
    {
        NotInUse = 0,
        NotInUse2 = 1,
        Elevator0402 = 2,
        EnergyUp0412 = 3,
        Shutter0410 = 4,
        BallGLSOn0480 = 5,
        BallGLSOff0480 = 6,
        SenkanMov = 7,
        Hakai1320 = 8,
        FallAshiba1400 = 9
    }

    public class Object020B_EventActivator : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ)
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.Translation(Position);

            CreateBoundingBox();
        }

        protected override void CreateBoundingBox()
        {
            List<Vector3> list = new List<Vector3>();
            list.AddRange(SharpRenderer.cubeVertices);
            for (int i = 0; i < list.Count; i++)
                list[i] = (Vector3)Vector3.Transform(list[i], transformMatrix);

            boundingBox = BoundingBox.FromPoints(list.ToArray());
        }

        public override void Draw(SharpRenderer renderer)
        {
            renderer.DrawCubeTrigger(transformMatrix, isSelected);
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            return TriangleIntersection(r, SharpRenderer.cubeTriangles, SharpRenderer.cubeVertices, initialDistance, out distance);
        }
        
        public float ScaleX
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float ScaleY
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float ScaleZ
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public EventActivatorType EventActivatorType
        {
            get => (EventActivatorType)ReadByte(16);
            set => Write(16, (byte)value);
        }

        public bool OnlyLeader
        {
            get => ReadByte(17) != 0;
            set => Write(17, value ? (byte)1 : (byte)0);
        }
    }
}
