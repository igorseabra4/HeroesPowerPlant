using System.Collections.Generic;

namespace HeroesPowerPlant.ShadowTexturePatternEditor
{
    public struct ShadowTexturePatternFrame
    {
        public uint FrameOffset;
        public uint TextureNumber;

        public ShadowTexturePatternFrame(ShadowTexturePatternFrame f)
        {
            FrameOffset = f.FrameOffset;
            TextureNumber = f.TextureNumber;
        }

        public override string ToString()
        {
            return $"[{FrameOffset} : {TextureNumber}]";
        }
    }

    public class ShadowPatternEntry
    {
        public string FileName;
        public uint FrameCount;
        public string TextureName;
        public string AnimationName;
        public uint UnknownInt;
        public uint KeyframeCount { get => (uint)frames.Count; }
        public List<ShadowTexturePatternFrame> frames;

        public ShadowPatternEntry()
        {
            FileName = "default.adb";
            TextureName = "default";
            AnimationName = "default";
            frames = new List<ShadowTexturePatternFrame>();
        }

        public ShadowPatternEntry(ShadowPatternEntry p)
        {
            FileName = p.FileName;
            FrameCount = p.FrameCount;
            TextureName = p.TextureName;
            AnimationName = p.AnimationName;
            UnknownInt = p.UnknownInt;

            frames = new List<ShadowTexturePatternFrame>();
            foreach (ShadowTexturePatternFrame f in p.frames)
                frames.Add(new ShadowTexturePatternFrame(f));
        }

        public override string ToString()
        {
            return $"{FileName} [{FrameCount}]";
        }

        // Rendering
        private uint counter = 0;
        public bool isSelected;

        public void Animate(ShadowTexturePatternEditor editor, BSPRenderer bspRenderer, DFFRenderer dffRenderer)
        {
            if (FrameCount == 0)
                return;

            counter++;
            counter = counter % FrameCount;

            for (int i = 0; i < frames.Count; i++)
                if (frames[i].FrameOffset == counter)
                {
                    string newTextureName = AnimationName + "." + frames[i].TextureNumber;
                    if (TextureManager.HasTexture(newTextureName))
                        TextureManager.SetTextureForAnimation(TextureName, newTextureName, bspRenderer, dffRenderer);
                }

            if (isSelected)
                editor.SendPlaying(counter);
        }

        public void StopAnimation(BSPRenderer bspRenderer, DFFRenderer dffRenderer)
        {
            counter = 0;

            if (TextureManager.HasTexture(TextureName))
                TextureManager.SetTextureForAnimation(TextureName, TextureName, bspRenderer, dffRenderer);
        }
    }
}
