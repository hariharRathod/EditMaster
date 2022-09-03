using UnityEngine;
using UnityEngine.UI;

public class ExportButtonCanvasController : MonoBehaviour
{
    private SideButtonsCanvasController _sideButtonsCanvasController;
    private EditCheckController _editCheckController;
    [SerializeField] private Button exportButton;
    
    private void OnEnable()
    {        
        GameEvents.PicturePrefabInstantiateDone += OnPicturePrefabInstantiateDone;
        GameEvents.ActivateDoneEditingButton += OnActivateDoneEditingButton;
    }

    private void OnDisable()
    {
        GameEvents.PicturePrefabInstantiateDone -= OnPicturePrefabInstantiateDone;
        GameEvents.ActivateDoneEditingButton -= OnActivateDoneEditingButton;
    }

    private void Start()
    {
        _sideButtonsCanvasController = GetComponent<SideButtonsCanvasController>();
    }
    
    private void OnPicturePrefabInstantiateDone(GameObject obj)
    {
        if (!obj.TryGetComponent(out EditCheckController editCheckController)) return;

       _editCheckController = editCheckController;
       
    }
    
    
    private void OnActivateDoneEditingButton()
    {
        if(GameFlowController.only.LevelType != LevelType.MoveAndErase) return;
        
        _sideButtonsCanvasController.ActivateMoveAndEraseTypeDoneEditingButton();
    }
    
    public void OnExportButtonPressed()
    {
        if (!_editCheckController) return;

        exportButton.interactable = false;
        
        _editCheckController.EditCheck();
    }

}
