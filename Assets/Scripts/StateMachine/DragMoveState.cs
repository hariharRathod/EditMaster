using UnityEngine;

public class DragMoveState : InputStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void Execute()
    {
        var ray = InputHandler.mainCamera.ScreenPointToRay(InputExtensions.GetInputPosition());

        var hit = Physics2D.Raycast(ray.origin, ray.direction, 50f);
                
        if(!hit.collider) return;
        
        if (!hit.transform.CompareTag("EditableImage")) {return;}

        if (!hit.transform.TryGetComponent(out ImageEditController editController)) return;
        
        if(editController.freeMovableStatus != ImageEditController.FreeMovableStatus.IsMovable) return;

        hit.transform.position = new Vector3(hit.point.x,hit.point.y,hit.transform.position.z);
        
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
