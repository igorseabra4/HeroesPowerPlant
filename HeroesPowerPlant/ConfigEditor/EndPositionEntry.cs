using Heroes.SDK.Definitions.Structures.Stage.Spawn;
using Heroes.SDK.Definitions.Enums;
using Heroes.SDK.Definitions.Structures.Stage.Splines;
using SharpDX;
using Newtonsoft.Json.Linq;

namespace HeroesPowerPlant.ConfigEditor
{
    public class EndPositionEntry
    {
        private PositionEnd position;
        public PositionEnd Position { get => position; }

        private EntryRenderer entryRenderer;

        public EndPositionEntry()
        {
            entryRenderer = new EntryRenderer(position.Position.ToSharpDXVector3(), position.Pitch, Color.White.ToVector3());
        }

        public float PositionX
        {
            get => position.Position.X;
            set => position.Position = new Heroes.SDK.Utilities.Math.Structs.Vector3 { X = value, Y = position.Position.Y, Z = position.Position.Z };
        }

        public float PositionY
        {
            get => position.Position.Y;
            set => position.Position = new Heroes.SDK.Utilities.Math.Structs.Vector3 { X = position.Position.X, Y = value, Z = position.Position.Z };
        }

        public float PositionZ
        {
            get => position.Position.Z;
            set => position.Position = new Heroes.SDK.Utilities.Math.Structs.Vector3 { X = position.Position.X, Y = position.Position.Y, Z = value };
        }

        public ushort Pitch
        {
            get => position.Pitch;
            set => position.Pitch = value;
        }

        public void NewColor(Vector3 c)
        {
            entryRenderer = new EntryRenderer(position.Position.ToSharpDXVector3(), position.Pitch, c);
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
