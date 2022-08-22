using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object00_Weight : SetObjectHeroes
    {
        public enum EWeightType : byte
        {
            Repeat = 0,
            Shadow = 1,
            Laser = 2,
            RepeatSwitch = 3,
            ShadowSwitch = 4,
            LaserSwitch = 5
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ) * DefaultTransformMatrix();
            CreateBoundingBox();
        }

        public EWeightType WeightType { get; set; }
        public byte LinkID { get;set; }
        public short Height { get; set; }
        public float ScaleX { get; set; }
        public float ScaleY { get; set; }
        public float ScaleZ { get; set; }
        [Description("In frames")]
        public short UpWaitTime { get; set; }
        [Description("In frames")]
        public short DownWaitTime { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            WeightType = (EWeightType)reader.ReadByte();
            LinkID = reader.ReadByte();
            Height = reader.ReadInt16();
            ScaleX = reader.ReadSingle();
            ScaleZ = reader.ReadSingle();
            UpWaitTime = reader.ReadInt16();
            DownWaitTime = reader.ReadInt16();
            ScaleY = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)WeightType);
            writer.Write(LinkID);
            writer.Write(Height);
            writer.Write(ScaleX);
            writer.Write(ScaleZ);
            writer.Write(UpWaitTime);
            writer.Write(DownWaitTime);
            writer.Write(ScaleY);
        }
    }
}