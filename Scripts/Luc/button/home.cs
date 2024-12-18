
using UnityEngine;

public class home : MonoBehaviour
{
    [SerializeField] private GameObject option;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject setting;
    [SerializeField] private GameObject exit;
    [SerializeField] private AudioSource SfxButton;
    private pause _pause;
    private bool statusHome = false;
    private bool statusMap = false;
    private bool statusSetting = false;
    private bool statusExit = false;

    void Start()
    {
        option.SetActive(false);
        map.SetActive(false);
        setting.SetActive(false);
        exit.SetActive(false);
        _pause = GetComponent<pause>();
    }

    public void controllHome()
    {
        statusHome = !statusHome;
        SfxButton.Play();
        if (statusHome == true)
        {
            _pause.Stop();
            option.SetActive(true);
        }
        else {
            _pause.Continue();
            option.SetActive(false);
        
        }

    }
     public void controllMap()
    {
        statusMap = !statusMap;
        SfxButton.Play();
        if (statusMap == true)
        {
            map.SetActive(true);
        }
        else if (statusMap == false) {
            map.SetActive(false);
        }
    }
    public void controllSetting()
    {
        SfxButton.Play();
        statusSetting = !statusSetting;
        if (statusSetting == true)
        {
            setting.SetActive(true);
        }
        else if (statusSetting == false)
        {
            setting.SetActive(false);
        }
    }
    public void controllExit()
    {
        SfxButton.Play();
        statusExit = !statusExit;
        if (statusExit == true)
        {
            exit.SetActive(true);
        }
        else { 
            exit.SetActive(false);
        }
    }
    public void Exit()
    {
        SfxButton.Play();
        Application.Quit();
    }
}
