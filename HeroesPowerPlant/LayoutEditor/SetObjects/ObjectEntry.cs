using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class ObjectEntry
    {
        public byte List;
        public byte Type;
        public string Name;
        public string DebugName;
        private string _description;
        public string Description
        {
            get => MiscSettingCount == -1 ? _description : _description + "\nSetting Count: " + (MiscSettingCount / 4).ToString();
            set => _description = value;
        }
        public int ModelMiscSetting;
        public string[][] ModelNames;
        public bool HasMiscSettings;
        public int MiscSettingCount;

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Name))
                return string.Format("{0, 2:X2} {1, 2:X2} {2}", List, Type, Name);
            else if (!string.IsNullOrWhiteSpace(DebugName))
                return string.Format("{0, 2:X2} {1, 2:X2} {2}", List, Type, DebugName);
            else
                return string.Format("{0, 2:X2} {1, 2:X2} {2}", List, Type, "Unknown / Unused");
        }
    }
}
