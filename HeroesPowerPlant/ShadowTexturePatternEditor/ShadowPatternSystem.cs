using HeroesONE_R.Structures;
using HeroesONE_R.Structures.Subsctructures;
using HeroesPowerPlant.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.IO;

namespace HeroesPowerPlant.ShadowTexturePatternEditor
{
    public class ShadowPatternSystem
    {
        public bool UnsavedChanges { get; set; } = false;

        private string currentlyOpenONE;
        public string CurrentlyOpenONE
        {
            get => currentlyOpenONE;
            private set => currentlyOpenONE = value;
        }

        private Archive shadowDATONE;

        public List<ShadowPatternEntry> patterns;

        public ShadowPatternSystem()
        {
            patterns = new List<ShadowPatternEntry>();
            UnsavedChanges = false;
        }

        public ShadowPatternSystem(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return;
            }
            byte[] fileContents = File.ReadAllBytes(fileName);
            shadowDATONE = Archive.FromONEFile(ref fileContents);
            EndianBinaryReader splineReader = null;

            patterns = new List<ShadowPatternEntry>();
            currentlyOpenONE = fileName;

            foreach (var file in shadowDATONE.Files)
            {
                if (file.Name.EndsWith(".ADB"))
                {
                    using var patternReader = new EndianBinaryReader(new MemoryStream(file.DecompressThis()), Endianness.Little);
                    uint frameCount = patternReader.ReadUInt32();

                    string textureName = new string(patternReader.ReadChars(0x20)).Trim('\0');
                    string animationName = new string(patternReader.ReadChars(0x20)).Trim('\0');

                    uint unknownInt = patternReader.ReadUInt32();
                    uint keyframeCount = patternReader.ReadUInt32(); // we throw this away since we calculate it ourselves on save

                    List<ShadowTexturePatternFrame> frames = new List<ShadowTexturePatternFrame>();

                    uint FrameOffset = patternReader.ReadUInt32();
                    uint TextureNumber = patternReader.ReadUInt32();

                    while (patternReader.BaseStream.Position < patternReader.BaseStream.Length) // could also count up to keyframeCount
                    {
                        frames.Add(new ShadowTexturePatternFrame()
                        {
                            FrameOffset = FrameOffset,
                            TextureNumber = TextureNumber
                        });

                        FrameOffset = patternReader.ReadUInt32();
                        TextureNumber = patternReader.ReadUInt32();
                    }

                    frames.Add(new ShadowTexturePatternFrame()
                    {
                        FrameOffset = FrameOffset,
                        TextureNumber = TextureNumber
                    });

                    patterns.Add(new ShadowPatternEntry()
                    {
                        FileName = file.Name,
                        UnknownInt = unknownInt,
                        FrameCount = frameCount,
                        TextureName = textureName,
                        AnimationName = animationName,
                        frames = frames
                    });
                }
            }
            UnsavedChanges = false;
        }

        public void Save(string fileName)
        {
            currentlyOpenONE = fileName;
            Save();
        }

        public void Save()
        {
            if (shadowDATONE == null)
            {
                shadowDATONE = new Archive(CommonRWVersions.Shadow050);
            }

            foreach (ShadowPatternEntry p in patterns) 
            {
                List<byte> adbBytes = new List<byte>();

                adbBytes.AddRange(BitConverter.GetBytes(p.FrameCount));
                foreach (char c in p.TextureName)
                    adbBytes.Add((byte)c);
                for (int i = p.TextureName.Length; i < 0x20; i++)
                    adbBytes.Add((byte)0);
                foreach (char c in p.AnimationName)
                    adbBytes.Add((byte)c);
                for (int i = p.AnimationName.Length; i < 0x20; i++)
                    adbBytes.Add((byte)0);
                adbBytes.AddRange(BitConverter.GetBytes(p.UnknownInt));
                adbBytes.AddRange(BitConverter.GetBytes(p.KeyframeCount));

                foreach (ShadowTexturePatternFrame f in p.frames)
                {
                    adbBytes.AddRange(BitConverter.GetBytes(f.FrameOffset));
                    adbBytes.AddRange(BitConverter.GetBytes(f.TextureNumber));
                }

                var adbOutput = adbBytes.ToArray();

                // if this ADB already exists in the .ONE...
                bool foundFile = false;
                foreach (var fileInDat in shadowDATONE.Files)
                {
                    if (fileInDat.Name == p.FileName)
                    {
                        fileInDat.CompressedData = Prs.Compress(ref adbOutput);
                        foundFile = true;
                        break;
                    }
                }

                // if we already stored the file, on to the next
                if (foundFile)
                {
                    continue;
                }

                // add as a new file in the .ONE in the case the ADB was not found
                ArchiveFile file = new ArchiveFile(p.FileName, adbBytes.ToArray());
                shadowDATONE.Files.Add(file);
            }

            // lastly remove any ADBs that were not in the list, since we are not actively keeping track (optimize later?)
            for (int i = 0; i < shadowDATONE.Files.Count; i++)
            {
                if (!shadowDATONE.Files[i].Name.EndsWith(".ADB"))
                {
                    continue;
                }
                bool exit = false;
                foreach (ShadowPatternEntry p in patterns)
                {
                    if (p.FileName == shadowDATONE.Files[i].Name)
                    {
                        exit = true;
                        break; // if we found the current entry in both lists, skip it
                    }
                }
                if (!exit)
                {
                    // if we've reached this point, this file needs to be removed
                    shadowDATONE.Files.RemoveAt(i);
                }
            }

            File.WriteAllBytes(currentlyOpenONE, shadowDATONE.BuildShadowONEArchive(true).ToArray());
            UnsavedChanges = false;
        }

        public IEnumerable<string> GetPatternEntries()
        {
            List<String> list = new List<string>();
            foreach (ShadowPatternEntry p in patterns)
                list.Add(p.ToString());
            return list;
        }

        public int GetPatternCount()
        {
            return patterns.Count;
        }

        public ShadowPatternEntry GetPatternAt(int index)
        {
            if (index >= 0 & index < patterns.Count)
                return patterns[index];
            throw new IndexOutOfRangeException();
        }

        public string Add()
        {
            ShadowPatternEntry p = new ShadowPatternEntry();
            patterns.Add(p);
            UnsavedChanges = true;
            return p.ToString();
        }

        public string Add(int index)
        {
            ShadowPatternEntry p = new ShadowPatternEntry(patterns[index]);
            patterns.Add(p);
            UnsavedChanges = true;
            return p.ToString();
        }

        public int Remove(int index)
        {
            if (index >= 0 & index < patterns.Count)
            {
                patterns[index].StopAnimation(Program.MainForm.LevelEditor.bspRenderer, Program.MainForm.renderer.dffRenderer);
                patterns.RemoveAt(index);
                UnsavedChanges = true;
                return index;
            }
            throw new IndexOutOfRangeException();
        }

        public void Deselect()
        {
            foreach (ShadowPatternEntry p in patterns)
                p.isSelected = false;
        }

        // Rendering

        private bool switcher = true;

        public void Animate(ShadowTexturePatternEditor editor)
        {
            if (switcher)
                foreach (ShadowPatternEntry p in patterns)
                    p.Animate(editor, Program.MainForm.LevelEditor.bspRenderer, Program.MainForm.renderer.dffRenderer);
            switcher = !switcher;
        }

        public void StopAnimation()
        {
            foreach (ShadowPatternEntry p in patterns)
                p.StopAnimation(Program.MainForm.LevelEditor.bspRenderer, Program.MainForm.renderer.dffRenderer);
        }
    }
}
