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

    public void Home()
    {
        SceneManager.LoadScene("StartScene");
        audioSource.PlayOneShot(touchSFX);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("LevelSelect");
        audioSource.PlayOneShot(touchSFX);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("Main");
        audioSource.PlayOneShot(touchSFX);
        Time.timeScale = 1f;
        _playerData.statusLv1 = false;
        _playerData.statusLv2 = false;
        SceneManager.LoadScene("Screen Main");
    }

    public void Exit()
    {
        SceneManager.LoadScene("Screen Main");
        audioSource.PlayOneShot(touchSFX);
        musicManager.StopMusic();
    }

}
