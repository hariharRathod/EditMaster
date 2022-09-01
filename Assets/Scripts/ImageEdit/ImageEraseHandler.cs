using System;
using UnityEngine;

public class ImageEraseHandler : MonoBehaviour
{
    private ImageEditRefBank _my;

    private MagicEraserLevelController _eraserLevelController;

    private void Start()
    {
        _my = GetComponent<ImageEditRefBank>();
        
        if(!transform.parent.TryGetComponent(out MagicEraserLevelController eraserLevelController)) return;

        _eraserLevelController = eraserLevelController;
    }

    


    public void OnEraserUsed()
   {
        this.gameObject.SetActive(false);
        
        if(!_eraserLevelController) return;
        
        _eraserLevelController.AddErasedImages(_my);
        
        
   }
  
}
