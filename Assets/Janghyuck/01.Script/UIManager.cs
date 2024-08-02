using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject creditPanel;
    [SerializeField] private GameObject exitPanel;

    public void OnClickStart()
    {
        SceneManager.LoadScene("Tutorial");        
    }

    public void OnClickSetting() 
    {
        settingPanel.SetActive(true);
    }

    public void OffClickSetting()
    {
        settingPanel.SetActive(false);
    }

    public void OnClickCredit()
    {
        creditPanel.SetActive(true);
    }

    public void OffClickCredit()
    {
        creditPanel.SetActive(false);
    }

    public void OnClickExit()
    {
        exitPanel.SetActive(true);
    }

    public void OffClickExit()
    {
        exitPanel.SetActive(false);
    }

    public void GameExit()
    {
        Debug.Log("GameExit");
        Application.Quit();
    }
}
