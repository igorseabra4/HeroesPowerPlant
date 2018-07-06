using System;
using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0584_GiantDice : Object_HeroesDefault
    {
        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (MiscSettings[7] <= 1)
                if (DFFRenderer.DFFStream.ContainsKey(modelNames[MiscSettings[7]]))
                    DFFRenderer.DFFStream[modelNames[MiscSettings[7]]].Render();
                else
                    DrawCube(Matrix.Scaling(5) * transformMatrix, isSelected);
            else
                DrawCube(Matrix.Scaling(5) * transformMatrix, isSelected);
        }
    }
}