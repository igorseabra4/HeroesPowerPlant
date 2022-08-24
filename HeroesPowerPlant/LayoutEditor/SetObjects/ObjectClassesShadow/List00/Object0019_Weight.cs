using SharpDX;
using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0019_Weight : SetObjectShadow
    {
        public enum EMoveType : int
        {
            UpDown,
            WaitForPlayer,
            NeverMove
        }

        [MiscSetting]
        public EMoveType MoveType { get; set; }
        [MiscSetting]
        public float Height { get; set; }
        [MiscSetting]
        public float WaitTimeTop { get; set; }
        [MiscSetting]
        public float WaitTimeBottom { get; set; }
        [MiscSetting]
        public float WaitTimeIfShot { get; set; }
        [MiscSetting]
        public int u5_int { get; set; } // 1 = never rise again
        public float u5_float
        {
            get => BitConverter.ToSingle(BitConverter.GetBytes(u5_int), 0);
            set => u5_int = BitConverter.ToInt32(BitConverter.GetBytes(value), 0);
        }
        [MiscSetting]
        public float ScaleX { get; set; }
        [MiscSetting]
        public float ScaleY { get; set; }
        [MiscSetting]
        public float ScaleZ { get; set; }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ);
            transformMatrix *= DefaultTransformMatrix();
            CreateBoundingBox();
        }
    }
}

