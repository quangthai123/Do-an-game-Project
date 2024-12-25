using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] private AudioClip touchSFX;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private playerData _playerData;

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
        audioSource.PlayOneShot(touchSFX);
        Time.timeScale = 1f;
        _playerData.statusLv1 = false;
        _playerData.statusLv2 = false;
        SceneManager.LoadScene("Screen Main");
    }


}
