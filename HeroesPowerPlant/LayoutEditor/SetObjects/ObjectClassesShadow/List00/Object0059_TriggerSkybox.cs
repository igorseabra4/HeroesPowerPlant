using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0059_TriggerSkybox : SetObjectShadow
    {
        //BlockChange (init only, no exec())
        //Player::Player, Player::PlayerObject,
        //Player::PlayerBase, Type, On/Off, Radius

        // 0 unk (always 0)
        // 1 int
        // 2 float
        // 3 int
        // 4 int
        // 5 int
        // 6 int

        [Description("Always 0 in original objects, purpose unknown\n Insert an int here for testing")]
        public int Unknown_int0
        {
            get => ReadInt(0);
            set => Write(0, value);
        }

        [Description("Always 0 in original objects, purpose unknown\n Insert a float here for testing")]
        public float Unknown_float0
        {
            get => ReadFloat(0);
            set => Write(0, value);
        }

        [Description("Force the chunk(s) On or Off while in Radius.\n Ignores Visibility constraints")]
        public CommonNoYes DrawMode
        {
            get => (CommonNoYes)ReadInt(4);
            set => Write(4, (int)value);
        }

        [Description("Radius from origin that the object is active")]
        public float Radius
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        [Description("Chunk to force on/off, set to -1 or 0 to pick none")]
        public int Chunk_1
        {
            get => ReadInt(12);
            set => Write(12, value);
        }

        [Description("Chunk to force on/off, set to -1 or 0 to pick none")]
        public int Chunk_2
        {
            get => ReadInt(16);
            set => Write(16, value);
        }

        [Description("Chunk to force on/off, set to -1 or 0 to pick none")]
        public int Chunk_3
        {
            get => ReadInt(20);
            set => Write(20, value);
        }

        [Description("Chunk to force on/off, set to -1 or 0 to pick none")]
        public int Chunk_4
        {
            get => ReadInt(24);
            set => Write(24, value);
        }

    }
}

