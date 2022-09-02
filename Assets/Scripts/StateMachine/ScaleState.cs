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
    
    private void CalculateReferenceScaledValue()
    {
        var ray = InputHandler.mainCamera.ScreenPointToRay(InputExtensions.GetInputPosition());
        var hit = Physics2D.Raycast(ray.origin, ray.direction, 50f);

        if (!hit.collider) return;

        if (!hit.transform.CompareTag("ScaleableDots")) return;
        
        if(!hit.transform.parent.parent.TryGetComponent(out ImageEditController editController)) return;

        var vecDir = editController.transform.position - hit.transform.position;
        var mag = vecDir.magnitude;

        referenceScaledValue = mag;
        
        AssignNewReferenceValue();

    }

    private void AssignNewReferenceValue()
    {
        referenceValue = referenceScaledValue;
    }

    public override void OnEnter()
    {
        print("scale state on enter");
        CalculateReferenceValue();
    }


    public override void Execute()
    {
        if (InputExtensions.GetFingerUp())
        {
            InputHandler.AssignNewState(InputState.Idle);
            
        }


        if(!_editController) return;

        if (InputExtensions.GetInputDelta().magnitude < 0.8f) return ;

        var x = referenceScaledValue / referenceValue;
        Vector3 scale = _editController.transform.localScale;
        scale *= x;
        _editController.transform.localScale = scale;
        
        if(_editController.transform.localScale.x < 1)
            _editController.transform.localScale = Vector3.one;
        
        CalculateReferenceScaledValue();
        
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
