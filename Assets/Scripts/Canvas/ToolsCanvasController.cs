using UnityEngine;

public class ToolsCanvasController : MonoBehaviour
{
    public void OnSelectToolPressed()
    {
        print("on select pressed");
        ToolsManager.CurrentToolState = ToolsState.Select;
    }

    public void OnMagicEraseToolPressed()
    {
        ToolsManager.CurrentToolState = ToolsState.Erase;
    }
    
    
}
