
public enum ToolsState
{
    Select,
    Erase,
    Cut,
    Patch,
    BackgroundChange,
    ClothsChange,
    HairChange,
    none
}

public static class ToolsManager 
{
    public static ToolsState CurrentToolState;
}
