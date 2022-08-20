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


    private Transform _transform;

    private void OnEnable()
    {
        GameEvents.ImageSelected += OnImageSelected;
    }

    private void OnDisable()
    {
        GameEvents.ImageSelected -= OnImageSelected;
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
        
        if(selectedImgTransform == _transform)
            _my.SelectHandler.OnImageSelected(true);
        else
            _my.SelectHandler.OnImageSelected(false);


    }
}
