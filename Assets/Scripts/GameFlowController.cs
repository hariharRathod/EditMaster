using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class GameFlowController : MonoBehaviour
{
    public static GameFlowController only;
    
    public static GameStepByStepProgressionController GameStepByStepProgressionController { get; private set; }

    public static ToolsCanvasController ToolsCanvasController { get; private set; }

    public List<int> ToolsActivationOrder
    {
        get => toolsActivationOrder;
        
    }

    [SerializeField] private List<int> toolsActivationOrder;
    
    
    private void Awake()
    {
        if (!only) only = this;
        else Destroy(gameObject);

        GameStepByStepProgressionController = GetComponentInChildren<GameStepByStepProgressionController>();
        ToolsCanvasController = GameObject.FindWithTag("EditorCanvas").GetComponent<ToolsCanvasController>();
        
        DOTween.KillAll();
    }
    
    void Start()
    {
        ToolsManager.CurrentToolState = ToolsState.none;

        if (!ToolsCanvasController) return;
        
        ToolsCanvasController.EnableToolButton(GameToolsIndex.SelectToolIndex);
    }

    
}
