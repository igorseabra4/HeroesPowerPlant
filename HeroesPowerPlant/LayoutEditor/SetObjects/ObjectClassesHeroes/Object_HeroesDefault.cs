using System;
using System.Collections.Generic;
using System.Linq;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_HeroesDefault : SetObjectManagerHeroes
    {
        public byte[] MiscSettingBytes
        {
            get => MiscSettings;
            set
            {
                var result = value.ToList();
                
                while (result.Count < 36)
                    result.Add(0);
                while (result.Count > 36)
                    result.RemoveAt(result.Count - 1);

                MiscSettings = result.ToArray();
            }
        }

        public short[] MiscSettingShorts
        {
            get
            {
                var result = new List<short>(18);
                for (int i = 0; i < MiscSettings.Length; i += 2)
                    result.Add(BitConverter.ToInt16(new byte[] { MiscSettings[i + 1], MiscSettings[i]}, 0));

                return result.ToArray();
            }
            set
            {
                var result = new List<byte>();

                foreach (short i in value)
                    result.AddRange(BitConverter.GetBytes(i).Reverse());

                while (result.Count < 36)
                    result.Add(0);
                while (result.Count > 36)
                    result.RemoveAt(result.Count - 1);

                MiscSettings = result.ToArray();
            }
        }

        public int[] MiscSettingInts
        {
            get
            {
                var result = new List<int>(9);
                for (int i = 0; i < MiscSettings.Length; i += 4)
                    result.Add(BitConverter.ToInt32(new byte[] { MiscSettings[i + 3], MiscSettings[i + 2], MiscSettings[i + 1], MiscSettings[i] }, 0));
                
                return result.ToArray();
            }
            set
            {
                var result = new List<byte>();

                foreach (int i in value)
                    result.AddRange(BitConverter.GetBytes(i).Reverse());

                while (result.Count < 36)
                    result.Add(0);
                while (result.Count > 36)
                    result.RemoveAt(result.Count - 1);

                MiscSettings = result.ToArray();
            }
        }

        public float[] MiscSettingFloats
        {
            get
            {
                var result = new List<float>(9);
                for (int i = 0; i < MiscSettings.Length; i += 4)
                    result.Add(BitConverter.ToSingle(new byte[] { MiscSettings[i + 3], MiscSettings[i + 2], MiscSettings[i + 1], MiscSettings[i] }, 0));
                
                return result.ToArray();
            }
            set
            {
                var result = new List<byte>();

                foreach (float i in value)
                    result.AddRange(BitConverter.GetBytes(i).Reverse());

                while (result.Count < 36)
                    result.Add(0);
                while (result.Count > 36)
                    result.RemoveAt(result.Count - 1);

                MiscSettings = result.ToArray();
            }
        }
    }
}