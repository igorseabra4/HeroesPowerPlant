using HeroesPowerPlant.Shared.Utilities;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0182_Whale : SetObjectHeroes
    {
        private Matrix triggerMatrix;

        public override void CreateTransformMatrix()
        {
            base.CreateTransformMatrix();

            triggerMatrix = Matrix.Scaling(TriggerSize) * Matrix.Translation(TriggerX, TriggerY, TriggerZ);
        }

        public override void Draw(SharpRenderer renderer)
        {
            base.Draw(renderer);

            if (isSelected)
                renderer.DrawSphereTrigger(triggerMatrix, true);
        }

        public byte WhaleType { get; set; }
        public short TriggerSize { get; set; }
        public float WhaleScale { get; set; }
        public float ArchRadius { get; set; }
        public float TriggerX { get; set; }
        public float TriggerY { get; set; }
        public float TriggerZ { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            WhaleType = reader.ReadByte();
            reader.BaseStream.Position += 1;
            TriggerSize = reader.ReadInt16();
            WhaleScale = reader.ReadSingle();
            ArchRadius = reader.ReadSingle();
            TriggerX = reader.ReadSingle();
            TriggerY = reader.ReadSingle();
            TriggerZ = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(WhaleType);
            writer.Write((byte)0);
            writer.Write(TriggerSize);
            writer.Write(WhaleScale);
            writer.Write(ArchRadius);
            writer.Write(TriggerX);
            writer.Write(TriggerY);
            writer.Write(TriggerZ);
        }
    }
}
