using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class PopUpOptionsPanelController : MonoBehaviour
{
    [SerializeField] private GameObject backgroundOptionsPanel;
    [SerializeField] private List<GameObject> optionsPanelsList;
    [SerializeField] private float optionsPanelInDuration, optionsPanelOutDuration;
    [SerializeField] private Ease inEase,outEase;

    private void OnEnable()
    {
        GameEvents.BackgroundChangeToolSelected += OnBackGroundChangeSelected;
        GameEvents.SelectToolSelected += OnSelectToolSelected;
        GameEvents.EraserToolSelected += OnEraseToolSelected;
        GameEvents.CutToolSelected += OnCutToolSelected;
    }

    private void OnDisable()
    {
        GameEvents.BackgroundChangeToolSelected -= OnBackGroundChangeSelected;
        GameEvents.SelectToolSelected -= OnSelectToolSelected;
        GameEvents.EraserToolSelected -= OnEraseToolSelected;
        GameEvents.CutToolSelected -= OnCutToolSelected;
    }

    
    private void Start()
    {
        DisableOptionsPanels();
    }

    private void DisableOptionsPanels()
    {
        for (int i = 0; i < optionsPanelsList.Count; i++)
        {
            optionsPanelsList[i].SetActive(false);
        }
        
    }


    private void OnBackGroundChangeSelected()
    {
        
        OptionsPanelInAnimation(backgroundOptionsPanel);
        
    }



    private void OptionsPanelInAnimation(GameObject optionsPanel)
    {
        if (optionsPanel.activeInHierarchy) return;
        
        optionsPanel.transform.localScale = Vector3.zero;
        
        optionsPanel.SetActive(true);
        optionsPanel.transform.DOScale(Vector3.one, optionsPanelInDuration).SetEase(inEase);
    }
    
    private void OptionsPanelOutAnimation(GameObject optionsPanel)
    {
        optionsPanel.transform.DOScale(Vector3.zero, optionsPanelOutDuration).SetEase(outEase).OnComplete(() =>
        {
            optionsPanel.SetActive(false);
        });
    }
    
    
    private void OnSelectToolSelected()
    {
        OptionsPanelOutAnimation(backgroundOptionsPanel);
    }
    
    private void OnCutToolSelected()
    {
        OptionsPanelOutAnimation(backgroundOptionsPanel);
    }

    private void OnEraseToolSelected()
    {
        
        OptionsPanelOutAnimation(backgroundOptionsPanel);
        
    }
    
    


}
