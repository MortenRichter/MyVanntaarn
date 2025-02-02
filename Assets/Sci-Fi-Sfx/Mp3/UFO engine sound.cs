using UnityEngine;

public class UFOAudioController : MonoBehaviour
{
    public Transform player;  // Dra spillerens transform hit i Inspector
    private AudioSource audioSource;
    
    public float maxVolume = 1.0f;  // Maks lydstyrke
    public float minVolume = 0.0f;  // Minste volum
    public float maxDistance = 10f; // Når spilleren er langt unna, er lyden svak

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0; // Start mutet
        audioSource.Play();  // Start lyden, men med lavt volum
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Beregner volum basert på hvor nær spilleren er UFO-en
        float volume = Mathf.Lerp(maxVolume, minVolume, distance / maxDistance);
        audioSource.volume = Mathf.Clamp(volume, minVolume, maxVolume);
    }
}

