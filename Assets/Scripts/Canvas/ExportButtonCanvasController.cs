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
        GameEvents.EditCorrect += OnEditCorrect;
        GameEvents.EditIncorrect += OnEditIncorrect;
    }

    private void OnDisable()
    {
        GameEvents.PicturePrefabInstantiateDone -= OnPicturePrefabInstantiateDone;
        GameEvents.ActivateDoneEditingButton -= OnActivateDoneEditingButton;
        GameEvents.EditCorrect -= OnEditCorrect;
        GameEvents.EditIncorrect -= OnEditIncorrect;
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
        
        _sideButtonsCanvasController.ActivateExportButton();
    }
    
    public void OnExportButtonPressed()
    {
        if (!_editCheckController) return;

        exportButton.interactable = false;
        
        if(AudioManager.instance)
            AudioManager.instance.Play("ButtonPress");
        
        
        _editCheckController.EditCheck();
        
        
    }

    private void OnEditIncorrect()
    {
       _sideButtonsCanvasController.SendOutExportButton();
    }

    private void OnEditCorrect()
    {
        _sideButtonsCanvasController.SendOutExportButton();
    }
    
    
}
