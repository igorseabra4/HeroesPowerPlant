using HeroesPowerPlant.Shared.Utilities;
using Newtonsoft.Json;
using SharpDX;
using System;
using System.IO;

namespace HeroesPowerPlant.LayoutEditor
{
    public abstract class SetObjectShadow : SetObject
    {
        public byte[] MiscSettings;

        public virtual void ReadMiscSettings(BinaryReader reader, int count)
        {
            MiscSettings = reader.ReadBytes(count);
        }

        public virtual void WriteMiscSettings(BinaryWriter writer)
        {
            writer.Write(MiscSettings);
        }

        public override byte[] GetMiscSettings()
        {
            using var writer = new EndianBinaryWriter(new MemoryStream(), Endianness.Big);
            WriteMiscSettings(writer);
            return ((MemoryStream)writer.BaseStream).ToArray();
        }

        [JsonConstructor]
        public SetObjectShadow()
        {
            UnkBytes = new byte[8];
            MiscSettings = Array.Empty<byte>();
        }

        public string DefaultMiscSettingCount { get; private set; }

        public override void SetObjectEntry(ObjectEntry objectEntry)
        {
            base.SetObjectEntry(objectEntry);

            if (objectEntry.MiscSettingCount == -1)
                DefaultMiscSettingCount = "Unknown";
            else
                DefaultMiscSettingCount = (objectEntry.MiscSettingCount / 4).ToString();
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = DefaultTransformMatrix();
            CreateBoundingBox();
        }

        protected Matrix DefaultTransformMatrix(float yAddDeg = 0) =>
            Matrix.RotationZ(MathUtil.DegreesToRadians(Rotation.Z)) *
            Matrix.RotationX(MathUtil.DegreesToRadians(Rotation.X)) *
            Matrix.RotationY(MathUtil.DegreesToRadians(Rotation.Y + yAddDeg)) *
            Matrix.Translation(Position);

        protected override int GetModelNumber()
        {
            try
            {
                int mms = Convert.ToInt32(ModelMiscSetting);
                if (mms != -1 && mms < MiscSettings.Length)
                    return MiscSettings[mms];
            }
            catch
            {
                return base.GetModelNumber();
            }
            return 0;
        }

        public int ReadInt(int j) => BitConverter.ToInt32(MiscSettings, j);

        public float ReadFloat(int j) => BitConverter.ToSingle(MiscSettings, j);

        public void Write(int j, int value)
        {
            for (int i = 0; i < 4; i++)
                MiscSettings[j + i] = BitConverter.GetBytes(value)[i];
        }

        public void Write(int j, float value)
        {
            for (int i = 0; i < 4; i++)
                MiscSettings[j + i] = BitConverter.GetBytes(value)[i];
        }
    }
}
