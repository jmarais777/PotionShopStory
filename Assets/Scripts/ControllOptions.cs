using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllOptions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnKeyboard()
    {
        SceneManager.LoadScene("Keyboard Controls");
    }

    // Update is called once per frame
    public void OnGamepad()
    {
        SceneManager.LoadScene("Gamepad Controls");
    }

    public void OnReturn()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void OnBackOption()
    {
        SceneManager.LoadScene("Control Choices");
    }
}
