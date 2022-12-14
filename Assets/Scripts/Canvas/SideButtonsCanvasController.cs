using System;
using System.Collections.Generic;
using DG.Tweening;
using GestureRecognizer;
using UnityEngine;
using UnityEngine.UI;

public class SideButtonsCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject cutDoneButton, cutClearButton, imageNotSelectedMessageGameObject,
        cutDoneMessageGameObject,cutNotDoneMessageGameObject,selectImageInstructionGameObject,
        cutImageInstructionGameObject,eraserSideInstructionGameObject,moveSideToolInstructionGameObject,scaleSideToolInstructionGameObject;
        
        
    [SerializeField] private bool showSideMessages;

    [Space(15),SerializeField] private List<GameObject> sideButtonsList;

    [Space(15), SerializeField] private float buttonsInDuration, buttonsOutDuration,rightButtonsOutOffset,leftButtonsOutOffset;

    [Header("DoneEditingButtons")] 
    [Space(30)] 
    [SerializeField] private GameObject eraserDoneEditingButton;
    
    [Header("ExportButton")] 
    [Space(30)] 
    [SerializeField] private GameObject exportButton;
    
    
    private Dictionary<string, Vector3> buttonsDict;

    private DrawMechanic drawMechanic;

    public GameObject EraserDoneEditingButton => eraserDoneEditingButton;


    private void OnEnable()
    {
        GameEvents.CutToolSelected += OnCutToolSelected;
        GameEvents.SelectToolSelected += OnSelectToolSelected;
        GameEvents.EraserToolSelected += OnEraseToolSelected;
        GameEvents.CameraZoomActionCompleted += OnCameraZoomCompleted;
        GameEvents.CutDoneAccurately += OnCutDoneAccurately;
        GameEvents.CutNotAccurate += OnCutNotDoneAccurately;
        GameEvents.ImageNotSelectedMessage += OnImageNotSelectedMessage;
        GameEvents.ImageSelected += OnAnyImageSelected;
        GameEvents.BackgroundChangeToolSelected += OnBackGroundChangeToolSelected;
        GameEvents.ScaleToolSelected += OnScaleToolSelected;
        GameEvents.MoveToolSelected += OnMoveToolSelected;
        GameEvents.EditCorrect += OnEditCorrect;
        GameEvents.EditIncorrect += OnEditIncorrect;

    }

    private void OnDisable()
    {
        GameEvents.CutToolSelected -= OnCutToolSelected;
        GameEvents.SelectToolSelected -= OnSelectToolSelected;
        GameEvents.EraserToolSelected -= OnEraseToolSelected;
        GameEvents.CameraZoomActionCompleted -= OnCameraZoomCompleted;
        GameEvents.CutDoneAccurately -= OnCutDoneAccurately;
        GameEvents.CutNotAccurate -= OnCutNotDoneAccurately;
        GameEvents.ImageNotSelectedMessage -= OnImageNotSelectedMessage;
        GameEvents.ImageSelected -= OnAnyImageSelected;
        GameEvents.BackgroundChangeToolSelected -= OnBackGroundChangeToolSelected;
        GameEvents.ScaleToolSelected -= OnScaleToolSelected;
        GameEvents.MoveToolSelected -= OnMoveToolSelected;
        GameEvents.EditCorrect -= OnEditCorrect;
        GameEvents.EditIncorrect -= OnEditIncorrect;

    }

    
    private void Start()
    {
        
        buttonsDict = new Dictionary<string, Vector3>();
        DisableAllSideButtons();
        StoreAllButtonInitailPos();
        SendAllSideButtonsOut();
        
        if(!GameObject.Find("DrawMechanic")) return;
        
        drawMechanic = GameObject.Find("DrawMechanic").GetComponent<DrawMechanic>();
        
    }

    public void OnCutDoneButtonPressed()
    {
        if (!drawMechanic) return;
        
        drawMechanic.RecogniseUserInput();
    }

    public void OnCutClearButtonPressed()
    {
        if (!drawMechanic) return;
        
        drawMechanic.ClearAllDrawing();
        InputHandler.AssignNewState(InputState.Idle);
    }


    private void DisableAllSideButtons()
    {
        for (int i = 0; i < sideButtonsList.Count; i++)
        {
            sideButtonsList[i].SetActive(false);
        }
    }
    
    private void EnableAllSideButtons()
    {
        for (int i = 0; i < sideButtonsList.Count; i++)
        {
            sideButtonsList[i].SetActive(true);
        }
    }

    private void StoreAllButtonInitailPos()
    {
        for (int i = 0; i < sideButtonsList.Count; i++)
        {
            GameObject button = sideButtonsList[i];
            RectTransform buttonrect = button.GetComponent<RectTransform>();
            string key = button.gameObject.name;
            if (!buttonsDict.ContainsKey(key))
            {
                buttonsDict.Add(key,buttonrect.anchoredPosition);
            }
        }

    }


    private void SendAllSideButtonsOut()
    {
        for (int i = 0; i < sideButtonsList.Count; i++)
        {
            SideButtonOutAnimation(sideButtonsList[i]);
        }
    }


    public void SideButtonInAnimation(GameObject button)
    {
      
        RectTransform buttonrect = button.GetComponent<RectTransform>();
        foreach (KeyValuePair<string,Vector3> ele in buttonsDict)
        {
            if (String.Compare(ele.Key, button.gameObject.name) == 0)
            {
                Vector3 pos = ele.Value;
                buttonrect.DOAnchorPos(pos, buttonsInDuration).SetEase(Ease.OutElastic);
                break;
            }
        }
        
    }

    public void SideButtonOutAnimation(GameObject button)
    {
        bool isRightButton;
        RectTransform buttonrect = button.GetComponent<RectTransform>();
        Vector3 buttonpos = buttonrect.anchoredPosition;
        if (!button.TryGetComponent(out CanvasGameObjectSideDecider canvasGameObjectSideDecider)) return;
        
        if (canvasGameObjectSideDecider.IsRightSideButton)
            isRightButton = true;
        else
            isRightButton = false;
        
        if (isRightButton)
        {
            print( button.name + "is right button");
            foreach (KeyValuePair<string,Vector3> ele in buttonsDict)
            {
                if (String.Compare(ele.Key, button.gameObject.name) == 0)
                {
                    Vector3 pos = ele.Value;
                    float xOffsetVal = pos.x + rightButtonsOutOffset;
                    buttonrect.DOAnchorPosX(xOffsetVal, buttonsOutDuration).SetEase(Ease.Linear);
                    break;
                }
            }
        }
        else
        {
            print(button.name + "is left button");
            foreach (KeyValuePair<string,Vector3> ele in buttonsDict)
            {
                if (String.Compare(ele.Key, button.gameObject.name) == 0)
                {
                    Vector3 pos = ele.Value;
                    float xOffsetVal = pos.x - leftButtonsOutOffset;
                    buttonrect.DOAnchorPosX(xOffsetVal, buttonsOutDuration).SetEase(Ease.Linear);
                    break;
                }
            }
        }
    }
    
   

    private void OnSelectToolSelected()
    {
        SideButtonOutAnimation(eraserSideInstructionGameObject);
        SideButtonOutAnimation(cutDoneButton);
        SideButtonOutAnimation(cutClearButton);
        SideButtonOutAnimation(cutImageInstructionGameObject);
        SideButtonOutAnimation(moveSideToolInstructionGameObject);
        SideButtonOutAnimation(scaleSideToolInstructionGameObject);
        
        SideButtonInAnimation(selectImageInstructionGameObject);
        
    }
    
    private void OnEraseToolSelected()
    {
        SideButtonOutAnimation(cutDoneButton);
        SideButtonOutAnimation(cutClearButton);
        SideButtonOutAnimation(cutImageInstructionGameObject);
        SideButtonOutAnimation(selectImageInstructionGameObject);
        SideButtonOutAnimation(moveSideToolInstructionGameObject);
        SideButtonOutAnimation(scaleSideToolInstructionGameObject);
       
        SideButtonInAnimation(eraserSideInstructionGameObject);
    }
    
    private void OnCutToolSelected()
    {
        SideButtonOutAnimation(eraserSideInstructionGameObject);
        SideButtonOutAnimation(selectImageInstructionGameObject);
        SideButtonOutAnimation(moveSideToolInstructionGameObject);
        SideButtonOutAnimation(scaleSideToolInstructionGameObject);
        SideButtonInAnimation(cutImageInstructionGameObject);
        SideButtonInAnimation(cutDoneButton);
        SideButtonInAnimation(cutClearButton);
    }
    
    private void OnBackGroundChangeToolSelected()
    {
        SideButtonOutAnimation(selectImageInstructionGameObject);
        SideButtonOutAnimation(eraserSideInstructionGameObject);
        SideButtonOutAnimation(moveSideToolInstructionGameObject);
        SideButtonOutAnimation(scaleSideToolInstructionGameObject);
        SideButtonOutAnimation(cutDoneButton);
        SideButtonOutAnimation(cutClearButton);
        SideButtonOutAnimation(cutImageInstructionGameObject);
    }
    
    private void OnScaleToolSelected()
    {
        SideButtonOutAnimation(selectImageInstructionGameObject);
        SideButtonOutAnimation(eraserSideInstructionGameObject);
        SideButtonOutAnimation(cutImageInstructionGameObject);
        SideButtonOutAnimation(moveSideToolInstructionGameObject);
       
        SideButtonInAnimation(scaleSideToolInstructionGameObject);
    }
    
    private void OnMoveToolSelected()
    {
        SideButtonOutAnimation(selectImageInstructionGameObject);
        SideButtonOutAnimation(eraserSideInstructionGameObject);
        SideButtonOutAnimation(cutImageInstructionGameObject);
        SideButtonOutAnimation(scaleSideToolInstructionGameObject);
        
        SideButtonInAnimation(moveSideToolInstructionGameObject);
    }
    
    private void OnCameraZoomCompleted()
    {
       DOVirtual.DelayedCall(0.5f,()=> EnableAllSideButtons()).OnComplete(()=>
       {
            SideButtonInAnimation(selectImageInstructionGameObject);    
       });
    }
    
    private void OnCutDoneAccurately()
    {
        SideButtonOutAnimation(cutDoneButton);
        SideButtonOutAnimation(cutClearButton);
        SideButtonOutAnimation(cutImageInstructionGameObject);
        
        SideButtonInAnimation(cutDoneMessageGameObject);

        DOVirtual.DelayedCall(1, () => SideButtonOutAnimation(cutDoneMessageGameObject));
    }
    
    private void OnCutNotDoneAccurately()
    {
        
        SideButtonInAnimation(cutNotDoneMessageGameObject);

        DOVirtual.DelayedCall(1.3f, () => SideButtonOutAnimation(cutNotDoneMessageGameObject));
    }
    
    
    private void OnImageNotSelectedMessage()
    {
       SideButtonInAnimation(imageNotSelectedMessageGameObject);
       
       DOVirtual.DelayedCall(1f, () => SideButtonOutAnimation(imageNotSelectedMessageGameObject));
    }
    
    private void OnAnyImageSelected(Transform obj)
    {
        //SideButtonOutAnimation(selectImageInstructionGameObject);
    }


    public void ActivateEraserTypeDoneEditingButton()
    {
        SideButtonInAnimation(eraserDoneEditingButton);
    }

    public void ActivateExportButton()
    {
        SideButtonInAnimation(exportButton);
    }


    public void SendOutExportButton()
    {
        SideButtonOutAnimation(exportButton);
    }

    private void OnEditIncorrect()
    {
       SendAllSideButtonsOut();
    }

    private void OnEditCorrect()
    {
       SendAllSideButtonsOut();
    }
    
    
}
