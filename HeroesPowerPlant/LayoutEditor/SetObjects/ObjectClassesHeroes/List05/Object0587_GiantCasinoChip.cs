using System;
using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0587_GiantCasinoChip : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
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
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public Int32 Type
        {
            get { return ReadWriteLong(8); }
            set { ReadWriteLong(8, value); }
        }

        public Int32 Speed
        {
            get { return ReadWriteLong(12); }
            set { ReadWriteLong(12, value); }
        }
    }
}