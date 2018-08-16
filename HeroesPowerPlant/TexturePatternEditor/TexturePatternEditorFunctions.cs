using System.Collections.Generic;
using System.IO;

namespace HeroesPowerPlant.TexturePatternEditor
{
    public static class TexturePatternEditorFunctions
    {
        public static IEnumerable<PatternEntry> GetPatternEntriesFromFile(string fileName)
        {
            BinaryReader patternReader = new BinaryReader(new FileStream(fileName, FileMode.Open));
            List<PatternEntry> patterns = new List<PatternEntry>();

            uint frameCount = patternReader.ReadUInt32();

            while (frameCount != 0xFFFFFFFF)
            {
                patternReader.BaseStream.Position += 0x204;

                string textureName = new string(patternReader.ReadChars(0x20)).Trim('\0');
                string animationName = new string(patternReader.ReadChars(0x20)).Trim('\0');

                List<Frame> frames = new List<Frame>();

                ushort FrameOffset = patternReader.ReadUInt16();
                ushort TextureNumber = patternReader.ReadUInt16();

                while (FrameOffset != 0xFFFF & TextureNumber != 0xFFFF)
                {
                    frames.Add(new Frame()
                    {
                        FrameOffset = FrameOffset,
                        TextureNumber = TextureNumber
                    });

                    FrameOffset = patternReader.ReadUInt16();
                    TextureNumber = patternReader.ReadUInt16();
                }

                patterns.Add(new PatternEntry()
                {
                    FrameCount = frameCount,
                    TextureName = textureName,
                    AnimationName = animationName,
                    frames = frames
                });

                frameCount = patternReader.ReadUInt32();
            }

            patternReader.Close();
            return patterns;
        }

        public static void SaveTXC(IEnumerable<PatternEntry> PatternEntries, string fileName)
        {
            BinaryWriter patternWriter = new BinaryWriter(new FileStream(fileName, FileMode.Create));

            foreach (PatternEntry p in PatternEntries)
            {
                patternWriter.Write(p.FrameCount);
                patternWriter.BaseStream.Position += 0x204;

                foreach (char c in p.TextureName)
                    patternWriter.Write(c);
                for (int i = p.TextureName.Length; i < 0x20; i++)
                    patternWriter.Write((byte)0);
                foreach (char c in p.AnimationName)
                    patternWriter.Write(c);
                for (int i = p.AnimationName.Length; i < 0x20; i++)
                    patternWriter.Write((byte)0);

                foreach (Frame f in p.frames)
                {
                    patternWriter.Write(f.FrameOffset);
                    patternWriter.Write(f.TextureNumber);
                }

                patternWriter.Write(0xFFFFFFFF);
            }
            patternWriter.Write(0xFFFFFFFF);

            patternWriter.Close();
        }
    }
}
