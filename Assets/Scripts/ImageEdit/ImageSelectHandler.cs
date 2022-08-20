using System;
using DG.Tweening;
using UnityEngine;

public class ImageSelectHandler : MonoBehaviour
{

    private Material outlineMaterial;

    private static readonly String OutlineEnabled = "_OutlineEnabled";
    private static readonly String FrameTex ="_FrameTex";
    private float yOffset;
    private Tween textureOffsetTween;
    private void Start()
    {
        
        outlineMaterial = GetComponent<Renderer>().material;
        DisableOutLine();
        yOffset = 0;

    }

    private void DisableOutLine()
    {
        outlineMaterial.SetFloat(OutlineEnabled,0);
        textureOffsetTween.Kill();
    }

    private void EnableOutline()
    {
        outlineMaterial.SetFloat(OutlineEnabled,1);
        AnimateOffsetTexture();
    }

    private void AnimateOffsetTexture()
    {

        yOffset = 0;
        textureOffsetTween=DOVirtual.DelayedCall(0.2f,()=>
        {

            yOffset -= 0.2f;
            outlineMaterial.SetTextureOffset(FrameTex,new Vector2(0,yOffset));

        }).SetLoops(-1);
    }


    public void OnImageSelected(bool shouldBeOutlined)
    {
        if(shouldBeOutlined)
            EnableOutline();
        else
            DisableOutLine();
    }
}
