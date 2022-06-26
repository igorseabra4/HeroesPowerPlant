namespace Shadow.Structures
{
    /**
     * SFXEntry
     * size = 0x18 
     * 
       ---Entries---
        0x0 | int String .bin offset | (first entry @ 0x28)
        0x4-0x7 | int?? Game SFX Index
        0x7 | byte? Game SFX Index
        0x8 | byte Bank ID
        0x9 | byte Bank SFX Index
        0xA | ?? (not sure)byte Loudness Left Channel
        0xB |  (99% sure) byte Loudness Right Channel
        0xC | ??
        0xD | SFX Duration
        0xE | ?? When divisible by 3 changes to menu sfx?
        0xF | ??
        0x10-0x13 | ??
        0x14 | ?? No Sound if greater than 0x80
        0x15 | ??
        0x16 | ??
        0x17 | ??
     * 
     */
    public struct SFXEntry
    {
        public int stringRefAddress;
        public int sfxId;
        public int temp0x8;
        public int temp0xC;
        public int temp0x10;
        public int temp0x14; // note real data below; but not relevant in current application

        /*        public byte bankId;
                public byte bankSfxIndex;
                public byte loudnessLeftChannel;
                public byte loudnessRightChannel;
                public byte unknown0xC;
                public byte sfxDuration;
                public byte unknown0xE;
                public byte unknown0xF;
                public int unknown0x10;
                public int unknown0x14;*/

        // not part of struct; convenience only
        public string sfxString;
        // end not part of struct

        public override string ToString() => sfxString;
    }
}
