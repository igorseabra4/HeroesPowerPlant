using Heroes.SDK.Definitions.Structures.Stage.Spawn;
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
            entryRenderer = new EntryRenderer(position.Position.ToSharpDXVector3(), position.Pitch, Color.White.ToVector3());
        }

        public float PositionX
        {
            get => position.Position.ToSharpDXVector3().X;
            set => position.Position = new Heroes.SDK.Utilities.Math.Structs.Vector3 { X = value, Y = position.Position.Y, Z = position.Position.Z };
        }

        public float PositionY
        {
            get => position.Position.ToSharpDXVector3().Y;
            set => position.Position = new Heroes.SDK.Utilities.Math.Structs.Vector3 { X = position.Position.X, Y = value, Z = position.Position.Z };
        }

        public float PositionZ
        {
            get => position.Position.ToSharpDXVector3().Z;
            set => position.Position = new Heroes.SDK.Utilities.Math.Structs.Vector3 { X = position.Position.X, Y = position.Position.Y, Z = value };
        }

        public ushort Pitch
        {
            get => position.Pitch;
            set => position.Pitch = value;
        }

        public int HoldTime
        {
            get => position.HoldTime;
            set => position.HoldTime = value;
        }

        public StartPositionMode Mode
        {
            get => position.Mode;
            set => position.Mode = value;
        }

        public void NewColor(Vector3 v)
        {
            entryRenderer = new EntryRenderer(position.Position.ToSharpDXVector3(), position.Pitch, v);
        }

        public void CreateTransformMatrix()
        {
            entryRenderer.NewMatrix(position.Position.ToSharpDXVector3(), position.Pitch);
        }

        public void Render(SharpRenderer renderer)
        {
            entryRenderer.Render(renderer);
        }
    }
}
