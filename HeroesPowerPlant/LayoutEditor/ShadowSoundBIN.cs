using System;
using System.Collections.Generic;
using System.Text;
using HeroesPowerPlant.Shared.Utilities;

namespace Shadow.Structures
{

    /*
     * sound/*.BIN file
        ---HEADER--- BIG ENDIAN
        Size (num of entries) @ 0x24 (int)
        difference of 0x20? 
        dreamsyntax — 02/01/2021
        
        Start list<SFXEntry> at 0x28; each 0x18 in size
        list<String> (null terminated)
        0x76 ending section of @BFFFF[...]FF followed by padded nulls then SCENE(\0)
    */

    public struct ShadowSoundBIN
    {
        public string fileName;
        public string filterString;
        public List<SFXEntry> sfxTable;
        private const int ENTRY_SIZE = 0x18;

        public ShadowSoundBIN(string fileName, ref byte[] file)
        {
            this = ParseShadowSoundBINFile(fileName, ref file);
            this.fileName = fileName;
        }

        /// <summary>
        /// Parses a .BIN (Shadow sound) file
        /// </summary>
        /// <param name="fileName">Full name (not SafeName) retrieved via a FilePicker</param>
        /// <param name="file">Bytes of file to parse</param>
        /// <param name="filterString">String to remove when ToString() is called</param>
        /// <returns>ShadowSoundBIN</returns>
        public static ShadowSoundBIN ParseShadowSoundBINFile(string fileName, ref byte[] file, string filterString = "")
        {
            ShadowSoundBIN bin = new ShadowSoundBIN();

            bin.fileName = fileName;
            bin.filterString = filterString;

            int numberOfEntries = BitConverter.ToInt32(file, 0x24).ReverseEndian();
            int positionIndex = 0x28;

            bin.sfxTable = new List<SFXEntry>();

            // read sfx entries
            for (int i = 0; i < numberOfEntries; i++)
            {
                SFXEntry entry = new SFXEntry
                {
                    stringRefAddress = BitConverter.ToInt32(file, positionIndex).ReverseEndian(),
                    sfxId = BitConverter.ToInt32(file, positionIndex + 4).ReverseEndian(),
                    temp0x8 = BitConverter.ToInt32(file, positionIndex + 8).ReverseEndian(),
                    temp0xC = BitConverter.ToInt32(file, positionIndex + 0xC).ReverseEndian(),
                    temp0x10 = BitConverter.ToInt32(file, positionIndex + 0x10).ReverseEndian(),
                    temp0x14 = BitConverter.ToInt32(file, positionIndex + 0x14).ReverseEndian()
                };

                bin.sfxTable.Add(entry);
                positionIndex += ENTRY_SIZE;
            }

            // read strings
            for (int i = 0; i < numberOfEntries; i++)
            {
                int stringLength;
                string parsedString;
                if (i == numberOfEntries - 1)
                {
                    // if last entry, size is originalFilesize - entry index
                    // 
                    // end at first "\0"
                    stringLength = file.Length - positionIndex;
                    parsedString = Encoding.ASCII.GetString(file, positionIndex, stringLength).Split("\0")[0];
                }
                else
                {
                    // otherwise calculate based on next entry in list
                    stringLength = bin.sfxTable[i + 1].stringRefAddress - bin.sfxTable[i].stringRefAddress;
                    parsedString = Encoding.ASCII.GetString(file, positionIndex, stringLength);
                }
                bin.UpdateEntryString(i, parsedString);
                positionIndex += stringLength;
            }

            return bin;
        }

        public void UpdateEntryString(int sfxEntryIndex, string updatedText)
        {
            SFXEntry entry = sfxTable[sfxEntryIndex];
            entry.sfxString = updatedText;
            sfxTable[sfxEntryIndex] = entry;
        }

        public override string ToString()
        {
            if (filterString != "")
                return fileName.Split(filterString + '\\')[1];
            return fileName;
        }
    }
}