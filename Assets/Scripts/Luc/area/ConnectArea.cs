
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectArea : MonoBehaviour
{
    [SerializeField] private AudioSource SfxButton;
    [SerializeField] private checkConnect _checkConnect;
    [SerializeField] private playerData _playerData;
    public void ConnectLibrary()
    {

            SfxButton.Play();
            SceneManager.LoadScene("Library");

    }
    public void ConnectGame1()
    {

            SfxButton.Play();
            _playerData.statusLv1 = true;
            SceneManager.LoadScene("Level1");
    }
    public void ConnectGame2()
    {

            SfxButton.Play();
            _playerData.statusLv2 = true;
            SceneManager.LoadScene("Minigame_SapXepChuCai");

    }
    public void ConnectGame3()
    {

            SfxButton.Play();
            _playerData.statusLv3 = true;
            SceneManager.LoadScene("Minigame_DaoAnh");

    }
    public void ConnectGame4()
    {

            SfxButton.Play();
            SceneManager.LoadScene("Library");
    }
    public void ConnectGame5()
    {

        SfxButton.Play();
        SceneManager.LoadScene("Library");
    }
    public void Leave()
    {
        SfxButton.Play();
        //_playerData.statusLv1 = false ;
        //_playerData.statusLv2 = false ;
        //_playerData.statusLv3 = false ;
        SceneManager.LoadScene("Screen Main");
    }

}