using System;
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

        [MiscSetting(0), Description("Always 0 in original objects, purpose unknown\n Insert an int here for testing")]
        public int Unknown_int0 { get; set; }

        [Description("Always 0 in original objects, purpose unknown\n Insert a float here for testing")]
        public float Unknown_float0
        {
            get => BitConverter.ToSingle(BitConverter.GetBytes(Unknown_int0), 0);
            set => Unknown_int0 = BitConverter.ToInt32(BitConverter.GetBytes(value));
        }

        [MiscSetting(1), Description("Force the chunk(s) On or Off while in Radius.\n Ignores Visibility constraints")]
        public ENoYes DrawMode { get; set; }

        [MiscSetting(2), Description("Radius from origin that the object is active")]
        public float Radius { get; set; }

        [MiscSetting(3), Description("Chunk to force on/off, set to -1 or 0 to pick none")]
        public int Chunk_1 { get; set; }

        [MiscSetting(4), Description("Chunk to force on/off, set to -1 or 0 to pick none")]
        public int Chunk_2 { get; set; }

        [MiscSetting(5), Description("Chunk to force on/off, set to -1 or 0 to pick none")]
        public int Chunk_3 { get; set; }

        [MiscSetting(6), Description("Chunk to force on/off, set to -1 or 0 to pick none")]
        public int Chunk_4 { get; set; }
    }
}

