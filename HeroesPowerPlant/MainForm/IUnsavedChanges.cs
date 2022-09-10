namespace HeroesPowerPlant;

public interface IUnsavedChanges
{
    public bool UnsavedChanges { get; }
    public string Text { get; }
    public void Save();
}