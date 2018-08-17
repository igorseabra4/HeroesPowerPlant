namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1189_GlowEffectSpider : SetObjectManagerHeroes
    {
        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (MiscSettings[7] <= 3)
                if (DFFRenderer.DFFModels.ContainsKey(modelNames[MiscSettings[7]]))
                    DFFRenderer.DFFModels[modelNames[MiscSettings[7]]].Render();
                else
                    DrawCube(isSelected);
            else
                DrawCube(isSelected);
        }
    }
}