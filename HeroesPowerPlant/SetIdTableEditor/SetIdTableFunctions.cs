using HeroesPowerPlant.LayoutEditor;
using System;
using System.Collections.Generic;
using System.IO;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.SetIdTableEditor
{
    public static class SetIdTableFunctions
    {
        public static StageEntry[] ReadStageListData(string FileName)
        {
            List<StageEntry> stageEntries = new List<StageEntry>();
            int currentInteger = 0;
            foreach (string i in File.ReadAllLines(FileName))
            {
                if (i.StartsWith("["))
                    currentInteger = Convert.ToInt32(new string(new char[] { i[1] }));
                else
                    stageEntries.Add(new StageEntry()
                    {
                        flag0 = currentInteger == 0 ? Convert.ToUInt32(i.Substring(0, 8), 16) : 0,
                        flag1 = currentInteger == 1 ? Convert.ToUInt32(i.Substring(0, 8), 16) : 0,
                        flag2 = currentInteger == 2 ? Convert.ToUInt32(i.Substring(0, 8), 16) : 0,
                        name = i.Substring(9)
                    });
            }

            return stageEntries.ToArray();
        }

        public static List<TableEntry> LoadTable(string fileName, bool isShadow, Dictionary<(byte, byte), ObjectEntry> objectEntries)
        {
            BinaryReader tableReader = new BinaryReader(new FileStream(fileName, FileMode.Open));

            List<TableEntry> tableEntries = new List<TableEntry>();

            int amount;
            if (isShadow)
            {
                tableReader.BaseStream.Position = 4;
                amount = tableReader.ReadInt32();
            }
            else
            {
                amount = (int)tableReader.BaseStream.Length / 20;
            }

            for (int i = 0; i < amount; i++)
            {
                TableEntry TemporaryEntry;
                if (isShadow)
                    TemporaryEntry = ReadShadowTableEntry(tableReader, objectEntries);
                else
                    TemporaryEntry = ReadHeroesTableEntry(tableReader, objectEntries);

                tableEntries.Add(TemporaryEntry);
            }

            tableReader.Close();

            return tableEntries;
        }

        private static TableEntry ReadHeroesTableEntry(BinaryReader tableReader, Dictionary<(byte, byte), ObjectEntry> objectEntries)
        {
            TableEntry temporaryEntry = new TableEntry
            {
                values0 = Switch(tableReader.ReadUInt32()),
                values1 = Switch(tableReader.ReadUInt32()),
                values2 = Switch(tableReader.ReadUInt32())
            };
            tableReader.BaseStream.Position += 6;

            byte objList = tableReader.ReadByte();
            byte objType = tableReader.ReadByte();

            if (objectEntries.ContainsKey((objList, objType)))
                temporaryEntry.objectEntry = objectEntries[(objList, objType)];
            else
                temporaryEntry.objectEntry = new ObjectEntry()
                {
                    Type = objType,
                    List = objList,
                    Name = "Unknown/Unused"
                };

            return temporaryEntry;
        }

        private static TableEntry ReadShadowTableEntry(BinaryReader tableReader, Dictionary<(byte, byte), ObjectEntry> objectEntries)
        {
            TableEntry temporaryEntry = new TableEntry();

            byte objType = tableReader.ReadByte();
            byte objList = tableReader.ReadByte();
            tableReader.ReadInt16();
            temporaryEntry.values0 = Switch(tableReader.ReadUInt32());
            temporaryEntry.values1 = Switch(tableReader.ReadUInt32());

            if (objectEntries.ContainsKey((objList, objType)))
                temporaryEntry.objectEntry = objectEntries[(objList, objType)];
            else
                temporaryEntry.objectEntry = new ObjectEntry()
                {
                    Type = objType,
                    List = objList,
                    Name = "Unknown/Unused"
                };

            return temporaryEntry;
        }

        public static void SaveTable(string fileName, bool isShadow, List<TableEntry> tableEntries)
        {
            BinaryWriter TableWriter = new BinaryWriter(new FileStream(fileName, FileMode.Create));

            if (isShadow)
                WriteShadowTable(TableWriter, tableEntries);
            else
                WriteHeroesTable(TableWriter, tableEntries);

            TableWriter.Close();
        }

        private static void WriteHeroesTable(BinaryWriter tableWriter, List<TableEntry> tableEntries)
        {
            foreach (TableEntry i in tableEntries)
            {
                tableWriter.Write(Switch(i.values0));
                tableWriter.Write(Switch(i.values1));
                tableWriter.Write(Switch(i.values2));
                tableWriter.Write(new byte[6]);
                tableWriter.Write(i.objectEntry.List);
                tableWriter.Write(i.objectEntry.Type);
            }
        }

        public static void WriteShadowTable(BinaryWriter tableWriter, List<TableEntry> tableEntries)
        {
            tableWriter.Write(0);
            tableWriter.Write(tableEntries.Count);

            foreach (TableEntry i in tableEntries)
            {
                tableWriter.Write(i.objectEntry.Type);
                tableWriter.Write(i.objectEntry.List);
                tableWriter.Write((short)0);
                tableWriter.Write(Switch(i.values0));
                tableWriter.Write(Switch(i.values1));
            }
        }
    }
}
