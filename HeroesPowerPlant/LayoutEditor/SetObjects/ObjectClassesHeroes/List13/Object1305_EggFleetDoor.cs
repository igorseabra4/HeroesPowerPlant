using HeroesPowerPlant.Shared.Utilities;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1305_EggFleetDoor : SetObjectHeroes
    {
        private Matrix triggerMatrix;

        public override void CreateTransformMatrix()
        {
            base.CreateTransformMatrix();

            triggerMatrix = Matrix.Scaling(TriggerSizeX, TriggerSizeY, TriggerSizeZ) *
                Matrix.RotationY(TriggerRotY) *
                Matrix.Translation(TriggerX, TriggerY, TriggerZ);

            CreateBoundingBox();
        }

        public override void Draw(SharpRenderer renderer)
        {
            base.Draw(renderer);

            if (isSelected)
                renderer.DrawCubeTrigger(triggerMatrix, true);
        }

        public float TriggerX { get; set; }
        public float TriggerY { get; set; }
        public float TriggerZ { get; set; }
        public short TriggerSizeX { get; set; }
        public short TriggerSizeY { get; set; }
        public short TriggerSizeZ { get; set; }
        public short TriggerRotY { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            TriggerSizeX = reader.ReadInt16();
            TriggerSizeY = reader.ReadInt16();
            TriggerSizeZ = reader.ReadInt16();
            reader.BaseStream.Position += 2;
            TriggerX = reader.ReadSingle();
            TriggerY = reader.ReadSingle();
            TriggerZ = reader.ReadSingle();
            TriggerRotY = reader.ReadInt16();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(TriggerSizeX);
            writer.Write(TriggerSizeY);
            writer.Write(TriggerSizeZ);
            writer.Pad(2);
            writer.Write(TriggerX);
            writer.Write(TriggerY);
            writer.Write(TriggerZ);
            writer.Write(TriggerRotY);
        }
    }
}