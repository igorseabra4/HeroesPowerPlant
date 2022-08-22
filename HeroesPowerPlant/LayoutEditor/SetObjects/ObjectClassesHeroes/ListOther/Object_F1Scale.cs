using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_F1Scale : SetObjectHeroes
    {
        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(Scale) * DefaultTransformMatrix();

            CreateBoundingBox();
        }

        public float Scale { get;set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Scale = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Scale);
        }
    }
}