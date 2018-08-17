namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0584_GiantDice : Object_HeroesDefault
    {
        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (MiscSettings[7] <= 1)
                if (DFFRenderer.DFFModels.ContainsKey(modelNames[MiscSettings[7]]))
                    DFFRenderer.DFFModels[modelNames[MiscSettings[7]]].Render();
                else
                    DrawCube(isSelected);
            else
                DrawCube(isSelected);
        }
    }
}