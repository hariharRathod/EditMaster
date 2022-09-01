
public enum ToolsState
{
    Select,
    Erase,
    Cut,
    BackgroundChange,
    ClothsChange,
    HairChange,
    Move,
    none
}

public static class ToolsManager 
{
    public static ToolsState CurrentToolState;
}
