
using System;
using DG.Tweening;
using UnityEngine;

public class EditorWindowCanvasController : MonoBehaviour
{
    [SerializeField] private RectTransform editorWindowPanelRect;
    [SerializeField] private float editorPanelInDuration;
    [SerializeField] private Ease editorPanelInEase;

    private void OnEnable()
    {
        GameEvents.CameraZoomActionCompleted += OnCameraZoomActionCompleted;
    }

    private void OnDisable()
    {
        GameEvents.CameraZoomActionCompleted -= OnCameraZoomActionCompleted;
    }

    


    private void Start()
    {
        SetInitalEditorPanelPos();

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
        editorWindowPanelRect.DOAnchorPos(Vector2.zero, editorPanelInDuration).SetEase(editorPanelInEase);
    }
    
}
