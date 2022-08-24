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

                var ray = InputHandler.mainCamera.ScreenPointToRay(InputExtensions.GetInputPosition());

                var hit = Physics2D.Raycast(ray.origin, ray.direction, 50f);

                if(!hit.collider) return;
                
                if (!hit.transform.CompareTag("EditableImage")) { InputHandler.AssignNewState(InputState.Idle);return;}
                
                print("Image Selected");

                GameEvents.InvokeOnImageSelected(hit.transform);
                
                
            }
                break;
            case ToolsState.Erase:
            {
                var ray = InputHandler.mainCamera.ScreenPointToRay(InputExtensions.GetInputPosition());

                var hit = Physics2D.Raycast(ray.origin, ray.direction, 50f);
                
                if(!hit.collider) return;
                
                if (!hit.transform.CompareTag("EditableImage")) { InputHandler.AssignNewState(InputState.Idle);return;}
                
                GameEvents.InvokeOnEraserUsed(hit.transform);
                
                
            }
                break;
        }
        
        InputHandler.AssignNewState(InputState.Idle);
        
        
    }

    public override void OnExit()
    {
        
    }
}
