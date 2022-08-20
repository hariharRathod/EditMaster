using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolsCanvasController : MonoBehaviour
{

    [SerializeField] private List<Image> buttonImageList;

    private int currentToolIndex;
    
    
    public void OnSelectToolPressed()
    {
        currentToolIndex = 0;
        print("on select pressed");
        ToolsManager.CurrentToolState = ToolsState.Select;
        ColorButtonImage();
                
        
    }

    public void OnMagicEraseToolPressed()
    {
        currentToolIndex = 1;
        ToolsManager.CurrentToolState = ToolsState.Erase;
        ColorButtonImage();
    }


    private void ColorButtonImage()
    {
        for (int i = 0; i < buttonImageList.Count; i++)
        {
            if (i==currentToolIndex)
            {
                buttonImageList[i].color = Color.green;
            }
            else
            {
                buttonImageList[i].color = Color.white;
            }
        }
    }


}
