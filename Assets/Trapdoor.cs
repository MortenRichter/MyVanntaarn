using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Sjekker om det er spilleren som tråkker på fellen
        {
            other.GetComponent<CharacterController>().enabled = false; // Midlertidig disable for å unngå rare kollisjoner
            other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y - 5, other.transform.position.z); // Flytter spilleren ned
            other.GetComponent<CharacterController>().enabled = true; // Enable igjen så fysikken fungerer som normalt
        }
    }
}
