using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0180_FlowerPatch : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            if (modelNames == null)
                return BoundingBox.FromPoints(Program.MainForm.renderer.cubeVertices.ToArray());
            else if (modelNames.Length == 0 | Type >= modelNames.Length | !Program.MainForm.renderer.dffRenderer.DFFModels.ContainsKey(modelNames[Type]))
                return BoundingBox.FromPoints(Program.MainForm.renderer.cubeVertices.ToArray());
            
            return BoundingBox.FromPoints(Program.MainForm.renderer.dffRenderer.DFFModels[modelNames[Type]].vertexListG.ToArray());
        }

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

        public override void Draw(SharpRenderer renderer, string[] modelNames, bool isSelected)
        {
            if (Type <= 8)
                Draw(renderer, modelNames[Type], isSelected);
            else
                DrawCube(renderer, isSelected);
        }

        public byte Type
        {
            get => ReadByte(4);
            set => Write(4, value);
        }

        public float Scale
        {
            get => ReadFloat(8);
            set { Write(8, value); CreateTransformMatrix(); }
        }
    }
}
