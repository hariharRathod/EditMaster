
public enum ToolsState
{
    Select,
    Erase,
    Cut,
    Patch,
    none
}

public static class ToolsManager 
{
    public static ToolsState CurrentToolState;
}
