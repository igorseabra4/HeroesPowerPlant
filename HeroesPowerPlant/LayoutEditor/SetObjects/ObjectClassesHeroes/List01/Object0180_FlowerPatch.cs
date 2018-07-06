using SharpDX;
using System;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0180_FlowerPatch : SetObjectManagerHeroes
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
            if (Type <= 8)
                if (DFFRenderer.DFFStream.ContainsKey(modelNames[Type]))
                    DFFRenderer.DFFStream[modelNames[Type]].Render();
                else
                    DrawCube(Matrix.Scaling(5) * transformMatrix, isSelected);
            else
                DrawCube(Matrix.Scaling(5) * transformMatrix, isSelected);
        }

        public byte Type
        {
            get { return ReadWriteByte(4); }
            set { ReadWriteByte(4, value); }
        }

        public float Scale
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }
    }
}
