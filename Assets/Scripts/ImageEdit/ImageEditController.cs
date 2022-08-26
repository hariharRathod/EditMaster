using GestureRecognizer;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageEditController : MonoBehaviour
{

    private ImageEditRefBank _my;

    [SerializeField] private GameObject myDrawableArea;
    [SerializeField] private GesturePattern myGesturePattern;
    
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

    public enum ReplaceStatus
    {
        IsReplacable,
        NotReplacable
    }


    public SelectStatus selectStatus;
    public EraseStatus eraseStatus;
    public MoveStatus moveStatus;
    public CutStatus cutStatus;
    public ReplaceStatus replaceStatus;


    private bool isSelected;

    private SpriteRenderer imageSprite;
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
        GameEvents.CutToolSelected += OnCutToolSelected;
        GameEvents.SelectToolSelected += OnSelectToolSelected;
        GameEvents.CutDoneAccurately += OnCutDoneAccurately;
    }

    private void OnDisable()
    {
        GameEvents.ImageSelected -= OnImageSelected;
        GameEvents.EraserUsed -= OnEraserUsed;
        GameEvents.CutToolSelected -= OnCutToolSelected;
        GameEvents.SelectToolSelected -= OnSelectToolSelected;
        GameEvents.CutDoneAccurately -= OnCutDoneAccurately;
    }

    private void Start()
    {
        _transform = transform;
        _my = GetComponent<ImageEditRefBank>();
        if(myDrawableArea)
            myDrawableArea.SetActive(false);

        imageSprite = GetComponent<SpriteRenderer>();

    }
    
    private void OnImageSelected(Transform selectedImgTransform)
    {
        if (selectStatus == SelectStatus.NotSelectable) return;

        if (!_my.SelectHandler) return;

        if (selectedImgTransform == _transform)
        {
            _my.SelectHandler.OnImageSelected(true);
            IsSelected = true;
            
            GameFlowController.GameStepByStepProgressionController.ToolTaskCompleted(GameToolsIndex.SelectToolIndex);
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

        if (!IsSelected)
        {
            GameEvents.InvokeOnImageNotSelectedMessage();
            return;
        }

        if (!_my.EraseHandler) return;
        
        
        _my.EraseHandler.OnEraserUsed();
        
        GameFlowController.GameStepByStepProgressionController.ToolTaskCompleted(GameToolsIndex.EraserToolIndex);
    }
    
    private void OnCutToolSelected()
    {
        if (!IsSelected)
        {
            if(myDrawableArea)
                myDrawableArea.SetActive(false);
            
           
            return;
        }
        
        if(!myDrawableArea) return;
        
        if(!myGesturePattern) return;
        
        myDrawableArea.SetActive(true);
        
        
        //this is enabling draw area and setting gesture pattern.
        GameEvents.InvokeOnMyDrawableAreaIsOn(myDrawableArea.transform,myGesturePattern);
        
        
    }
    
    private void OnSelectToolSelected()
    {
        if(myDrawableArea)
            myDrawableArea.SetActive(false);
    }
    
    private void OnCutDoneAccurately()
    {
        if (!IsSelected) return;
        
        _my.SelectHandler.OnImageSelected(false);
        IsSelected = false;
        
    }

    public void ReplaceImage(Sprite sprite)
    {
        print("In replace image");
        
        if(replaceStatus == ReplaceStatus.NotReplacable) return;

        if (!IsSelected) return;
        
        print("change sprite");
        imageSprite.sprite = sprite;
    }
}
