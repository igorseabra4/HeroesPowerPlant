using SharpDX;
using System;
using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0019_Weight : SetObjectShadow
    {
        public enum EMoveType
        {
            UpDown,
            WaitForPlayer,
            NeverMove
        }

        public EMoveType MoveType { get; set; }
        public float Height { get; set; }
        public float WaitTimeTop { get; set; }
        public float WaitTimeBottom { get; set; }
        public float WaitTimeIfShot { get; set; }
        public int u5_int { get; set; } // 1 = never rise again
        public float u5_float
        {
            get => BitConverter.ToSingle(BitConverter.GetBytes(u5_int), 0);
            set => u5_int = BitConverter.ToInt32(BitConverter.GetBytes(value), 0);
        }
        public float ScaleX { get; set; }
        public float ScaleY { get; set; }
        public float ScaleZ { get; set; }

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            MoveType = (EMoveType)reader.ReadInt32();
            Height = reader.ReadSingle();
            WaitTimeTop = reader.ReadSingle();
            WaitTimeBottom = reader.ReadSingle();
            WaitTimeIfShot = reader.ReadSingle();
            u5_int = reader.ReadInt32();
            ScaleX = reader.ReadSingle();
            ScaleY = reader.ReadSingle();
            ScaleZ = reader.ReadSingle();
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write((int)MoveType);
            writer.Write(Height);
            writer.Write(WaitTimeTop);
            writer.Write(WaitTimeBottom);
            writer.Write(WaitTimeIfShot);
            writer.Write(u5_int);
            writer.Write(ScaleX);
            writer.Write(ScaleY);
            writer.Write(ScaleZ);
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ);
            transformMatrix *= DefaultTransformMatrix();
            CreateBoundingBox();
        }
    }
}

