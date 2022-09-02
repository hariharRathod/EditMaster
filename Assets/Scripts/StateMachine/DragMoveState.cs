using UnityEngine;

public class DragMoveState : InputStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void Execute()
    {
        if (InputExtensions.GetFingerUp())
        {
            print("finger up in dragmove");
            GameFlowController.GameStepByStepProgressionController.ToolTaskCompleted(GameToolsIndex.MoveToolIndex);
        }
        
        
        var ray = InputHandler.mainCamera.ScreenPointToRay(InputExtensions.GetInputPosition());

        //object to move
        var hit = Physics2D.Raycast(ray.origin, ray.direction, 50f);

        //collider
        var layerMask = LayerMask.GetMask(new[] { "DragMovable" });
        var hitAll = Physics2D.RaycastAll(ray.origin, ray.direction, 50f, layerMask);
        
        if(!hit.collider) return;
        
        if(hitAll.Length == 0) return;

        if (!hitAll[0].transform.CompareTag("DragMovableArea"));

        if (!hit.transform.CompareTag("EditableImage")) {return;}

        if (!hit.transform.TryGetComponent(out ImageEditController editController)) return;

        if (!editController.IsSelected) return;
        
        if(editController.freeMovableStatus != ImageEditController.FreeMovableStatus.IsMovable) return;

        hit.transform.position = new Vector3(hit.point.x,hit.point.y,hit.transform.position.z);

        

    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
