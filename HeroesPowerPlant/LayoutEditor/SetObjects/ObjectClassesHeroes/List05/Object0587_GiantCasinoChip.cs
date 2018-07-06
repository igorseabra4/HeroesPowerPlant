using System;
using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0587_GiantCasinoChip : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, int XRot, int YRot, int ZRot)
        {
            transformMatrix = Matrix.Scaling(Scale)
                * Matrix.RotationX((float)(XRot * (Math.PI / 32768f)))
                * Matrix.RotationY((float)(YRot * (Math.PI / 32768f)))
                * Matrix.RotationZ((float)(ZRot * (Math.PI / 32768f)))
                * Matrix.Translation(Position);
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (Type <= 4)
                if (DFFRenderer.DFFStream.ContainsKey(modelNames[Type]))
                    DFFRenderer.DFFStream[modelNames[Type]].Render();
                else
                    DrawCube(Matrix.Scaling(5) * transformMatrix, isSelected);
            else
                DrawCube(Matrix.Scaling(5) * transformMatrix, isSelected);
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