using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Heroes.SDK.Definitions.Structures.Stage.Splines;
using Heroes.SDK.Parsers;
using SonicHeroes.Utils.StageInjector.Common;
using SonicHeroes.Utils.StageInjector.Common.Utilities;

namespace HeroesPowerPlant.SplineEditor
{
    public class SplineEditorFunctions
    {
        private List<Spline> SplineList = new List<Spline>();
        private string splineJsonPath => Path.Combine(Path.GetDirectoryName(Program.MainForm.ConfigEditor.GetOpenConfigFileName()), "Splines.json");
        private string splineFolder => Path.Combine(Path.GetDirectoryName(Program.MainForm.ConfigEditor.GetOpenConfigFileName()), "Splines");
        private int CurrentlySelectedObject = -1;

        public void AddSplines(string[] fileNames, SharpRenderer renderer)
        {
            foreach (string i in fileNames)
            {
                Spline s = Spline.FromFile(i, renderer);

                if (s.Points.Length < 2)
                {
                    MessageBox.Show("Error: file " + i + " has less than two vertices. Skipping...");
                    continue;
                }
                SplineList.Add(s);
            }
        }

        public void AddBlankSpline()
        {
            SplineList.Add(Spline.Blank(Program.MainForm.renderer));
        }

        public void DeleteSelectedSpline()
        {
            if (CurrentlySelectedObject != -1 & CurrentlySelectedObject < SplineList.Count)
            {
                int previous = CurrentlySelectedObject;
                SplineList[CurrentlySelectedObject].Dispose();
                SplineList.RemoveAt(CurrentlySelectedObject);
            }
        }
        
        public int GetSplineCount()
        {
            return SplineList.Count;
        }
        
        public Spline GetSelected()
        {
            return SplineList[CurrentlySelectedObject];
        }

        public void SelectedIndexChanged(int selectedIndex)
        {
            if (CurrentlySelectedObject != -1 & CurrentlySelectedObject < SplineList.Count)
                SplineList[CurrentlySelectedObject].isSelected = false;

            SplineList[selectedIndex].isSelected = true;
            CurrentlySelectedObject = selectedIndex;
        }

        public void ChangeType(int type)
        {
            if (CurrentlySelectedObject != -1 & CurrentlySelectedObject < SplineList.Count)
                SplineList[CurrentlySelectedObject].Type = (SplineType)type;
        }
        
        public void SplineEditorNewConfig()
        {
            DisposeSplines();

            SplineList = new List<Spline>();
            AddBlankSpline();
        }

        public void RenderSplines(SharpRenderer renderer)
        {
            foreach (Spline s in SplineList) s.Render(renderer);
        }

        public void ViewHere(int pointIndex)
        {
            if (pointIndex < 0 || pointIndex >= SplineList[CurrentlySelectedObject].Points.Length) 
                pointIndex = 0;

            if (CurrentlySelectedObject != -1 & CurrentlySelectedObject < SplineList.Count)
                Program.MainForm.renderer.Camera.SetPosition(
                    new SharpDX.Vector3(
                    SplineList[CurrentlySelectedObject].Points[pointIndex].Position.X,
                    SplineList[CurrentlySelectedObject].Points[pointIndex].Position.Y,
                    SplineList[CurrentlySelectedObject].Points[pointIndex].Position.Z)
                    - 200 * Program.MainForm.renderer.Camera.GetForward());
        }

        public void AutoPitchPoint(int pointIndex)
        {
            if (pointIndex >= 0 && pointIndex < SplineList[CurrentlySelectedObject].Points.Length - 1)
                SplineList[CurrentlySelectedObject].Points[pointIndex].Pitch = (ushort)SplineList[CurrentlySelectedObject].Points[pointIndex].GetPitch(SplineList[CurrentlySelectedObject].Points[pointIndex + 1]);
        }

        public void AutoPitchSpline()
        {
            AutoPitchSpline(CurrentlySelectedObject);
        }

        public void AutoPitchSpline(int index)
        {
            if (index != -1 && index < SplineList.Count)
            for (int x = 0; x < SplineList[index].Points.Length - 1; x++)
                SplineList[index].Points[x].Pitch = (ushort)SplineList[index].Points[x].GetPitch(SplineList[index].Points[x + 1]);
        }

        public void AutoPitchAll()
        {
            for (int x = 0; x < SplineList.Count; x++)
                AutoPitchSpline(x);
        }

        public void DisposeSplines()
        {
            foreach (Spline s in SplineList)
                s.Dispose();
        }

        public void SplineEditorOpenConfig(SharpRenderer renderer)
        {
            DisposeSplines();
            SplineList = new List<Spline>();
            
            if (File.Exists(splineJsonPath))
                LoadSplinesFromJson(renderer);
            else if (Directory.Exists(splineFolder))
            {
                MessageBox.Show("Note: Splines loaded from Splines Folder (Reloaded I Stage Injector: legacy), not JSON (Reloaded II Stage Injector)");

                foreach (string i in Directory.GetFiles(splineFolder))
                    if (Path.GetExtension(i) == ".obj")
                        SplineList.Add(Spline.FromFile(i, renderer));
            }

            if (Directory.Exists(splineFolder))
            {
                DialogResult result = MessageBox.Show("Splines Folder (Reloaded I Stage Injector) found. Do you wish to delete this folder and keep only a Splines.json file (Reloaded II Stage Injector)?", "Legacy splines found", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    foreach (var s in Directory.GetFiles(splineFolder))
                        File.Delete(s);
                    Directory.Delete(splineFolder);
                    SaveJson();
                }
            }
        }

        internal void AddSpline(List<SplineVertex> vertices, Heroes.SDK.Definitions.Structures.Stage.Splines.SplineType splineType, SharpRenderer renderer)
        {
            Spline s = new Spline(vertices.ToArray(), ToSplineType(splineType), renderer);
            SplineList.Add(s);
        }

        /* Save Json */
        private Heroes.SDK.Definitions.Structures.Stage.Splines.SplineType ToSplineType(SplineType type)
            => type switch
            {
                SplineType.Null => Heroes.SDK.Definitions.Structures.Stage.Splines.SplineType.Loop,
                SplineType.Loop => Heroes.SDK.Definitions.Structures.Stage.Splines.SplineType.Loop,
                SplineType.Rail => Heroes.SDK.Definitions.Structures.Stage.Splines.SplineType.Rail,
                SplineType.Ball => Heroes.SDK.Definitions.Structures.Stage.Splines.SplineType.Ball,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
            };

        private SplineType ToSplineType(Heroes.SDK.Definitions.Structures.Stage.Splines.SplineType type)
            => type switch
            {
                Heroes.SDK.Definitions.Structures.Stage.Splines.SplineType.Loop => SplineType.Loop,
                Heroes.SDK.Definitions.Structures.Stage.Splines.SplineType.Rail => SplineType.Rail,
                Heroes.SDK.Definitions.Structures.Stage.Splines.SplineType.Ball => SplineType.Ball,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
            };
        

        private SplineVertex[] ToSplineVertexArray(SplineVertex[] vertices)
        {
            for (int x = 0; x < vertices.Length - 1; x++)
                vertices[x].DistanceToNextVertex = vertices[x].GetDistance(vertices[x + 1]);
            
            return vertices;
        }

        public void SaveJson()
        {            
            var splines = SplineList.Select(x => new ManagedSpline(ToSplineType(x.Type), ToSplineVertexArray(x.Points))).ToArray();
            var splineFile = new SplineFile(splines);
            JsonSerializable<SplineFile>.ToPath(splineFile, splineJsonPath);
        }

        private void LoadSplinesFromJson(SharpRenderer renderer)
        {
            foreach (var spline in JsonSerializable<SplineFile>.FromPath(splineJsonPath).Splines)
                SplineList.Add(new Spline(spline.Vertices, ToSplineType(spline.SplineType), renderer));
        }

        internal void ExportOBJ(string fileName)
        {
            using StreamWriter writer = new StreamWriter(new FileStream(fileName, FileMode.Create));
            {
                writer.WriteLine("# Exported by Heroes Power Plant");
                foreach (var v in SplineList[CurrentlySelectedObject].Points)
                    writer.WriteLine($"v {v.Position.X} {v.Position.Y} {v.Position.Z} {v.Roll} {v.Pitch}");
                string l = "l ";
                for (int i = 0; i < SplineList[CurrentlySelectedObject].Points.Length; i++)
                    l += $"{i} ";
                writer.WriteLine(l);
            }
        }
    }
}
