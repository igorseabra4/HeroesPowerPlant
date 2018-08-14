using GenericStageInjectionCommon.Structs.Enums;
using GenericStageInjectionCommon.Structs.Positions.Substructures;
using SharpDX;

namespace HeroesPowerPlant.ConfigEditor
{
    public class StartPositionEntry
    {
        private PositionStart position;
        public PositionStart Position { get => position; }

        private EntryRenderer entryRenderer;

        public StartPositionEntry()
        {
            entryRenderer = new EntryRenderer(position.Position.ToVector3(), position.Pitch, Color.White.ToVector3());
        }

        public float PositionX
        {
            get { return position.Position.ToVector3().X; }
            set { position.Position.X = value; }
        }

        public float PositionY
        {
            get { return position.Position.ToVector3().Y; }
            set { position.Position.Y = value; }
        }

        public float PositionZ
        {
            get { return position.Position.ToVector3().Z; }
            set { position.Position.Z = value; }
        }

        public int Pitch
        {
            get { return position.Pitch; }
            set { position.Pitch = value; }
        }

        public int HoldTime
        {
            get { return position.HoldTime; }
            set { position.HoldTime = value; }
        }

        public StartPositionMode Mode
        {
            get { return position.Mode; }
            set { position.Mode = value; }
        }

        public void NewColor(Vector3 v)
        {
            entryRenderer = new EntryRenderer(position.Position.ToVector3(), position.Pitch, v);
        }

        public void CreateTransformMatrix()
        {
            entryRenderer.NewMatrix(position.Position.ToVector3(), position.Pitch);
        }

        public void Render()
        {
            entryRenderer.Render();
        }
    }
}
