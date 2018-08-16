using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesONE_R.Utilities;

namespace HeroesPowerPlant.ParticleEditor
{
    /// <summary>
    /// Parses Sonic Heroes particles.
    /// </summary>
    public class ParticleEditor
    {
        /// <summary>
        /// The list of particles for an individual particle file.
        /// </summary>
        public List<Particle> Particles;

        /*
            ------------
            Construction
            ------------
        */

        public ParticleEditor() { }

        /// <summary>
        /// Opens a set of Sonic Heroes particles from a supplied file name.
        /// </summary>
        public ParticleEditor(string fileName)
        {
            byte[] particleBytes = File.ReadAllBytes(fileName);
            SetupParticleEditor(ref particleBytes);
        }

        /// <summary>
        /// Opens a set of Sonic Heroes particles from a supplied set of bytes.
        /// </summary>
        public ParticleEditor(ref byte[] bytes)
        {
            SetupParticleEditor(ref bytes);
        }

        /// <summary>
        /// Loads a set of particles from a passed in array of bytes.
        /// </summary>
        private void SetupParticleEditor(ref byte[] particleBytes)
        {
            int particleCount = particleBytes.Length / Particle.SIZE;
            Particles = new List<Particle>(particleCount);

            for (int x = 0; x < particleCount; x++)
            {
                int currentOffset = Particle.SIZE * x;
                Particles.Add(Particle.FromBytes(ref particleBytes, currentOffset));
            }
        }

        /*
            -------
            Methods
            -------
        */

        public void Save(string fileName)
        {
            List<byte> finalFile = new List<byte>(Particles.Count * Particle.SIZE);

            foreach (var particle in Particles)
                finalFile.AddRange(StructUtilities.ConvertStructureToByteArrayUnsafe(particle));

            File.WriteAllBytes(fileName, finalFile.ToArray());
        }
    }
}
