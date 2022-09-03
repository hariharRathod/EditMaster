using System;
using UnityEngine;

public class ImageEraseHandler : MonoBehaviour
{
    private ImageEditRefBank _my;

    private EditCheckController _editCheckController;

    private void Start()
    {
        _my = GetComponent<ImageEditRefBank>();
        
        if(!transform.parent.TryGetComponent(out EditCheckController editCheckController)) return;

        _editCheckController = editCheckController;
    }

    


    public void OnEraserUsed()
   {
        this.gameObject.SetActive(false);
        
        if(!_editCheckController) return;
        
        _editCheckController.AddErasedImages(_my);
        
        
   }
  
}
