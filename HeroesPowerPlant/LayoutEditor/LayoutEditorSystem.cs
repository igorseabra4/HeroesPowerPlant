using HeroesPowerPlant.Shared.Utilities;
using Newtonsoft.Json;
using SharpDX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static HeroesPowerPlant.LayoutEditor.LayoutEditorFunctions;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.LayoutEditor
{
    public class LayoutEditorSystem
    {
        private static bool isShadow;
        public bool IsShadow => isShadow;

        private string currentlyOpenFileName;
        public string CurrentlyOpenFileName => currentlyOpenFileName;

        public static Dictionary<(byte, byte), ObjectEntry> heroesObjectEntries { get; private set; }
        public static Dictionary<(byte, byte), ObjectEntry> shadowObjectEntries { get; private set; }
        public static ObjectEntry heroesObjectEntry(byte List, byte Type) => heroesObjectEntries[(List, Type)];
        public static ObjectEntry shadowObjectEntry(byte List, byte Type) => shadowObjectEntries[(List, Type)];

        public bool autoUnkBytes;

        public bool UnsavedChanges = false;

        public static void SetupLayoutEditorSystem()
        {
            heroesObjectEntries = ReadObjectListData(Application.StartupPath + "/Resources/Lists/HeroesObjectList.ini");
            shadowObjectEntries = ReadObjectListData(Application.StartupPath + "/Resources/Lists/ShadowObjectList.ini");

            string extraObjectEntriesPath = Application.StartupPath + "/Resources/Lists/HeroesObjectListCustom.ini";
            if (File.Exists(extraObjectEntriesPath))
            {
                var dict = ReadObjectListData(extraObjectEntriesPath);
                foreach (var k in dict.Keys)
                    heroesObjectEntries.Add(k, dict[k]);
            }
        }

        public void BindControl(ListControl listControl)
        {
            listControl.DataSource = setObjects;
        }

        private BindingList<SetObject> setObjects { get; set; } = new BindingList<SetObject>();

        public int GetSetObjectAmount()
        {
            return setObjects.Count;
        }

        public SetObject GetSetObjectAt(int index)
        {
            return setObjects[index];
        }

        public static IEnumerable<ObjectEntry> GetAllObjectEntries()
        {
            List<ObjectEntry> list = new List<ObjectEntry>();
            list.AddRange(heroesObjectEntries.Values);
            list.AddRange(shadowObjectEntries.Values);

            return list;
        }

        public static ObjectEntry[] GetActiveObjectEntries()
        {
            if (isShadow)
                return shadowObjectEntries.Values.ToArray();
            return heroesObjectEntries.Values.ToArray();
        }

        public (byte, byte)[] GetAllCurrentObjectEntries()
        {
            HashSet<(byte, byte)> objectEntries = new HashSet<(byte, byte)>();

            foreach (SetObject s in setObjects)
                if (!objectEntries.Contains((s.List, s.Type)))
                    objectEntries.Add((s.List, s.Type));

            return objectEntries.ToArray();
        }

        public void NewHeroesLayout()
        {
            isShadow = false;
            currentlyOpenFileName = null;
            setObjects.Clear();
        }

        public void NewShadowLayout()
        {
            isShadow = true;
            currentlyOpenFileName = null;
            setObjects.Clear();
        }

        #region Import/Export Methods

        public void OpenLayoutFile(string fileName, out string result, ref Dictionary<(byte, byte, string), HashSet<byte[]>> miscSettingsDict)
        {
            result = "";
            currentlyOpenFileName = fileName;
            setObjects.Clear();

            if (Path.GetExtension(CurrentlyOpenFileName).ToLower() == ".bin")
            {
                isShadow = false;
                var l = GetHeroesLayout(CurrentlyOpenFileName, out result, ref miscSettingsDict);
                l.ForEach(setObjects.Add);

            }
            else if (Path.GetExtension(CurrentlyOpenFileName).ToLower() == ".dat")
            {
                isShadow = true;
                try
                {
                    GetShadowLayout(CurrentlyOpenFileName, out result).ForEach(setObjects.Add);
                }
                catch (InvalidDataException)
                {
                    // handle gracefully
                }
            }
            else
                throw new InvalidDataException("Unknown file type");
            UnsavedChanges = false;
        }

        public void Save(string fileName)
        {
            currentlyOpenFileName = fileName;
            Save();
        }

        public void Save()
        {
            if (IsShadow)
                SaveShadowLayout(setObjects.Cast<SetObjectShadow>(), CurrentlyOpenFileName, autoUnkBytes);
            else
                SaveHeroesLayout(setObjects.Cast<SetObjectHeroes>(), CurrentlyOpenFileName, autoUnkBytes);
            UnsavedChanges = false;
        }

        public void SaveINI(string fileName)
        {
            SaveHeroesLayoutINI(setObjects.Cast<SetObjectHeroes>(), fileName);
        }

        public void ImportINI(string fileName)
        {
            foreach (var v in GetHeroesLayoutFromINI(fileName))
                setObjects.Add(v);
            UnsavedChanges = true;
        }

        public void ImportLayoutFile(string fileName)
        {
            if (isShadow)
            {
                try
                {
                    GetShadowLayout(fileName, out _).ForEach(setObjects.Add);
                }
                catch (InvalidDataException)
                {
                    // cancel gracefully
                }
            }
            else
            {
                if (Path.GetExtension(fileName).ToLower() == ".bin")
                {
                    var f = new Dictionary<(byte, byte, string), HashSet<byte[]>>();
                    GetHeroesLayout(fileName, out _, ref f).ForEach(setObjects.Add);
                }
                else if (Path.GetExtension(fileName).ToLower() == ".dat")
                {
                    GetHeroesLayoutFromShadow(fileName).ForEach(setObjects.Add);
                }
                else
                    throw new InvalidDataException("Unknown file type");
            }
            UnsavedChanges = true;
        }

        public void ImportOBJ(string fileName)
        {
            GetObjectsFromObjFile(fileName, 0, 3).ForEach(setObjects.Add);
            UnsavedChanges = true;
        }

        public void ImportSALayout(string fileName)
        {
            foreach (var c in Other.OtherFunctions.ConvertSASetToHeroes(fileName))
                setObjects.Add(c);
            UnsavedChanges = true;
        }

        #endregion

        #region View/Rendering Methods

        public void ViewHere(int index)
        {
            Program.MainForm.renderer.Camera.SetPosition(GetSetObjectAt(index).Position - 200 * Program.MainForm.renderer.Camera.GetForward());
        }

        public void UpdateAllMatrices()
        {
            foreach (SetObject s in setObjects)
                s.CreateTransformMatrix();
        }

        public void RenderSetObjects(SharpRenderer renderer, bool drawEveryObject, bool renderTriggers)
        {
            foreach (SetObject s in setObjects)
                if (renderer.frustum.Intersects(ref s.boundingBox))
                {
                    if (!renderTriggers && s.IsTrigger())
                        continue;
                    s.Draw(renderer, drawEveryObject);
                }
        }

        public void UpdateSetParticleMatrices()
        {
            foreach (SetObject s in setObjects)
                if (s.List == 0x01 & s.Type == 0xFF)
                    s.CreateTransformMatrix();
        }
        #endregion

        #region Object Editing Methods

        public void SelectedIndexChanged(ListBox.SelectedIndexCollection selectedIndices)
        {
            for (int i = 0; i < setObjects.Count; i++)
                setObjects[i].isSelected = false;

            foreach (int i in selectedIndices)
                GetSetObjectAt(i).isSelected = true;
        }

        public void AddNewSetObject()
        {
            Vector3 Position = Program.MainForm.renderer.Camera.GetPosition() + 100 * Program.MainForm.renderer.Camera.GetForward();
            byte currentNum = 0;

            if (isShadow)
            {
                if (!string.IsNullOrEmpty(currentlyOpenFileName))
                {
                    if (currentlyOpenFileName.ToLower().Contains("cmn"))
                        currentNum = 0x10;
                    else if (currentlyOpenFileName.ToLower().Contains("nrm"))
                        currentNum = 0x20;
                    else if (currentlyOpenFileName.ToLower().Contains("hrd"))
                        currentNum = 0x40;
                    else if (currentlyOpenFileName.ToLower().Contains("ds1"))
                        currentNum = 0x80;
                }

                var unkBytes = new List<byte>() { 1, currentNum };
                unkBytes.AddRange(currentNum == 0x80 ? new byte[] { 0x40, 0x80 } : new byte[] { 0, 0x80 });
                unkBytes.AddRange(new byte[] { 1, currentNum });
                unkBytes.AddRange(currentNum == 0x80 ? new byte[] { 0x40, 0x80 } : new byte[] { 0, 0 });

                setObjects.Add(CreateShadowObject(0, 0, Position, Vector3.Zero, 0, 10, unkBytes.ToArray()));
            }
            else
            {
                if (!string.IsNullOrEmpty(currentlyOpenFileName))
                {
                    if (currentlyOpenFileName.Contains("P1"))
                        currentNum = 0x20;
                    else if (currentlyOpenFileName.Contains("P2"))
                        currentNum = 0x40;
                    else if (currentlyOpenFileName.Contains("P3"))
                        currentNum = 0x60;
                    else if (currentlyOpenFileName.Contains("P4"))
                        currentNum = 0x80;
                    else if (currentlyOpenFileName.Contains("P5"))
                        currentNum = 0xA0;
                }

                var unkBytes = new byte[8] { 0, 2, currentNum, 9, 0, 2, currentNum, 9 };

                setObjects.Add(CreateHeroesObject(0, 0, Position, Vector3.Zero, 0, 10, unkBytes));
            }
            UnsavedChanges = true;
        }

        public void DuplicateSetObjectAt(int index, Vector3 Position)
        {
            PasteSetObject(JsonConvert.SerializeObject(new List<SetObject> { GetSetObjectAt(index) }));
            setObjects.Last().Position = Position;
            setObjects.Last().CreateTransformMatrix();
        }

        public string SerializeSetObject(ListBox.SelectedIndexCollection selectedIndices)
        {
            var setObjs = new List<(SetObject, byte[])>();
            foreach (int i in selectedIndices)
            {
                var obj = GetSetObjectAt(i);
                setObjs.Add((obj, obj.GetMiscSettings()));
            }
            return JsonConvert.SerializeObject(setObjs);
        }

        public int PasteSetObject(string text = null)
        {
            text ??= Clipboard.GetText();
            int result = 0;
            var pasted = new List<SetObject>();
            //try
            {
                if (isShadow)
                {
                    var list = JsonConvert.DeserializeObject<List<(SetObjectShadow, byte[])>>(text);
                    foreach (var src in list)
                    {
                        var setObj = src.Item1;
                        var miscSettings = src.Item2;
                        var dest = CreateShadowObject(setObj.List, setObj.Type, setObj.Position, setObj.Rotation, setObj.Link, setObj.Rend, setObj.UnkBytes, false);
                        dest.SetMiscSettings(miscSettings);
                        dest.CreateTransformMatrix();
                        pasted.Add(dest);
                        result++;
                    }
                }
                else
                {
                    var list = JsonConvert.DeserializeObject<List<(SetObjectHeroes, byte[])>>(text);
                    foreach (var src in list)
                    {
                        var setObj = src.Item1;
                        var miscSettings = src.Item2;
                        var dest = CreateHeroesObject(setObj.List, setObj.Type, setObj.Position, setObj.Rotation, setObj.Link, setObj.Rend, setObj.UnkBytes, false);
                        dest.SetMiscSettings(miscSettings);
                        dest.CreateTransformMatrix();
                        pasted.Add(dest);
                        result++;
                    }
                }
                foreach (var obj in pasted)
                    setObjects.Add(obj);
            }
            //catch
            //{
            //    MessageBox.Show($"Error pasting objects from clipboard. Are you sure you have {(isShadow ? "Shadow The Hedgehog" : "Sonic Heroes")} objects copied?");
            //    result = 0;
            //}
            UnsavedChanges = true;
            return result;
        }

        public void RemoveSetObject(int index)
        {
            setObjects.RemoveAt(index);
        }

        public void ClearList()
        {
            setObjects.Clear();
            UnsavedChanges = true;
        }

        public void ChangeObjectType(int index, ObjectEntry newEntry)
        {
            SetObject current = GetSetObjectAt(index);

            Vector3 pos = current.Position;
            Vector3 rot = current.Rotation;
            byte link = current.Link;
            byte rend = current.Rend;
            byte[] unkb = current.UnkBytes;
            bool isSelected = current.isSelected;

            setObjects[index] = isShadow ?
                CreateShadowObject(newEntry.List, newEntry.Type, pos, rot, link, rend, unkb) :
                CreateHeroesObject(newEntry.List, newEntry.Type, pos, rot, link, rend, unkb);

            setObjects[index].isSelected = isSelected;

            UnsavedChanges = true;
        }

        public void SetObjectPosition(int index, float x, float y, float z)
        {
            SetObjectPosition(index, new Vector3(x, y, z));
        }

        public void SetObjectPosition(int index, Vector3 v)
        {
            GetSetObjectAt(index).Position = v;
            GetSetObjectAt(index).CreateTransformMatrix();
            UnsavedChanges = true;
        }

        public void SetObjectRotation(int index, float x, float y, float z)
        {
            if (isShadow)
                GetSetObjectAt(index).Rotation = new Vector3(x, y, z);
            else
                GetSetObjectAt(index).Rotation = new Vector3(DegreesToBAMS(x), DegreesToBAMS(y), DegreesToBAMS(z));
            GetSetObjectAt(index).CreateTransformMatrix();
            UnsavedChanges = true;
        }

        private void SetObjectRotationDefault(int index, Vector3 v)
        {
            GetSetObjectAt(index).Rotation = v;
            GetSetObjectAt(index).CreateTransformMatrix();
            UnsavedChanges = true;
        }

        public void SetObjectLink(int index, byte value)
        {
            GetSetObjectAt(index).Link = value;
            UnsavedChanges = true;
        }

        public void SetObjectRend(int index, byte value)
        {
            GetSetObjectAt(index).Rend = value;
            UnsavedChanges = true;
        }

        public void SetUnkBytes(int index, byte v1, byte v2, byte v3, byte v4, byte v5, byte v6, byte v7, byte v8)
        {
            GetSetObjectAt(index).UnkBytes = new byte[] { v1, v2, v3, v4, v5, v6, v7, v8 };
            UnsavedChanges = true;
        }

        public void Drop(int index)
        {
            GetSetObjectAt(index).Position = Program.MainForm.LevelEditor.bspRenderer.GetDroppedPosition(GetSetObjectAt(index).Position);
            GetSetObjectAt(index).CreateTransformMatrix();
            UnsavedChanges = true;
        }

        public void DropToCurrentView(int index)
        {
            GetSetObjectAt(index).Position = Program.MainForm.renderer.Camera.GetPosition() + 200 * Program.MainForm.renderer.Camera.GetForward();
            GetSetObjectAt(index).CreateTransformMatrix();
            UnsavedChanges = true;
        }

        public void CopyMisc(int index)
        {
            Clipboard.SetText(JsonConvert.SerializeObject(GetSetObjectAt(index).GetMiscSettings()));
        }

        public void PasteMisc(int index)
        {
            try
            {
                var obj = GetSetObjectAt(index);
                byte[] misc = JsonConvert.DeserializeObject<byte[]>(Clipboard.GetText());
                using var reader = new EndianBinaryReader(new MemoryStream(misc), Endianness.Big);

                if (obj is SetObjectHeroes objHeroes)
                    objHeroes.ReadMiscSettings(reader);
                else if (obj is SetObjectShadow objShadow)
                    objShadow.ReadMiscSettings(reader, misc.Length);

                obj.CreateTransformMatrix();
                UnsavedChanges = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error pasting misc. settings: " + ex.Message);
            }
        }

        #endregion

        #region GUI Return Methods

        public decimal GetPosX(int index)
        {
            return (decimal)GetSetObjectAt(index).Position.X;
        }

        public decimal GetPosY(int index)
        {
            return (decimal)GetSetObjectAt(index).Position.Y;
        }

        public decimal GetPosZ(int index)
        {
            return (decimal)GetSetObjectAt(index).Position.Z;
        }

        public decimal GetRotX(int index)
        {
            if (isShadow)
                return (decimal)GetSetObjectAt(index).Rotation.X;
            else
                return (decimal)BAMStoDegrees(GetSetObjectAt(index).Rotation.X);
        }

        public decimal GetRotY(int index)
        {
            if (isShadow)
                return (decimal)GetSetObjectAt(index).Rotation.Y;
            else
                return (decimal)BAMStoDegrees(GetSetObjectAt(index).Rotation.Y);
        }

        public decimal GetRotZ(int index)
        {
            if (isShadow)
                return (decimal)GetSetObjectAt(index).Rotation.Z;
            else
                return (decimal)BAMStoDegrees(GetSetObjectAt(index).Rotation.Z);
        }

        public decimal GetObjectLink(int index)
        {
            return GetSetObjectAt(index).Link;
        }

        public decimal GetObjectRend(int index)
        {
            return GetSetObjectAt(index).Rend;
        }

        public byte[] GetUnkBytes(int index)
        {
            return GetSetObjectAt(index).UnkBytes;
        }

        public void ScreenClicked(Vector3 camPos, Ray r, bool seeAllObjects, bool renderTriggers, out int index, out float smallerDistance)
        {
            index = -1;
            smallerDistance = 40000f;

            for (int i = 0; i < setObjects.Count; i++)
            {
                if (setObjects[i].isSelected || (!seeAllObjects && setObjects[i].DontDraw(camPos)))
                    continue;

                if (!renderTriggers && setObjects[i].IsTrigger())
                    continue;

                if (setObjects[i].IntersectsWith(r, out float distance))
                    if (distance < smallerDistance)
                    {
                        smallerDistance = distance;
                        index = i;
                    }
            }
        }

        public void GetClickedModelPosition(Ray ray, Vector3 camPos, bool seeAllObjects, out bool hasIntersected, out float smallestDistance)
        {
            hasIntersected = false;
            smallestDistance = 40000f;

            foreach (SetObject s in setObjects)
                if ((seeAllObjects || !s.DontDraw(camPos)) && s.IntersectsWith(ray, out float distance))
                {
                    hasIntersected = true;
                    if (distance < smallestDistance)
                        smallestDistance = distance;
                }
        }

        public int FindNext(int index)
        {
            if (index != setObjects.Count - 1)
                for (int i = index + 1; i < setObjects.Count; i++)
                    if (setObjects[i].Link == GetSetObjectAt(index).Link)
                        return i;

            for (int i = 0; i < index; i++)
                if (setObjects[i].Link == GetSetObjectAt(index).Link)
                    return i;

            return index;
        }
        #endregion

        #region Sorting Methods
        public void SortObjectsByID()
        {
            List<SetObject> sorted = setObjects.OrderBy(f => f.Link).ToList();
            sorted = sorted.OrderBy(f => f.Type).ToList();
            sorted = sorted.OrderBy(f => f.List).ToList();
            setObjects.Clear();
            sorted.ForEach(setObjects.Add);
            UnsavedChanges = true;
        }

        public void SortObjectsByDistance()
        {
            List<SetObject> sorted = setObjects.OrderBy(f => f.GetDistanceFrom()).ToList();
            setObjects.Clear();
            sorted.ForEach(setObjects.Add);
            UnsavedChanges = true;
        }

        public void SortObjectsAlphabetical()
        {
            List<SetObject> sorted = setObjects.OrderBy(f => f.GetName).ToList();
            setObjects.Clear();
            sorted.ForEach(setObjects.Add);
            UnsavedChanges = true;
        }
        #endregion

        #region Memory Functions

        public bool GetSpeedMemory(int index)
        {
            if (MemoryFunctions.TryAttach())
            {
                SetObjectPosition(index, MemoryFunctions.GetPlayer0Position());
                return true;
            }
            return false;
        }

        public bool GetFlyMemory(int index)
        {
            if (MemoryFunctions.TryAttach())
            {
                SetObjectPosition(index, MemoryFunctions.GetPlayer1Position());
                return true;
            }
            return false;
        }

        public bool GetPowMemory(int index)
        {
            if (MemoryFunctions.TryAttach())
            {
                SetObjectPosition(index, MemoryFunctions.GetPlayer2Position());
                return true;
            }
            return false;
        }

        public bool GetSpeedRotMemory(int index)
        {
            if (MemoryFunctions.TryAttach())
            {
                SetObjectRotationDefault(index, MemoryFunctions.GetPlayer0Rotation());
                return true;
            }
            return false;
        }

        public bool GetFlyRotMemory(int index)
        {
            if (MemoryFunctions.TryAttach())
            {
                SetObjectRotationDefault(index, MemoryFunctions.GetPlayer1Rotation());
                return true;
            }
            return false;
        }

        public bool GetPowRotMemory(int index)
        {
            if (MemoryFunctions.TryAttach())
            {
                SetObjectRotationDefault(index, MemoryFunctions.GetPlayer2Rotation());
                return true;
            }
            return false;
        }

        public bool Teleport(int index)
        {
            return MemoryFunctions.Teleport(GetSetObjectAt(index).Position);
        }
        #endregion

        public bool Equals(LayoutEditorSystem system)
        {
            if (setObjects.Count != system.setObjects.Count)
                return false;
            for (int i = 0; i < setObjects.Count; i++)
            {
                if (!setObjects[i].Equals(system.setObjects[i]))
                    return false;
            }
            return true;
        }

        public (LayoutEditorSystem, LayoutEditorSystem, string) Diff(LayoutEditorSystem system)
        {
            List<int> indexes_I_ToDelete = new List<int>();
            List<int> indexes_J_ToDelete = new List<int>();
            string log = system.currentlyOpenFileName + " Differences \n\n--\n";

            // find if match exists at different indexes
            for (int i = 0; i < system.setObjects.Count; i++)
            {
                for (int j = 0; j < setObjects.Count; j++)
                {
                    if (system.setObjects[i].Equals(setObjects[j]))
                    {
                        indexes_I_ToDelete.Add(i);
                        indexes_J_ToDelete.Add(j);
                        break;
                    }
                    if (j == setObjects.Count - 1)
                    {
                        log += system.setObjects[i].ToString() + " at index " + i + '\n';
                    }
                }
            }

            log += "\n\n----\n\n" + currentlyOpenFileName + " Differences \n\n--\n";

            // do the same but the other way around
            // this is terribly inefficient but it will work; "good enough for a beta"
            for (int i = 0; i < setObjects.Count; i++)
            {
                for (int j = 0; j < system.setObjects.Count; j++)
                {
                    if (setObjects[i].Equals(system.setObjects[j]))
                    {
                        break;
                    }
                    if (j == system.setObjects.Count - 1)
                    {
                        log += setObjects[i].ToString() + " at index " + i + '\n';
                    }
                }
            }

            indexes_I_ToDelete.Sort();
            indexes_I_ToDelete.Reverse();
            indexes_J_ToDelete.Sort();
            indexes_J_ToDelete.Reverse();

            for (int i = 0; i < indexes_I_ToDelete.Count; i++)
            {
                system.setObjects.RemoveAt(indexes_I_ToDelete[i]);
                setObjects.RemoveAt(indexes_J_ToDelete[i]);
            }

            // now leaves us with the before and after Diff results

            return (this, system, log);
        }

        public static Dictionary<(byte, byte), string> ObjectsForModels = new Dictionary<(byte, byte), string>
        {
            { (0x15, 0x00), "en_searcher.one" },
            { (0x15, 0x10), "en_pawn.one" },
            { (0x15, 0x20), "en_capture.one" },
            { (0x15, 0x30), "en_flyer.one" },
            { (0x15, 0x40), "en_wall.one" },
            { (0x15, 0x70), "en_turtle.one" },
            { (0x15, 0x90), "en_rinoliner.one" },
            { (0x15, 0xC0), "en_magician.one" },
            { (0x15, 0xD0), "en_e2000.one" },
        };

        public List<string> GetObjectsForModels()
        {
            var result = new HashSet<string>();
            foreach (var s in setObjects)
                if (ObjectsForModels.ContainsKey((s.List, s.Type)))
                    result.Add(ObjectsForModels[(s.List, s.Type)]);
            return result.ToList();
        }
    }
}
