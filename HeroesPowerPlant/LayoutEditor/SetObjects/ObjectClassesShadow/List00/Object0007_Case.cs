using SharpDX;
using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0007_Case : SetObjectShadow
    {
        public enum ECaseType
        {
            BlackArms,
            GUN
        }

        public ECaseType CaseType { get; set; }
        public float ScaleX { get; set; }
        public float ScaleY { get; set; }
        public float ScaleZ { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            CaseType = (ECaseType)reader.ReadInt32();
            ScaleX = reader.ReadSingle();
            ScaleY = reader.ReadSingle();
            ScaleZ = reader.ReadSingle();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)CaseType);
            writer.Write(ScaleX);
            writer.Write(ScaleY);
            writer.Write(ScaleZ);
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ) *
                DefaultTransformMatrix();
            CreateBoundingBox();
        }
    }
}

