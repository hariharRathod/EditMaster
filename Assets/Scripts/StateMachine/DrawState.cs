using GestureRecognizer;
using UnityEngine;

public class DrawState : InputStateBase
{
    private static DrawMechanic _drawMechanic;

    private DrawState() { }

    public DrawState(DrawMechanic Mechanic) => _drawMechanic = Mechanic;

    public override void OnEnter()
    {
        base.OnEnter();
        _drawMechanic.StartDrawing();
    }

    public override void Execute()
    {
        base.Execute();
        if (ToolsManager.CurrentToolState != ToolsState.Cut) return;
        
        var ray = InputHandler.mainCamera.ScreenPointToRay(InputExtensions.GetInputPosition());

        Debug.DrawRay(ray.origin,Vector3.forward,Color.red,2f);
        
        if (!Physics.Raycast(ray, out var hit, 50f))
        {
            
            return;
        }

        if (!hit.transform.CompareTag("Drawable")) { return;}

        if (!hit.transform.parent.TryGetComponent(out ImageEditController editController)) return;
        
        if (!editController.IsSelected){ return;}
        
        _drawMechanic.Draw(hit);
        
    }

    public override void OnExit()
    {
        base.OnExit();
        _drawMechanic.StopDrawing();
    }
}
