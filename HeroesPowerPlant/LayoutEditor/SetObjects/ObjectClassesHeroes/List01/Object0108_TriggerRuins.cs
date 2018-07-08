using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0108_TriggerRuins : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            return new BoundingBox(-Vector3.One, Vector3.One);
        }

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            transformMatrix = Matrix.Scaling(ScaleX / 2, ScaleY / 2, ScaleZ / 2) *
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X)) *
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z)) *
                Matrix.Translation(Position);
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            DrawCube(transformMatrix, isSelected);
        }

        public float ScaleX
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float ScaleY
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float ScaleZ
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public enum RuinType : byte
        {
            SeasideHillRuin = 0,
            OceanPalaceRuins = 1
        }

        public RuinType Type
        {
            get { return (RuinType)ReadWriteByte(16); }
            set { byte a = (byte)value; ReadWriteByte(16, a); }
        }
    }
}
