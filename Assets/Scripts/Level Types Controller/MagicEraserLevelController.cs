using System;
using System.Collections.Generic;
using UnityEngine;

public class MagicEraserLevelController : LevelsControllerBase
{

    [SerializeField] private List<ImageEditRefBank> listOfSpritesToErase;
    

    private List<ImageEditRefBank> _listOfImagesErased;

    private void Start()
    {
        _listOfImagesErased = new List<ImageEditRefBank>();
    }

    public void AddErasedImages(ImageEditRefBank refBank)
    {
        _listOfImagesErased.Add(refBank);
    }

    public void CheckMagicEraser()
    {
        if (_listOfImagesErased.Count != listOfSpritesToErase.Count)
        {
            //game over
            return;
        }

        for (int i = 0; i < _listOfImagesErased.Count; i++)
        {
            if (!listOfSpritesToErase.Contains(_listOfImagesErased[0]))
            {
                //game over
                return;
            }
        }
        
        //game win
        
        
        
    }
}
