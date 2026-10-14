using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    public void OnBackButton()
    {
        SceneManager.LoadScene("Characters");
    }

}
