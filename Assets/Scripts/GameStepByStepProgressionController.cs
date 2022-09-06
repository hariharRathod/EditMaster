using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStepByStepProgressionController : MonoBehaviour
{

    [SerializeField] private bool enableStepByStepProgression;
    [SerializeField] private int minToolsCount;
    
    private Dictionary<int, int> _gameToolsProgressionDict;

    public bool EnableStepByStepProgression => enableStepByStepProgression;


    private void Start()
    {
        IntializeGameToolsDictionary();
    }

    private void IntializeGameToolsDictionary()
    {
        _gameToolsProgressionDict = new Dictionary<int, int>();
        int count = GameFlowController.only.ToolsActivationOrder.Count;
        
        for (int i = 0; i < count; i++)
        {
            _gameToolsProgressionDict.Add(GameFlowController.only.ToolsActivationOrder[i],0);
        }
    }

    public void ToolTaskCompleted(int index)
    {

        if (!_gameToolsProgressionDict.ContainsKey(index)) return;

        if (_gameToolsProgressionDict[index] == 1) return;
        
        print("Tooltask completed");
        
        UpdateToolsDict(index);

        if (!EnableStepByStepProgression)
        {
            CheckIfToolsUsed();
            return;
        }
        
        ActivateNextToolIndex(index);

    }

    public void ActivateNextToolIndex(int index)
    {
        print("Inside activate next tool");
        
        int nextToolIndex = -1;
        
        for (int i = 0; i < GameFlowController.only.ToolsActivationOrder.Count; i++)
        {
            if (GameFlowController.only.ToolsActivationOrder[i] != index) continue;

            i = i + 1;
            if (i >= GameFlowController.only.ToolsActivationOrder.Count)
            {
                GameEvents.InvokeOnActivateDoneEditingButton();
                print("Invoke activate done");
                return;
            }
            
            nextToolIndex = GameFlowController.only.ToolsActivationOrder[i];

            break;

        }
        
        if(nextToolIndex < 0) return;
        
        print("Enable next index: " + nextToolIndex);
        
        GameFlowController.ToolsCanvasController.EnableToolButton(nextToolIndex);
        
    }

    public void UpdateToolsDict(int index)
    {
        _gameToolsProgressionDict[index] = 1;
        
    }

    private void CheckIfAllToolsUsed()
    {
        foreach (KeyValuePair<int,int> ele in _gameToolsProgressionDict)
        {
            if (ele.Value == 0) return;
        }
        
        GameEvents.InvokeOnActivateDoneEditingButton();
    }

    private void CheckIfToolsUsed()
    {
       
        int count = 0;
        foreach (KeyValuePair<int,int> ele in _gameToolsProgressionDict)
        {
            if (ele.Value == 1)
            {
                count++;
                if (count >= minToolsCount)
                {
                    GameEvents.InvokeOnActivateDoneEditingButton();
                    return;
                }
            }
        }

    }


}
