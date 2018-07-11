using System;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0587_GiantCasinoChip : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix = Matrix.Scaling(Scale)
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.Translation(Position);
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (Type <= 4)
                if (DFFRenderer.DFFStream.ContainsKey(modelNames[Type]))
                    DFFRenderer.DFFStream[modelNames[Type]].Render();
                else
                    DrawCube(isSelected);
            else
                DrawCube(isSelected);
        }

        public float Scale
        {
            get { return ReadFloat(4); }
            set { Write(4, value); CreateTransformMatrix(Position, Rotation); }
        }

        public Int32 Type
        {
            get { return ReadLong(8); }
            set { Write(8, value); }
        }

        public Int32 Speed
        {
            get { return ReadLong(12); }
            set { Write(12, value); }
        }
    }
}