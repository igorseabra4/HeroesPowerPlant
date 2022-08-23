using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System.Collections.Generic;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0980_Butterfly : SetObjectHeroes
    {
        public override bool IsTrigger() => true;

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(AreaX, AreaY, AreaZ) * DefaultTransformMatrix();
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

        public float AreaX { get; set; }
        public float AreaY { get; set; }
        public float AreaZ { get; set; }
        public int Number { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            AreaX = reader.ReadSingle();
            AreaY = reader.ReadSingle();
            AreaZ = reader.ReadSingle();
            Number = reader.ReadInt32();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(AreaX);
            writer.Write(AreaY);
            writer.Write(AreaZ);
            writer.Write(Number);
        }
    }
}