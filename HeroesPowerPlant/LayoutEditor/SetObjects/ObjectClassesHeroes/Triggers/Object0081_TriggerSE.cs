using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System.Collections.Generic;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0081_TriggerSE : SetObjectHeroes
    {
        public enum SE_CALL : byte
        {
            SE_CALL = 0,
            SE_CALL_VP = 1,
            SE_LOOP = 2,
            SE_LOOP_VP = 3,
            SE_SCLOOP = 4,
            SE_LOOP_VP_ = 5
        }

        public enum EShape : byte
        {
            Sphere = 0,
            Cube = 1,
        }

        private BoundingSphere sphereBound;

        public override void CreateTransformMatrix()
        {
            switch (Shape)
            {
                case EShape.Sphere:
                    sphereBound = new BoundingSphere(Position, Radius);
                    transformMatrix = Matrix.Scaling(Radius * 2);
                    break;
                case EShape.Cube:
                    transformMatrix = Matrix.Scaling(ScaleX * 2, ScaleY * 2, ScaleZ * 2);
                    break;
                default:
                    base.CreateTransformMatrix();
                    return;
            }

            transformMatrix *= DefaultTransformMatrix();

            CreateBoundingBox();
        }

        protected override void CreateBoundingBox()
        {
            List<Vector3> list = new List<Vector3>();

            switch (Shape)
            {
                case EShape.Sphere:
                    list.AddRange(SharpRenderer.sphereVertices);
                    break;
                case EShape.Cube:
                    list.AddRange(SharpRenderer.cubeVertices);
                    break;
                default:
                    base.CreateBoundingBox();
                    return;
            }

            for (int i = 0; i < list.Count; i++)
                list[i] = (Vector3)Vector3.Transform(list[i], transformMatrix);

            boundingBox = BoundingBox.FromPoints(list.ToArray());
        }

        public override void Draw(SharpRenderer renderer)
        {
            switch (Shape)
            {
                case EShape.Sphere:
                    renderer.DrawSphereTrigger(transformMatrix, isSelected);
                    break;
                case EShape.Cube:
                    renderer.DrawCubeTrigger(transformMatrix, isSelected);
                    break;
                default:
                    DrawCube(renderer);
                    break;
            }
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            switch (Shape)
            {
                case EShape.Sphere:
                    return r.Intersects(ref sphereBound, out distance);
                case EShape.Cube:
                    return TriangleIntersection(r, SharpRenderer.cubeTriangles, SharpRenderer.cubeVertices, initialDistance, out distance);
                default:
                    return base.TriangleIntersection(r, initialDistance, out distance);
            }
        }

        public int SE_ID { get; set; }
        public SE_CALL CallType { get; set; }
        public bool Doppler { get; set; }
        public byte Volume { get; set; }
        public EShape Shape { get; set; }
        public short Time { get; set; }
        public short Radius { get; set; }
        public short ScaleX { get; set; }
        public short ScaleY { get; set; }
        public short ScaleZ { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            SE_ID = reader.ReadInt32();
            CallType = (SE_CALL)reader.ReadByte();
            Doppler = reader.ReadByteBool();
            Volume = reader.ReadByte();
            Shape = (EShape)reader.ReadByte();
            Time = reader.ReadInt16();
            Radius = reader.ReadInt16();
            ScaleX = reader.ReadInt16();
            ScaleY = reader.ReadInt16();
            ScaleZ = reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(SE_ID);
            writer.Write((byte)CallType);
            writer.Write((byte)(Doppler ? 1 : 0));
            writer.Write(Volume);
            writer.Write((byte)Shape);
            writer.Write(Time);
            writer.Write(Radius);
            writer.Write(ScaleX);
            writer.Write(ScaleY);
            writer.Write(ScaleZ);
        }
    }
}