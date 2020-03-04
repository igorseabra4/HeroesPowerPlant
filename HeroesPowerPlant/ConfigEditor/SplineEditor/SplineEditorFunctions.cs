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
            SplineVertex[] Points = new SplineVertex[2];
            Points[0] = new SplineVertex(0, 0, 0);
            Points[1] = new SplineVertex(0, 0, 0);
            SplineList.Add(new Spline { Points = Points });
            SplineList.Last().SetRenderStuff(Program.MainForm.renderer);
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

        public string GetSelectedType()
        {
            return SplineList[CurrentlySelectedObject].Type.ToString();
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

        public void ViewHere()
        {
            if (CurrentlySelectedObject != -1 & CurrentlySelectedObject < SplineList.Count)
                Program.MainForm.renderer.Camera.SetPosition(
                    new SharpDX.Vector3(
                    SplineList[CurrentlySelectedObject].Points[0].Position.X,
                    SplineList[CurrentlySelectedObject].Points[0].Position.Y,
                    SplineList[CurrentlySelectedObject].Points[0].Position.Z)
                    - 200 * Program.MainForm.renderer.Camera.GetForward());
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
                LoadSplinesFromJson();
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
            var result = vertices.Select(x => new SplineVertex(x.Position.X, x.Position.Y, x.Position.Z)).ToArray();
            for (int x = 0; x < result.Length - 1; x++)
            {
                result[x].DistanceToNextVertex = result[x].GetDistance(result[x + 1]);
                result[x].Pitch = (ushort) result[x].GetPitch(result[x + 1]);
            }

            return result;
        }

        public void SaveJson()
        {            
            var splines = SplineList.Select(x => new ManagedSpline(ToSplineType(x.Type), ToSplineVertexArray(x.Points))).ToArray();
            var splineFile = new SplineFile(splines);
            JsonSerializable<SplineFile>.ToPath(splineFile, splineJsonPath);
        }

        private void LoadSplinesFromJson()
        {
            foreach (var spline in JsonSerializable<SplineFile>.FromPath(splineJsonPath).Splines)
                SplineList.Add(new Spline() { Points = spline.Vertices, Type = ToSplineType(spline.SplineType) });
        }
    }
}
