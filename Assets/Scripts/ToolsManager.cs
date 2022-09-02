
public enum ToolsState
{
    Select,
    Erase,
    Cut,
    BackgroundChange,
    ClothsChange,
    HairChange,
    Move,
    Scale,
    none
}

public static class ToolsManager 
{
    public static ToolsState CurrentToolState;
}
