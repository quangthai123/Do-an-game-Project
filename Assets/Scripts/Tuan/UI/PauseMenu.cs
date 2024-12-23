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
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main");
        audioSource.PlayOneShot(touchSFX);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("LevelSelect");
        audioSource.PlayOneShot(touchSFX);

        // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // int nextSceneIndex = currentSceneIndex + 1;

        // if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        // {
        //     SceneManager.LoadScene(nextSceneIndex);
        //     audioSource.PlayOneShot(touchSFX);
        // }
        // else
        // {
        //     Debug.LogWarning("No more levels to load!");
        // }
    }


}
