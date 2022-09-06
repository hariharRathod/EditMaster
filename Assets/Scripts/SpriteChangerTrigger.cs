using System;
using UnityEngine;

public class SpriteChangerTrigger : MonoBehaviour
{

    [SerializeField] private float distThershold;
    [SerializeField] private ImageEditSpriteReplace imageEditSpriteReplace;
    private Transform _transform;

    private bool _stop;
    
    
    //agar break hua to..........
    //event use kar to broadcast drag image ke position.
    
    private void OnEnable()
    {
        GameEvents.EditCorrect += EnableStop;
        GameEvents.EditIncorrect +=  EnableStop;
    }

    private void OnDisable()
    {
      
        GameEvents.EditCorrect -=  EnableStop;
        GameEvents.EditIncorrect -=  EnableStop;
    }

    
    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (_stop) return;
        
        
        float dist = (_transform.position - imageEditSpriteReplace.transform.position).magnitude;
        
        print("distance: " + dist);

        if (dist > distThershold)
        {
            imageEditSpriteReplace.ReplaceWithFullLegSprite();
            return;
        }
        
        imageEditSpriteReplace.ReplaceWithOneLegSprite();
    }
    
    
    private void EnableStop()
    {
        _stop = true;
    }
}
