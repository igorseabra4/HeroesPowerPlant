using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class ObjectEntry
    {
        public byte List;
        public byte Type;
        public string Name;
        public string DebugName;
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

        public string GetName()
        {
            if (Name != "")
                return Name;
            if (DebugName != "")
                return DebugName;
            return "Unknown/Unused";
        }
    }
}
