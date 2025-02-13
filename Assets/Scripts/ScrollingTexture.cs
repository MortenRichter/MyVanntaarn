using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    public float scrollSpeedX = 0.5f; // Juster farten horisontalt
    public float scrollSpeedY = 0.0f; // Juster farten vertikalt
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offsetX = Time.time * scrollSpeedX;
        float offsetY = Time.time * scrollSpeedY;
        rend.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
