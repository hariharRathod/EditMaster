using DG.Tweening;
using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    public static GameFlowController only;
    
    private void Awake()
    {
        if (!only) only = this;
        else Destroy(gameObject);

        DOTween.KillAll();
    }
    
    void Start()
    {
        ToolsManager.CurrentToolState = ToolsState.none;
    }

    
}
