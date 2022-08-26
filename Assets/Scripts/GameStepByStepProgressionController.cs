using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStepByStepProgressionController : MonoBehaviour
{

    private Dictionary<int, int> _gameToolsProgressionDict;


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
        
        foreach (KeyValuePair<int, int> ele in _gameToolsProgressionDict)
        {
            if(ele.Key!=index) continue;
            
            //check if tool already active i.e 1.
            if (ele.Value == 1) return;

            _gameToolsProgressionDict[ele.Key] = 1;
            
            //Activate next button
           if(!GameFlowController.ToolsCanvasController) return;
           
           GameFlowController.ToolsCanvasController.EnableToolButton(index);
            
        }
    }


}
