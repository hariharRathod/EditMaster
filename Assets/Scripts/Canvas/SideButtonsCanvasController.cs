using System;
using UnityEngine;
using UnityEngine.UI;

public class SideButtonsCanvasController : MonoBehaviour
{
    [Tooltip("CutTool Buttons")] [SerializeField]
    private Button cutDoneButton, cutClearButton;

    private void OnEnable()
    {
        GameEvents.CutToolSelected += OnCutToolSelected;
        
    }

    private void OnDisable()
    {
        GameEvents.CutToolSelected -= OnCutToolSelected;
    }

    private void OnCutToolSelected()
    {
        
    }
}
