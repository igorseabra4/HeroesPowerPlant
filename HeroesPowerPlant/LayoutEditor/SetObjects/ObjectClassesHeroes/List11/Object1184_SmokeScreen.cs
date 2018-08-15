using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1184_SmokeScreen : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            if (modelNames == null)
                return BoundingBox.FromPoints(cubeVertices.ToArray());
            else if (modelNames.Length == 0 | ModelNumber >= modelNames.Length | !DFFRenderer.DFFStream.ContainsKey(modelNames[ModelNumber]))
                return BoundingBox.FromPoints(cubeVertices.ToArray());

            return BoundingBox.FromPoints(DFFRenderer.DFFStream[modelNames[ModelNumber]].GetVertexList().ToArray());
        }
        
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix = IsUpsideDown ? Matrix.RotationY(MathUtil.Pi) : Matrix.Identity *
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                Matrix.Translation(Position);
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (ModelNumber < modelNames.Length)
                Draw(modelNames[ModelNumber], isSelected);
            else
                DrawCube(isSelected);
        }

        public int ModelNumber
        {
            get { return ReadLong(4); }
            set { Write(4, value); }
        }

        public float Speed
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public bool IsUpsideDown
        {
            get { return ReadByte(12) != 0; }
            set { Write(12, (byte)(value ? 1 : 0)); }
        }
    }
}
