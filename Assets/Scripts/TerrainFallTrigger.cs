using UnityEngine;

public class TerrainFallTrigger : MonoBehaviour
{
    public Collider terrainCollider; // Dra inn Terrain Collider fra Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            terrainCollider.enabled = false; // Deaktiver terreng-kollisjon
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            terrainCollider.enabled = true; // Skru den på igjen når spilleren er ute av fella
        }
    }
}
