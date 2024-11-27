
using UnityEngine;
using UnityEngine.SceneManagement;

public class startMenu : MonoBehaviour
{
    [SerializeField] private AudioSource SfxButton;
    public void StartGame()
    {
        SfxButton.Play();
        SceneManager.LoadScene("Main");
    }
    public void Exit()
    {
        SfxButton.Play();
        Application.Quit();
    }

}