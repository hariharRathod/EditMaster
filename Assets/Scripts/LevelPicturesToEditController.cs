using System.Collections.Generic;
using UnityEngine;

public class LevelPicturesToEditController : MonoBehaviour
{
   [SerializeField] private List<GameObject> picturesToEditsList;

   private int _counter=0;
   
   private void Start()
   {

      if (picturesToEditsList.Count == 0) return;

      GameObject picture = Instantiate(picturesToEditsList[_counter], transform);
      
      GameEvents.InvokeOnPicturePrefabInstantiateDone(picture);
   }
}
