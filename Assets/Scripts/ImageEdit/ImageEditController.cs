using UnityEngine;

public class ImageEditController : MonoBehaviour
{

    private ImageEditRefBank _my;
    
    public enum SelectStatus
    {
        IsSelectable,
        NotSelectable
    }

    public enum EraseStatus
    {
        IsEraseable,
        NotEraseable
    }

    public enum MoveStatus
    {
        IsMoveable,
        NotMoveable
    }

    public enum CutStatus
    {
        IsCutable,
        NotCutable
    }


    public SelectStatus selectStatus;
    public EraseStatus eraseStatus;
    public MoveStatus moveStatus;
    public CutStatus cutStatus;


    private bool isSelected;


    private Transform _transform;

    public bool IsSelected
    {
        get => isSelected;
        set => isSelected = value;
    }


    private void OnEnable()
    {
        GameEvents.ImageSelected += OnImageSelected;
        GameEvents.EraserUsed += OnEraserUsed;
    }

    private void OnDisable()
    {
        GameEvents.ImageSelected -= OnImageSelected;
        GameEvents.EraserUsed -= OnEraserUsed;
    }

    
    private void Start()
    {
        _transform = transform;
        _my = GetComponent<ImageEditRefBank>();
    }
    
    private void OnImageSelected(Transform selectedImgTransform)
    {
        if (selectStatus == SelectStatus.NotSelectable) return;

        if (!_my.SelectHandler) return;

        if (selectedImgTransform == _transform)
        {
            _my.SelectHandler.OnImageSelected(true);
            IsSelected = true;
        }
        else
        {
            _my.SelectHandler.OnImageSelected(false);
            IsSelected = false;
        }
        
    }
    
    private void OnEraserUsed(Transform imgTransformToErase)
    {
        if (eraseStatus == EraseStatus.NotEraseable) return;

        if (imgTransformToErase != _transform) return;
        
        if (!IsSelected) return;

        if (!_my.EraseHandler) return;
        
        
        _my.EraseHandler.OnEraserUsed();
    }
}
