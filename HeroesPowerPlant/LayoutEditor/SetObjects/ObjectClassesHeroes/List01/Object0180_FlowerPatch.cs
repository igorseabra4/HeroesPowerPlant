using SharpDX;
using System;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0180_FlowerPatch : SetObjectManagerHeroes
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
