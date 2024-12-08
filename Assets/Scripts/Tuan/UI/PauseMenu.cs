using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    [SerializeField] private AudioClip touchSFX;
    [SerializeField] private AudioSource audioSource;



    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        audioSource.PlayOneShot(touchSFX);
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        audioSource.PlayOneShot(touchSFX);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        audioSource.PlayOneShot(touchSFX);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main");
        audioSource.PlayOneShot(touchSFX);
    }


}
