using HeroesPowerPlant.LayoutEditor;
using HeroesPowerPlant.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.IO;

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
            var tableEntries = new List<TableEntry>();

            if (isShadow)
            {
                using var reader = new EndianBinaryReader(new FileStream(fileName, FileMode.Open), Endianness.Little);
                reader.BaseStream.Position = 4;
                var amount = reader.ReadInt32();
                for (int i = 0; i < amount; i++)
                    tableEntries.Add(ReadShadowTableEntry(reader, objectEntries));
            }
            else
            {
                using var reader = new EndianBinaryReader(new FileStream(fileName, FileMode.Open), Endianness.Big);
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                    tableEntries.Add(ReadHeroesTableEntry(reader, objectEntries));
            }


            return tableEntries;
        }

        private static TableEntry ReadHeroesTableEntry(EndianBinaryReader reader, Dictionary<(byte, byte), ObjectEntry> objectEntries)
        {
            var entry = new TableEntry
            {
                values0 = reader.ReadUInt32(),
                values1 = reader.ReadUInt32(),
                values2 = reader.ReadUInt32()
            };
            reader.BaseStream.Position += 6;

            byte objList = reader.ReadByte();
            byte objType = reader.ReadByte();

            if (objectEntries.ContainsKey((objList, objType)))
                entry.objectEntry = objectEntries[(objList, objType)];
            else
                entry.objectEntry = new ObjectEntry()
                {
                    Type = objType,
                    List = objList,
                    Name = "Unknown/Unused"
                };

            return entry;
        }

        private static TableEntry ReadShadowTableEntry(EndianBinaryReader tableReader, Dictionary<(byte, byte), ObjectEntry> objectEntries)
        {
            var entry = new TableEntry();

            byte objType = tableReader.ReadByte();
            byte objList = tableReader.ReadByte();
            tableReader.ReadInt16();
            entry.values0 = tableReader.ReadUInt32();
            entry.values1 = tableReader.ReadUInt32();

            if (objectEntries.ContainsKey((objList, objType)))
                entry.objectEntry = objectEntries[(objList, objType)];
            else
                entry.objectEntry = new ObjectEntry()
                {
                    Type = objType,
                    List = objList,
                    Name = "Unknown/Unused"
                };
 
            return entry;
        }

        public static void SaveTable(string fileName, bool isShadow, List<TableEntry> tableEntries)
        {
            if (isShadow)
            {
                using var TableWriter = new EndianBinaryWriter(new FileStream(fileName, FileMode.Create), Endianness.Little);
                WriteShadowTable(TableWriter, tableEntries);
            } else
            {
                using var TableWriter = new EndianBinaryWriter(new FileStream(fileName, FileMode.Create), Endianness.Big);
                WriteHeroesTable(TableWriter, tableEntries);
            }
        }

        private static void WriteHeroesTable(EndianBinaryWriter tableWriter, List<TableEntry> tableEntries)
        {
            foreach (TableEntry i in tableEntries)
            {
                tableWriter.Write(i.values0);
                tableWriter.Write(i.values1);
                tableWriter.Write(i.values2);
                tableWriter.Pad(6);
                tableWriter.Write(i.objectEntry.List);
                tableWriter.Write(i.objectEntry.Type);
            }
        }

        public static void WriteShadowTable(EndianBinaryWriter tableWriter, List<TableEntry> tableEntries)
        {
            tableWriter.Write(0);
            tableWriter.Write(tableEntries.Count);

            foreach (TableEntry i in tableEntries)
            {
                tableWriter.Write(i.objectEntry.Type);
                tableWriter.Write(i.objectEntry.List);
                tableWriter.Write((short)0);
                tableWriter.Write(i.values0);
                tableWriter.Write(i.values1);
            }
        }
    }
}
