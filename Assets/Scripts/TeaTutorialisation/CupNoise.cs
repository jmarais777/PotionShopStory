using UnityEngine;

public class CupNoise : MonoBehaviour
{
    [SerializeField] private AudioSource source; 

    void Awake()
    {
        source = GetComponent<AudioSource>(); //get the woosh sound
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Tea"))
        {
            source.Play();

        }
        
    }


}
