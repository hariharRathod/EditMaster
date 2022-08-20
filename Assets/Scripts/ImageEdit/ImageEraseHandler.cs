using UnityEngine;

public class ImageEraseHandler : MonoBehaviour
{
    private ImageEditRefBank _my;


    private void Start()
    {
        _my = GetComponent<ImageEditRefBank>();
    }
    
    
   public void OnEraserUsed()
   {
        this.gameObject.SetActive(false);
   }
  
}
