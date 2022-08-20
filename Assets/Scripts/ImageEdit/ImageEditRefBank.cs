using UnityEngine;

public class ImageEditRefBank : MonoBehaviour
{

    public ImageEditController EditController { get; private set; }

    public ImageSelectHandler SelectHandler { get; private set; }


    private void Start()
    {
        if (transform.TryGetComponent(out ImageEditController controller))
            EditController = controller;

        if (transform.TryGetComponent(out ImageSelectHandler selectHandler))
            SelectHandler = selectHandler;

    }
}
