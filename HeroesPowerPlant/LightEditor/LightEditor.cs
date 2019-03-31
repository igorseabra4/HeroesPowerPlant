using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using SharpDX;
using HeroesONE_R.Utilities;

namespace HeroesPowerPlant.LightEditor
{
    public class LightEditor
    {
        public List<Light> Lights;
        public bool isShadow;

        private string currentlyOpenParticleFile;
        public string CurrentlyOpenLightFile
        {
            get => currentlyOpenParticleFile;
            private set => currentlyOpenParticleFile = value;
        }

        public LightEditor()
        {
            Lights = new List<Light>();
            isShadow = false;
        }

        public LightEditor(string fileName, bool isShadow)
        {
            currentlyOpenParticleFile = fileName;
            this.isShadow = isShadow;
            byte[] particleBytes = File.ReadAllBytes(fileName);
            SetupLightEditor(ref particleBytes, isShadow);
        }

        public LightEditor(ref byte[] bytes, bool isShadow)
        {
            SetupLightEditor(ref bytes, isShadow);
        }

        private void SetupLightEditor(ref byte[] lightBytes, bool isShadow)
        {
            this.isShadow = isShadow;

            int particleCount = lightBytes.Length / Light.SIZE;
            Lights = new List<Light>(particleCount);

            for (int x = 0; x < particleCount; x++)
            {
                int currentOffset = Light.SIZE * x;
                if (isShadow)
                    Lights.Add(Light.FromLittleEndianBytes(ref lightBytes, currentOffset));
                else
                    Lights.Add(Light.FromBigEndianBytes(ref lightBytes, currentOffset));
            }
        }

        public void Save(string fileName)
        {
            Save(fileName, isShadow);
        }

        public void Save(string fileName, bool isShadow)
        {
            currentlyOpenParticleFile = fileName;

            List<byte> finalFile = new List<byte>(Lights.Count * Light.SIZE);

            foreach (var light in Lights)
                if (isShadow)
                    finalFile.AddRange(Light.GetBytesLittleEndian(light));
                else
                    finalFile.AddRange(Light.GetBytesBigEndian(light));

            File.WriteAllBytes(fileName, finalFile.ToArray());
        }
    }
}
