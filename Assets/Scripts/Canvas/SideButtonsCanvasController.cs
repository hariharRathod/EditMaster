using System;
using System.Collections.Generic;
using DG.Tweening;
using GestureRecognizer;
using UnityEngine;
using UnityEngine.UI;

public class SideButtonsCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject cutDoneButton, cutClearButton, imageNotSelectedButton;


    [Space(5),SerializeField] private List<GameObject> sideButtonsList;

    [Space(5), SerializeField] private float buttonsInDuration, buttonsOutDuration,rightButtonsOutOffset,leftButtonsOutOffset;

    private Dictionary<string, Vector3> buttonsDict;

    private DrawMechanic drawMechanic;


    private void OnEnable()
    {
        GameEvents.CutToolSelected += OnCutToolSelected;
        GameEvents.SelectToolSelected += OnSelectToolSelected;
        GameEvents.EraserToolSelected += OnEraseToolSelected;
        GameEvents.CameraZoomActionCompleted += OnCameraZoomCompleted;

    }

    private void OnDisable()
    {
        GameEvents.CutToolSelected -= OnCutToolSelected;
        GameEvents.SelectToolSelected -= OnSelectToolSelected;
        GameEvents.EraserToolSelected -= OnEraseToolSelected;
        GameEvents.CameraZoomActionCompleted -= OnCameraZoomCompleted;
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


    private void OnCutToolSelected()
    {
        SideButtonInAnimation(cutDoneButton);
        SideButtonInAnimation(cutClearButton);
    }
    
    private void SideButtonInAnimation(GameObject button)
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

    private void SideButtonOutAnimation(GameObject button)
    {
        bool isRightButton;
        RectTransform buttonrect = button.GetComponent<RectTransform>();
        Vector3 buttonpos = buttonrect.anchoredPosition;
        if (Mathf.Abs(buttonpos.x) > (Screen.width * 0.5f))
            isRightButton = false;
        else
            isRightButton = true;
        
        
        if (isRightButton)
        {
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
    
    private void OnEraseToolSelected()
    {
       SideButtonOutAnimation(cutDoneButton);
       SideButtonOutAnimation(cutClearButton);
    }

    private void OnSelectToolSelected()
    {
        SideButtonOutAnimation(cutDoneButton);
        SideButtonOutAnimation(cutClearButton);
    }
    
    private void OnCameraZoomCompleted()
    {
       DOVirtual.DelayedCall(0.5f,()=> EnableAllSideButtons());
    }
}
