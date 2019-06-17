using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum RuinType : byte
    {
        SeasideHillRuin = 0,
        OceanPalaceRuins = 1
    }

    public class Object0108_TriggerRuins : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[][] modelNames, int miscSettingByte)
        {
            return new BoundingBox(-Vector3.One / 2, Vector3.One / 2);
        }

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ) *
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                Matrix.Translation(Position);
        }

        public override bool TriangleIntersection(Ray r, string[][] modelNames, int miscSettingByte, float initialDistance, out float distance)
        {
            return TriangleIntersection(r, Program.MainForm.renderer.cubeTriangles, Program.MainForm.renderer.cubeVertices, initialDistance, out distance, 0.25f);
        }

        public override void Draw(SharpRenderer renderer, string[][] modelNames, int miscSettingByte, bool isSelected)
        {
            renderer.DrawCubeTrigger(transformMatrix, isSelected);
        }

        public float ScaleX
        {
            get => ReadFloat(4);
            set { Write(4, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float ScaleY
        {
            get => ReadFloat(8);
            set { Write(8, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float ScaleZ
        {
            get => ReadFloat(12);
            set { Write(12, value); CreateTransformMatrix(Position, Rotation); }
        }

        public RuinType Type
        {
            get => (RuinType)ReadByte(16);
            set => Write(16, (byte) value);
        }
    }
}
