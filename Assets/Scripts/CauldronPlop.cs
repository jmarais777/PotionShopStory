using UnityEngine;

public class CauldronPlop : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float cooldown = 0.5f;

    private float nextAllowedTime;

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < nextAllowedTime) return;

        nextAllowedTime = Time.time + cooldown;
        audioSource.PlayOneShot(audioSource.clip);
    }
}