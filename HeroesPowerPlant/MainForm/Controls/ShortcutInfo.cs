namespace HeroesPowerPlant.MainForm;

public class ShortcutInfo
{
    public string Input { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public KeyboardAction? EditorAction;
    public bool IsPreset;

    public ShortcutInfo(string input, string description, string category, KeyboardAction? editorAction = null)
    {
        Input = input;
        Description = description;
        Category = category;
        EditorAction = editorAction;
    }

    public ShortcutInfo(string description, string category, KeyboardAction editorAction)
    {
        Input = null;
        Description = description;
        Category = category;
        EditorAction = editorAction;
    }
}
