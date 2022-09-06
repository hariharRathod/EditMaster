using System;
using UnityEngine;

public class ImageEditSpriteReplace : MonoBehaviour
{

    [SerializeField] private Sprite oneLegSprite,fullLegSprite;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ReplaceWithOneLegSprite()
    {
        _spriteRenderer.sprite = oneLegSprite;
    }

    public void ReplaceWithFullLegSprite()
    {
        _spriteRenderer.sprite = fullLegSprite;
    }
}
