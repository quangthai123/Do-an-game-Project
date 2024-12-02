
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectArea : MonoBehaviour
{
    [SerializeField] private AudioSource SfxButton;
    [SerializeField] private checkConnect _checkConnect;
    public void ConnectLibrary()
    {
        if(_checkConnect)
        SfxButton.Play();
        SceneManager.LoadScene("Library");
    }
    public void ConnectGame1()
    {
        if (_checkConnect)
            SfxButton.Play();
        SceneManager.LoadScene("Library");
    }
    public void ConnectGame2()
    {
        if (_checkConnect)
        SfxButton.Play();
        SceneManager.LoadScene("Library");
    }
    public void ConnectGame3()
    {
        if (_checkConnect)
        SfxButton.Play();
        SceneManager.LoadScene("Library");
    }
    public void ConnectGame4()
    {
        if (_checkConnect)
        SfxButton.Play();
        SceneManager.LoadScene("Library");
    }
    public void Leave()
    {
        SfxButton.Play();
        SceneManager.LoadScene("Main");
    }

}