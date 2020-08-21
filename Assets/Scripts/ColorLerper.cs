using UnityEngine;

public class ColorLerper : MonoBehaviour
{
    public Color targetColor = Color.white;
    public bool initTargetColorToSprite = true;

    public float lerpSpeed = 10f;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        targetColor = spriteRenderer.color;
    }

    private void Update()
    {
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, Time.deltaTime * lerpSpeed);       
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public void SetColorWhite()
    {
        spriteRenderer.color = Color.white;
    }
}
