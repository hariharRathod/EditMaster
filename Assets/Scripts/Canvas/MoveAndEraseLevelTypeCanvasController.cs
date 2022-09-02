using UnityEngine;

public class MoveAndEraseLevelTypeCanvasController : MonoBehaviour
{
    private SideButtonsCanvasController _sideButtonsCanvasController;
   
    
    
    private void OnEnable()
    {        
        
        GameEvents.ActivateDoneEditingButton += OnActivateDoneEditingButton;
    }

    private void OnDisable()
    {
        
        GameEvents.ActivateDoneEditingButton -= OnActivateDoneEditingButton;
    }

    private void Start()
    {
        _sideButtonsCanvasController = GetComponent<SideButtonsCanvasController>();
    }
    
    private void OnActivateDoneEditingButton()
    {
        if(GameFlowController.only.LevelType != LevelType.MoveAndErase) return;
        
        _sideButtonsCanvasController.ActivateMoveAndEraseTypeDoneEditingButton();
    }
    
    public void OnDoneEditingButtonPressed()
    {
        
    }

}
