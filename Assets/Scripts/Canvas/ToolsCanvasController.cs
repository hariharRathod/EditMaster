using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolsCanvasController : MonoBehaviour
{

    [SerializeField] private List<Image> buttonImageList;

    private int currentToolIndex;

    private void OnEnable()
    {
        GameEvents.CutDoneAccurately += OnCutDoneAccurately;
    }

    private void OnDisable()
    {
        GameEvents.CutDoneAccurately -= OnCutDoneAccurately;
    }

    


    public void OnSelectToolPressed()
    {
        currentToolIndex = 0;
        print("on select pressed");
        InputHandler.AssignNewState(InputState.Idle);
        ToolsManager.CurrentToolState = ToolsState.Select;
        GameEvents.InvokeOnSelectToolSelected();
        ColorButtonImage();
                
        
    }

    public void OnMagicEraseToolPressed()
    {
        currentToolIndex = 1;
        InputHandler.AssignNewState(InputState.Idle);
        ToolsManager.CurrentToolState = ToolsState.Erase;
        GameEvents.InvokeOnEraserToolSelected();
        ColorButtonImage();
    }

    public void OnCutToolPressed()
    {
        currentToolIndex = 2;
        InputHandler.AssignNewState(InputState.Idle);
        ToolsManager.CurrentToolState = ToolsState.Cut;
        GameEvents.InvokeOnCutToolSelected();
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
    
    private void OnCutDoneAccurately()
    {
        currentToolIndex = -1;
        ColorButtonImage();
    }


}
