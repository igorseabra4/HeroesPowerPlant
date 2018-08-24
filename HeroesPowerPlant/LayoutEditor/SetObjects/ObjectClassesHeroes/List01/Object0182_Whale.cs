using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0182_Whale : SetObjectManagerHeroes
    {
        private Matrix triggerMatrix;

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            base.CreateTransformMatrix(Position, Rotation);

            triggerMatrix = Matrix.Scaling(TriggerSize) * Matrix.Translation(TriggerX, TriggerY, TriggerZ);
        }

        public override void Draw(SharpRenderer renderer, string[] modelNames, bool isSelected)
        {
            base.Draw(renderer, modelNames, isSelected);

            if (isSelected)
                renderer.DrawSphereTrigger(triggerMatrix, true);
        }

        public byte WhaleType
        {
            get { return ReadByte(4); }
            set { Write(4, value); }
        }

        public short TriggerSize
        {
            get { return ReadShort(6); }
            set { Write(6, value); }
        }

        public float WhaleScale
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float ArchRadius
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public float TriggerX
        {
            get { return ReadFloat(16); }
            set { Write(16, value); }
        }

        public float TriggerY
        {
            get { return ReadFloat(20); }
            set { Write(20, value); }
        }

        public float TriggerZ
        {
            get { return ReadFloat(24); }
            set { Write(24, value); }
        }
    }
}
