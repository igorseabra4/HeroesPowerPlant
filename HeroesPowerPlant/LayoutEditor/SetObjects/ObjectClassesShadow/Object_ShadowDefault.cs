using System;
using System.Collections.Generic;
using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_ShadowDefault : SetObjectShadow
    {
        private byte[] _miscSettingBytes = Array.Empty<byte>();

        public override void ReadMiscSettings(BinaryReader reader, int count)
        {
            _miscSettingBytes = reader.ReadBytes(count);
        }

        public override void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write(_miscSettingBytes);
        }

        protected override int GetModelNumber()
        {
            try
            {
                int mms = Convert.ToInt32(ModelMiscSetting);
                if (mms != -1 && mms < _miscSettingBytes.Length)
                    return _miscSettingBytes[mms];
            }
            catch
            {
                return base.GetModelNumber();
            }
            return 0;
        }

        public int[] MiscSettingInts
        {
            get
            {
                var result = new List<int>();
                for (int i = 0; i < _miscSettingBytes.Length; i += 4)
                    result.Add(BitConverter.ToInt32(_miscSettingBytes, i));
                return result.ToArray();
            }
            set
            {
                var result = new List<byte>();
                foreach (int i in value)
                    result.AddRange(BitConverter.GetBytes(i));
                _miscSettingBytes = result.ToArray();
            }
        }

        public float[] MiscSettingFloats
        {
            get
            {
                var result = new List<float>();
                for (int i = 0; i < _miscSettingBytes.Length; i += 4)
                    result.Add(BitConverter.ToSingle(_miscSettingBytes, i));
                return result.ToArray();
            }
            set
            {
                var result = new List<byte>();
                foreach (float i in value)
                    result.AddRange(BitConverter.GetBytes(i));
                _miscSettingBytes = result.ToArray();
            }
        }
    }
}