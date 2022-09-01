using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EraserCanvasController : MonoBehaviour
{
   
    [SerializeField] private TextMeshProUGUI headTextIns, headImageTextIns;
    [SerializeField] private Image headImage;



    private SideButtonsCanvasController _sideButtonsCanvasController;
    private MagicEraserLevelController _eraserLevelController;
    
    
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
        if (!obj.TryGetComponent(out MagicEraserLevelController eraserLevelController)) return;

        _eraserLevelController = eraserLevelController;
        
        SetupHeadInstruction(eraserLevelController);
        
    }

    private void SetupHeadInstruction(MagicEraserLevelController eraserLevelController)
    {
        if (!eraserLevelController.HasHeadInstruction) return;

        if (eraserLevelController.IsTextInstruction)
        {
            headTextIns.text = eraserLevelController.TextHeadInstruction;
            headTextIns.gameObject.SetActive(true);
            return;
        }

        if (eraserLevelController.IsImageInstruction)
        {
            headImageTextIns.text = eraserLevelController.ImageHeadInstruction;
            headImageTextIns.gameObject.SetActive(true);
        }
      
    }
    
    private void OnActivateDoneEditingButton()
    {
        
        print("done editing eraser side in");
        
        if(GameFlowController.only.LevelType != LevelType.MagicEraserType) return;
        
        _sideButtonsCanvasController.ActivateEraserTypeDoneEditingButton();
       
       print("done editing eraser side in");
        
    }

    public void OnDoneEditingButtonPressed()
    {
        if (!_eraserLevelController) return;
        
        _eraserLevelController.CheckMagicEraser();
    }
}
