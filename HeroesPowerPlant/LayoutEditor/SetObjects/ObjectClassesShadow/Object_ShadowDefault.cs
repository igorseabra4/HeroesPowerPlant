using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_ShadowDefault : SetObjectShadow
    {
        [Browsable(false)]
        public byte[] MiscSettingsBytes
        {
            get => MiscSettings;
            set => MiscSettings = value;
        }

        public int[] MiscSettingInts
        {
            get
            {
                List<int> result = new List<int>();
                for (int i = 0; i < MiscSettings.Length; i += 4)
                {
                    result.Add(BitConverter.ToInt32(MiscSettings, i));
                }
                return result.ToArray();
            }
            set
            {
                List<byte> result = new List<byte>();
                foreach (int i in value)
                    result.AddRange(BitConverter.GetBytes(i));
                MiscSettings = result.ToArray();
            }
        }

        public float[] MiscSettingFloats
        {
            get
            {
                List<float> result = new List<float>();
                for (int i = 0; i < MiscSettings.Length; i += 4)
                {
                    result.Add(BitConverter.ToSingle(MiscSettings, i));
                }
                return result.ToArray();
            }
            set
            {
                List<byte> result = new List<byte>();
                foreach (float i in value)
                    result.AddRange(BitConverter.GetBytes(i));
                MiscSettings = result.ToArray();
            }
        }
    }
}