using GenericStageInjectionCommon.Structs.Positions.Substructures;
using SharpDX;

namespace HeroesPowerPlant.ConfigEditor
{
    public class EndPositionEntry
    {
        private PositionEnd position;
        public PositionEnd Position { get => position; }

        private EntryRenderer entryRenderer;

        public EndPositionEntry()
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

        public void NewColor(Vector3 c)
        {
            entryRenderer = new EntryRenderer(position.Position.ToVector3(), position.Pitch, c);
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
