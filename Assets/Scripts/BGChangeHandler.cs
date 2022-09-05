using UnityEngine;

public class BGChangeHandler : MonoBehaviour
{
    [SerializeField] private GameObject bg, instaBgAfterWin;

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
        bg.SetActive(true);
        instaBgAfterWin.SetActive(false);
    }

    private void OnEditCorrect()
    {
        bg.SetActive(false);
        instaBgAfterWin.SetActive(true);
    }
}
