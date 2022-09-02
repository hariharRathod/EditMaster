using UnityEngine;

public class ImageScaleObjectHandler : MonoBehaviour
{
    [SerializeField] private GameObject scaleFrameGameObject;


    private void OnEnable()
    {
        GameEvents.SelectToolSelected += DisableScaleFrame;
        GameEvents.EraserToolSelected += DisableScaleFrame;
        GameEvents.CutToolSelected += DisableScaleFrame;
        GameEvents.MoveToolSelected += DisableScaleFrame;
        GameEvents.BackgroundChangeToolSelected += DisableScaleFrame;
    }

    private void OnDisable()
    {
        GameEvents.SelectToolSelected -= DisableScaleFrame;
        GameEvents.EraserToolSelected -= DisableScaleFrame;
        GameEvents.CutToolSelected -= DisableScaleFrame;
        GameEvents.MoveToolSelected -= DisableScaleFrame;
        GameEvents.BackgroundChangeToolSelected -= DisableScaleFrame;
    }


    public void EnableScaleFrame()
    {
        scaleFrameGameObject.SetActive(true);
    }

    public void DisableScaleFrame()
    {
        scaleFrameGameObject.SetActive(false);
    }


}
