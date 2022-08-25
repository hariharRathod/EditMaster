using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpOptionsPanelController : MonoBehaviour
{
    [SerializeField] private GameObject backgroundOptionsPanel;
    [SerializeField] private List<GameObject> optionsPanelsList;

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
        
        
        
    }


}
