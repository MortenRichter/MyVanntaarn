using UnityEngine;

public class FlowAnimation : MonoBehaviour
{
    public float flowSpeed = 0.1f; // Juster hastigheten på vannstrømmen
    private Material material;

    void Start()
    {
        // Hent materialet fra mesh renderer
        material = GetComponent<Renderer>().material;
    }

   void Update()
{
    Vector2 currentOffset = material.GetTextureOffset("_MainTex");
    currentOffset.x += flowSpeed * Time.deltaTime;
    material.SetTextureOffset("_MainTex", currentOffset);

    Debug.Log("Updating texture offset: " + currentOffset); // For debugging
}

}
