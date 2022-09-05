using DG.Tweening;
using UnityEngine;

public class PicHolderController : MonoBehaviour
{
    [SerializeField] private GameObject instaCommentsBg;
    [SerializeField] private Ease imageInEase;
    private void OnEnable()
    {
        GameEvents.EditCorrect += OnEditCorrect;
        
    }

    private void OnDisable()
    {
        GameEvents.EditCorrect -= OnEditCorrect;
       
    }

    private void Start()
    {
        instaCommentsBg.SetActive(false);
    }
    
    private void OnEditCorrect()
    {
        instaCommentsBg.SetActive(true);
        
        transform.rotation = Quaternion.Euler(45,180,90);

        transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 0.85f).SetEase(imageInEase);
    }
}
