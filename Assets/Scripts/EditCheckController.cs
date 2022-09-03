using System;
using System.Collections.Generic;
using UnityEngine;

public class EditCheckController : MonoBehaviour
{
    [SerializeField] private List<String> levelActionsName;
    [SerializeField] private bool makeSureToUserWinsEnabled;
    [SerializeField] private List<ImageEditRefBank> listOfImagesToRemove;
    [SerializeField] private GameObject moveAndScaleRefGameObject,moveAndScaleGameObject;
    [SerializeField] private float moveThershold,scaleThershold;

    private const string Erase = "Erase";
    private const string MoveAndScale = "MoveAndScale";
    
    private List<ImageEditRefBank> _listOfImagesErased;
    
    private void Start()
    {
        _listOfImagesErased = new List<ImageEditRefBank>();
    }
    
    
    public void AddErasedImages(ImageEditRefBank refBank)
    {
        _listOfImagesErased.Add(refBank);
    }

    public void EditCheck()
    {
        if (makeSureToUserWinsEnabled)
        {
            //game win
            print("game win");
            return;
        }


        for (int i = 0; i < levelActionsName.Count; i++)
        {
            bool check = CheckForSpecificAction(levelActionsName[i]);
            if (!check)
            {
                //game lose
                print("game lost");
                return;
            }
        }
        
        //game win
        print("game win");
    }

    private bool CheckForSpecificAction(string actionName)
    {
        switch (actionName)
        {
            case Erase:
                return CheckEraseImages();
            case MoveAndScale:
                return CheckMoveAndScaleImages();
               
                
        }

        return false;
    }

    private bool CheckEraseImages()
    {
        if (_listOfImagesErased.Count != listOfImagesToRemove.Count) return false;

        for (int i = 0; i < _listOfImagesErased.Count; i++)
        {
            if (!listOfImagesToRemove.Contains(_listOfImagesErased[i])) return false;
        }

        return true;
    }
    
    private bool CheckMoveAndScaleImages()
    {
        float mag = (moveAndScaleGameObject.transform.position - moveAndScaleRefGameObject.transform.position)
            .magnitude;
        
        print("mag in edit check controller: " + mag);

        if (mag > moveThershold) return false;

        float value = moveAndScaleGameObject.transform.localScale.x - moveAndScaleRefGameObject.transform.localScale.x;
        value = Mathf.Abs(value);
        
        print("value in edit check controller: " + value);

        if (value > scaleThershold) return false;
        
        
        return true;
    }



}
