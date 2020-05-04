using Newtonsoft.Json;
using SharpDX;
using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public abstract class SetObjectShadow : SetObject
    {
        [JsonConstructor]
        public SetObjectShadow()
        {
            UnkBytes = new byte[8];
            MiscSettings = new byte[0];
        }
        
        public override void CreateTransformMatrix()
        {
            transformMatrix = DefaultTransformMatrix();
            CreateBoundingBox();
        }

        protected Matrix DefaultTransformMatrix(float yAddDeg = 0) =>
            Matrix.RotationY(MathUtil.DegreesToRadians(Rotation.Y + yAddDeg)) *
            Matrix.RotationX(MathUtil.DegreesToRadians(Rotation.X)) *
            Matrix.RotationZ(MathUtil.DegreesToRadians(Rotation.Z)) *
            Matrix.Translation(Position);

        public int ReadInt(int j) => BitConverter.ToInt32(MiscSettings, j);

        public float ReadFloat(int j) => BitConverter.ToSingle(MiscSettings, j);

        public void Write(int j, int value)
        {
            for (int i = 0; i < 4; i++)
                MiscSettings[j + i] = BitConverter.GetBytes(value)[i];
        }

        public void Write(int j, float value)
        {
            for (int i = 0; i < 4; i++)
                MiscSettings[j + i] = BitConverter.GetBytes(value)[i];
        }
    }
}
