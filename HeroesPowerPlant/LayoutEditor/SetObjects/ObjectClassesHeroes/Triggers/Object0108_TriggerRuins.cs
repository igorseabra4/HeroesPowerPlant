using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum RuinType : byte
    {
        SeasideHillRuin = 0,
        OceanPalaceRuins = 1
    }

    public class Object0108_TriggerRuins : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            return new BoundingBox(-Vector3.One / 2, Vector3.One / 2);
        }

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ) *
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X)) *
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z)) *
                Matrix.Translation(Position);
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            DrawCubeTrigger(transformMatrix, isSelected);
        }

        public float ScaleX
        {
            get { return ReadFloat(4); }
            set { Write(4, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float ScaleY
        {
            get { return ReadFloat(8); }
            set { Write(8, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float ScaleZ
        {
            get { return ReadFloat(12); }
            set { Write(12, value); CreateTransformMatrix(Position, Rotation); }
        }

        public RuinType Type
        {
            get { return (RuinType)ReadByte(16); }
            set { byte a = (byte)value; Write(16, a); }
        }
    }
}
