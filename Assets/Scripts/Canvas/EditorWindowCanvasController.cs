using DG.Tweening;
using UnityEngine;

public class EditorWindowCanvasController : MonoBehaviour
{
    [SerializeField] private RectTransform editorWindowPanelRect;
    [SerializeField] private float editorPanelInDuration;
    [SerializeField] private Ease editorPanelInEase;

    [SerializeField] private GameObject levelInstruction,footerPanel;
    
    private void OnEnable()
    {
        GameEvents.CameraZoomActionCompleted += OnCameraZoomActionCompleted;
        GameEvents.EditCorrect += OnEditCorrect;
        GameEvents.EditIncorrect += OnEditIncorrect;
    }

    private void OnDisable()
    {
        GameEvents.CameraZoomActionCompleted -= OnCameraZoomActionCompleted;
        GameEvents.EditCorrect -= OnEditCorrect;
        GameEvents.EditIncorrect -= OnEditIncorrect;
    }

    
    private void Start()
    {
       SetInitalEditorPanelScale();

    }

    private void SetInitalEditorPanelScale()
    {
        editorWindowPanelRect.transform.localScale = Vector3.zero;
        
    }


    public void SetInitalEditorPanelPos()
    {
        float yPos= -Screen.width - 50f;
        var pos = editorWindowPanelRect.anchoredPosition;
        pos.x = yPos;
        editorWindowPanelRect.anchoredPosition = pos;
    }
    
    
    private void OnCameraZoomActionCompleted()
    {
        //editorWindowPanelRect.DOAnchorPos(Vector2.zero, editorPanelInDuration).SetEase(editorPanelInEase);
        editorWindowPanelRect.transform.DOScale(Vector3.one, editorPanelInDuration).SetEase(editorPanelInEase);
    }

    private void DisableLevelInstruction()
    {
        levelInstruction.SetActive(false);
    }

    private void DisableFooterPanel()
    {
        footerPanel.SetActive(false);
    }

    private void OnEditCorrect()
    {
       DisableLevelInstruction();
       DisableFooterPanel();
    }

    private void OnEditIncorrect()
    {
        DisableLevelInstruction();
        DisableFooterPanel();
    }
    
    

}
