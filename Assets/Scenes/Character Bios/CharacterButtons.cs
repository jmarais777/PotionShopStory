using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterButtons : MonoBehaviour
{
    public void OnSylvan()
    {
        SceneManager.LoadScene("Sylvan");
    }

    public void OnArchibald()
    {
        SceneManager.LoadScene("frog");
    }

    public void OnEdwin()
    {
        SceneManager.LoadScene("bird");
    }

    public void OnBalthazar()
    {
        SceneManager.LoadScene("Balthazar");
    }

    public void OnBack()
    {
        SceneManager.LoadScene("Start Menu");
    }


}


