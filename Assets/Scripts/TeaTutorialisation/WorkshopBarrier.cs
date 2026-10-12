using UnityEngine;

public class WorkshopBarrier : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(BrewTea.isTeaComplete == true)
        {
            gameObject.SetActive(false);
        }
    }
}
