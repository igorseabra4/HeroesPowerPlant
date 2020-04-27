using HeroesPowerPlant.LayoutEditor;

namespace HeroesPowerPlant.SetIdTableEditor
{
    public class TableEntry
    {
        public ObjectEntry objectEntry;
        public uint values0;
        public uint values1;
        public uint values2;

        public override string ToString() => objectEntry.ToString();
    }
}
