using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolsCanvasController : MonoBehaviour
{

    [SerializeField] private List<Image> buttonImageList;
    [SerializeField] private List<GameObject> toolsButtonsList;
    [SerializeField] private GameObject toolsPanelParent;

    private int currentToolIndex;

    private void OnEnable()
    {
        GameEvents.CutDoneAccurately += OnCutDoneAccurately;
        GameEvents.EditCorrect += OnEditCorrect;
        GameEvents.EditIncorrect += OnEditInCorrect;

    }

    private void OnDisable()
    {
        GameEvents.CutDoneAccurately -= OnCutDoneAccurately;
        GameEvents.EditCorrect -= OnEditCorrect;
        GameEvents.EditIncorrect -= OnEditInCorrect;
       
    }

    
    private void Awake()
    {
        DeactivateAllToolsButtons();
    }


    private void Start()
    {
       SetToolsButtonsOrder();
    }

    private void DeactivateAllToolsButtons()
    {
        for (int i = 0; i < toolsButtonsList.Count; i++)
        {
            toolsButtonsList[i].SetActive(false);
        }
    }


    public void OnSelectToolPressed()
    {
        currentToolIndex = 0;
        print("on select pressed");
        InputHandler.AssignNewState(InputState.Idle);
        ToolsManager.CurrentToolState = ToolsState.Select;
        GameEvents.InvokeOnSelectToolSelected();
        ColorButtonImage();
        
        if(AudioManager.instance)
            AudioManager.instance.Play("ButtonPress");
                
        
    }

    public void OnMagicEraseToolPressed()
    {
        currentToolIndex = 1;
        InputHandler.AssignNewState(InputState.Idle);
        ToolsManager.CurrentToolState = ToolsState.Erase;
        GameEvents.InvokeOnEraserToolSelected();
        ColorButtonImage();
        
        if(AudioManager.instance)
            AudioManager.instance.Play("ButtonPress");
    }

    public void OnCutToolPressed()
    {
        currentToolIndex = 2;
        InputHandler.AssignNewState(InputState.Idle);
        ToolsManager.CurrentToolState = ToolsState.Cut;
        GameEvents.InvokeOnCutToolSelected();
        ColorButtonImage();
        
        if(AudioManager.instance)
            AudioManager.instance.Play("ButtonPress");
    }

    public void OnBackgroundOptionsToolPressed()
    {
        currentToolIndex = 3;
        InputHandler.AssignNewState(InputState.Idle);
        ToolsManager.CurrentToolState = ToolsState.BackgroundChange;
        GameEvents.InvokeOnBackgroundChangeToolSelected();
        ColorButtonImage();
        
        if(AudioManager.instance)
            AudioManager.instance.Play("ButtonPress");

    }
    
    public void OnMoveToolPressed()
    {
        currentToolIndex = 6;
        print("on select pressed");
        InputHandler.AssignNewState(InputState.Idle);
        ToolsManager.CurrentToolState = ToolsState.Move;
        GameEvents.InvokeOnMoveToolSelected();
        ColorButtonImage();
        
        if(AudioManager.instance)
            AudioManager.instance.Play("ButtonPress");
        
    }
    
    public void OnScaleToolPressed()
    {
        currentToolIndex = 7;
        InputHandler.AssignNewState(InputState.Idle);
        ToolsManager.CurrentToolState = ToolsState.Scale;
        GameEvents.InvokeOnScaleToolSelected();
        ColorButtonImage();
        
        if(AudioManager.instance)
            AudioManager.instance.Play("ButtonPress");
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


    public void EnableToolButton(int toolIndex)
    {
        print("Enable index: " + toolIndex);
        toolsButtonsList[toolIndex].SetActive(true);
    }

    private void SetToolsButtonsOrder()
    {
        for (int i = 0; i < GameFlowController.only.ToolsActivationOrder.Count; i++)
        {
            int toolIndex = GameFlowController.only.ToolsActivationOrder[i];
            toolsButtonsList[toolIndex].transform.SetSiblingIndex(i);
        }

        if (GameFlowController.GameStepByStepProgressionController.EnableStepByStepProgression) return;

        for (int i = 0; i < GameFlowController.only.ToolsActivationOrder.Count; i++)
        {
            int toolIndex = GameFlowController.only.ToolsActivationOrder[i];
            EnableToolButton(toolIndex);
        }
    }
    
    private void OnEditInCorrect()
    {
       toolsPanelParent.SetActive(false);
    }

    private void OnEditCorrect()
    {
        toolsPanelParent.SetActive(false);
    }


}
