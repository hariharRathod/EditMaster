using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DragAndDropUIElements : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler,IPointerUpHandler
{

    private RectTransform _rectTransform;

    [FormerlySerializedAs("_canvas")] [SerializeField] private Canvas canvas;
    
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float upLimit, downLimit;

    private Sprite _imageSprite;
    private bool isDragging;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _imageSprite = GetComponent<Image>().sprite;
    }

    
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("Begin drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("End drag");
        
        //gameObject.SetActive(false);
        
        _rectTransform.anchoredPosition = Vector2.zero;
        
        //gameObject.SetActive(true);
        
        
        var ray = InputHandler.mainCamera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin,Vector3.forward,Color.red,2f);

        var hit = Physics2D.Raycast(ray.origin, ray.direction, 50f);
        
        if(!hit.collider) return;

        if (!hit.transform.CompareTag("EditableImage")) return;

        if (!hit.transform.TryGetComponent(out ImageEditRefBank refBank)) return;
        
        if(!refBank.EditController) return;
        
        refBank.EditController.ReplaceImage(_imageSprite);


    }


    public void OnDrag(PointerEventData eventData)
    {
       
        var anchoredPosition = _rectTransform.anchoredPosition;
        var previPosition = anchoredPosition;

        anchoredPosition += eventData.delta / canvas.scaleFactor;
        _rectTransform.anchoredPosition = anchoredPosition;

        if (_rectTransform.anchoredPosition.x >= rightLimit)
        {
            _rectTransform.anchoredPosition = previPosition;
            return;
        }

        if (_rectTransform.anchoredPosition.x <= leftLimit)
        {
        
            _rectTransform.anchoredPosition = previPosition;
            return;
        }

        if (_rectTransform.anchoredPosition.y >= upLimit)
        {
            _rectTransform.anchoredPosition = previPosition;
            return;
        }
        
        if (_rectTransform.anchoredPosition.y <= downLimit)
        {
            _rectTransform.anchoredPosition = previPosition;
            return;
        }
        
        
       
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
