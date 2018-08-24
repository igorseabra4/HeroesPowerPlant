namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0584_GiantDice : Object_HeroesDefault
    {
        public override void Draw(SharpRenderer renderer, string[] modelNames, bool isSelected)
        {
            if (MiscSettings[7] < modelNames.Length)
                Draw(renderer, modelNames[MiscSettings[7]], isSelected);
            else
                DrawCube(renderer, isSelected);
        }
    }
}