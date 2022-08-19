using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapState : InputStateBase
{
    public override void OnEnter()
    {
       
    }

    public override void Execute()
    {
        switch (InputHandler.CurrentToolState)
        {
            case ToolsState.Select:
            {
                var ray = InputHandler.mainCamera.ScreenPointToRay(InputExtensions.GetInputPosition());

                if (!Physics.Raycast(ray, out var hit, 50f)) return;
                
                

            }
                break;
            case ToolsState.Erase:
            {
                
            }
                break;
        }
    }

    public override void OnExit()
    {
        
    }
}
