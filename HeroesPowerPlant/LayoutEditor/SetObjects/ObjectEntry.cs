using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class ObjectEntry
    {
        public byte List;
        public byte Type;
        public string Name;
        public string DebugName;
        public string Description;
        public string[] ModelNames;
        public bool HasMiscSettings;

        public override string ToString()
        {
            if (Name != "")
                return String.Format("{0, 2:X2} {1, 2:X2} {2}", List, Type, Name);
            else if (DebugName != "")
                return String.Format("{0, 2:X2} {1, 2:X2} {2}", List, Type, DebugName);
            else
                return String.Format("{0, 2:X2} {1, 2:X2} {2}", List, Type, "Unknown / Unused");
        }

        public string GetName()
        {
            if (Name != "")
                return Name;
            else if (DebugName != "")
                return DebugName;
            else
                return "Unknown/Unused";
        }

        public override bool Equals(object obj)
        {
            if (obj is ObjectEntry o)
                return (o.GetHashCode() == GetHashCode());

            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 1507667510;
            hashCode = hashCode * -1521134295 + List.GetHashCode();
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            return hashCode;
        }
    }
}
