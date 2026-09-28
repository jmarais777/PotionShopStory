using UnityEngine;
using UnityEngine.UI;

public class BasicPopup : MonoBehaviour
{
   //public GameObject mouseIcon;
    //public GameObject triggerIcon;
    //public GameObject textBox;
    public GameObject buttonIcons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //mouseIcon.SetActive(false);
       // triggerIcon.SetActive(false);
        //textBox.SetActive(false);
        buttonIcons.SetActive(false);
    }
    

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //mouseIcon.SetActive(true);
            //triggerIcon.SetActive(true);
            //textBox.SetActive(true);
            buttonIcons.SetActive(true);
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
            //mouseIcon.SetActive(false);
            //triggerIcon.SetActive(false);
            //textBox.SetActive(false);
            buttonIcons.SetActive(false);
        }
        else
        {
            return;
        }
    }

}
