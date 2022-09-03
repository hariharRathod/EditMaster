using UnityEngine;

public class ScaleState : InputStateBase
{
    private float referenceValue = -1;
    private float referenceScaledValue = -1;

    private ImageEditController _editController;

    private void CalculateReferenceValue()
    {
       
        
        var ray = InputHandler.mainCamera.ScreenPointToRay(InputExtensions.GetInputPosition());
        var hit = Physics2D.Raycast(ray.origin, ray.direction, 50f);
        
        print("calculate ref value hit tak");
        if (!hit.collider) return;
        
        
        print("calculate ref value collider tak");
        if (!hit.transform.CompareTag("ScaleableDots")) return;
        
        print("calculate ref value scaleabledots mila");
        
        if(!hit.transform.parent.parent.TryGetComponent(out ImageEditController editController)) return;
        
        print("calculate ref value editcontroller mila");

        _editController = editController;
        
        var vecDir = editController.transform.position - hit.transform.position;
        var mag = vecDir.magnitude;

        referenceValue = mag;
        
        print("calculate ref value");

    }
    
    private void CalculateReferenceScaledValue(Vector3 hitpos)
    {
        var ray = InputHandler.mainCamera.ScreenPointToRay(InputExtensions.GetInputPosition());
        var hit = Physics2D.Raycast(ray.origin, ray.direction, 50f);

        if (!hit.collider) return;

        if (!hit.transform.CompareTag("ScaleableDots")) return;
        
        if(!hit.transform.parent.parent.TryGetComponent(out ImageEditController editController)) return;

        var vecDir = editController.transform.position - hitpos;
        var mag = vecDir.magnitude;

        referenceScaledValue = mag;
        

    }

    private void AssignNewReferenceValue()
    {
        referenceValue = referenceScaledValue;
    }

    public override void OnEnter()
    {
        
        CalculateReferenceValue();
    }


    public override void Execute()
    {

        if (InputExtensions.GetFingerUp())
        {
            InputHandler.AssignNewState(InputState.Idle);
            GameFlowController.GameStepByStepProgressionController.ToolTaskCompleted(GameToolsIndex.ScaleToolIndex);
        }
        
        var ray = InputHandler.mainCamera.ScreenPointToRay(InputExtensions.GetInputPosition());
        var hit = Physics2D.Raycast(ray.origin, ray.direction, 50f);

        if (!hit.collider) return;

        if (!hit.transform.CompareTag("ScaleableDots")) return;

        Vector3 hitPos = new Vector3(hit.point.x,hit.point.y,hit.transform.position.z);
        
        
        if(!_editController) return;

        if (InputExtensions.GetInputDelta().magnitude < 0.1f) return;
        
        CalculateReferenceScaledValue(hitPos);

        var x = referenceScaledValue / referenceValue;
        Vector3 scale = _editController.transform.localScale;
        scale *= x;
        _editController.transform.localScale = scale;
        
        AssignNewReferenceValue();
        
        if(_editController.transform.localScale.x < 1)
            _editController.transform.localScale = Vector3.one;

    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
