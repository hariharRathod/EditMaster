using UnityEngine;

public class CutCheckHandler : MonoBehaviour
{
    public void CheckCutResult(int result)
    {
        if (result < 0)
        {
            GameEvents.InvokeOnCutNotAccurate();
            return;
        }
        
        //invoke cut success
        GameEvents.InvokeOnCutDoneAccurately();
        
    }
}
