using UnityEngine;
using UnityEngine.UI;

public class BasicPopup : MonoBehaviour
{
    public GameObject mouseIcon;
    public GameObject triggerIcon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouseIcon.SetActive(false);
        triggerIcon.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            mouseIcon.SetActive(true);
            triggerIcon.SetActive(true);
        }
        else
        {
            return;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            mouseIcon.SetActive(false);
            triggerIcon.SetActive(false);
        }
        else
        {
            return;
        }
    }

}
