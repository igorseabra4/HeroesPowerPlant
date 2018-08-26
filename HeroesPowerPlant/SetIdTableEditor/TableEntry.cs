using HeroesPowerPlant.LayoutEditor;

namespace HeroesPowerPlant.SetIdTableEditor
{
    public class TableEntry
    {
        public ObjectEntry objectEntry;
        public uint values0;
        public uint values1;
        public uint values2;

        public override string ToString()
        {
            return objectEntry.ToString();
        }

        public void FindObjectEntry(byte objList, byte objType, ObjectEntry[] objectEntries)
        {
            bool found = false;
            foreach (ObjectEntry j in objectEntries)
            {
                if (j.List == objList & j.Type == objType)
                {
                    objectEntry = j;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                objectEntry = new ObjectEntry()
                {
                    Type = objType,
                    List = objList,
                    Name = "Unknown/Unused"
                };
            }
        }
    }
}
