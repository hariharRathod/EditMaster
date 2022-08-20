using System.Collections.Generic;
using UnityEngine;

public class TapState : InputStateBase
{
    public override void OnEnter()
    {
       print("In tap state");
    }

    public override void Execute()
    {
        //abstraction
        switch (ToolsManager.CurrentToolState)
        {
            case ToolsState.Select:
            {
                print("In tap state Image Select case "); 

                var ray = InputHandler.mainCamera.ScreenPointToRay(InputExtensions.GetInputPosition());

                var hit = Physics2D.Raycast(ray.origin, ray.direction, 50f);
                
                print("raycast done in tap state");
                if (!hit.collider) return;
                
                print("Image Selected");

                GameEvents.InvokeOnImageSelected(hit.transform);
            }
                break;
            case ToolsState.Erase:
            {
                
            }
                break;
        }
        
        InputHandler.AssignNewState(InputState.Idle);
    }

    public override void OnExit()
    {
        
    }
}
