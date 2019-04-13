using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1184_SmokeScreen : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            if (modelNames == null)
                return BoundingBox.FromPoints(Program.MainForm.renderer.cubeVertices.ToArray());
            else if (modelNames.Length == 0 | ModelNumber >= modelNames.Length | !Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(modelNames[ModelNumber]))
                return BoundingBox.FromPoints(Program.MainForm.renderer.cubeVertices.ToArray());

            return BoundingBox.FromPoints(Program.MainForm.renderer.dffRenderer.DFFModels[modelNames[ModelNumber]].vertexListG.ToArray());
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

        public override void Draw(SharpRenderer renderer, string[] modelNames, bool isSelected)
        {
            if (ModelNumber < modelNames.Length)
                Draw(renderer, modelNames[ModelNumber], isSelected);
            else
                DrawCube(renderer, isSelected);
        }

        public int ModelNumber
        {
            get => ReadInt(4);
            set => Write(4, value);
        }

        public float Speed
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public bool IsUpsideDown
        {
            get => ReadByte(12) != 0;
            set => Write(12, (byte)(value ? 1 : 0));
        }
    }
}
