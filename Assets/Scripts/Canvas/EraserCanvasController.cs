using UnityEngine;

public class EraserCanvasController : SideButtonsCanvasController
{
    [SerializeField] private GameObject eraserInstructionButton;
    
    
    private void OnEnable()
    {        
        GameEvents.EraserToolSelected += OnEraserToolSelected;
        GameEvents.PicturePrefabInstantiateDone += OnPicturePrefabInstantiateDone;
    }

    private void OnDisable()
    {
        GameEvents.EraserToolSelected -= OnEraserToolSelected;
        GameEvents.PicturePrefabInstantiateDone -= OnPicturePrefabInstantiateDone;
    }

    private void OnEraserToolSelected()
    {
        SideButtonInAnimation(eraserInstructionButton);
    }
    
    private void OnPicturePrefabInstantiateDone(GameObject obj)
    {
        
    }
}
