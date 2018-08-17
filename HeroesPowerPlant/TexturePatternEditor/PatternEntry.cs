using System;
using System.Collections.Generic;

namespace HeroesPowerPlant.TexturePatternEditor
{
    public struct Frame
    {
        public ushort FrameOffset;
        public ushort TextureNumber;

        public Frame(Frame f)
        {
            FrameOffset = f.FrameOffset;
            TextureNumber = f.TextureNumber;
        }

        public override string ToString()
        {
            return $"[{FrameOffset} : {TextureNumber}]";
        }
    }

    public class PatternEntry
    {
        public uint FrameCount;
        public string TextureName;
        public string AnimationName;
        public List<Frame> frames;

        public PatternEntry()
        {
            TextureName = "default";
            AnimationName = "default";
            frames = new List<Frame>();
        }

        public PatternEntry(PatternEntry p)
        {
            FrameCount = p.FrameCount;
            TextureName = p.TextureName;
            AnimationName = p.AnimationName;

            frames = new List<Frame>();
            foreach (Frame f in p.frames)
                frames.Add(new Frame(f));
        }

        public override string ToString()
        {
            return $"{TextureName} [{FrameCount}]";
        }


        // Rendering
        private uint counter = 0;

        public void Animate()
        {
            if (FrameCount == 0)
                return;

            counter++;
            counter = counter % FrameCount;

            for (int i = 0; i < frames.Count; i++)
                if (frames[i].FrameOffset == counter)
                {
                    string newTextureName = AnimationName + "." + frames[i].TextureNumber;
                    if (BSPRenderer.HasTexture(newTextureName))
                        BSPRenderer.SetTexture(TextureName, newTextureName);
                }
        }

        public void StopAnimation()
        {
            counter = 0;

            if (BSPRenderer.HasTexture(TextureName))
                BSPRenderer.SetTexture(TextureName, TextureName);
        }
    }
}
