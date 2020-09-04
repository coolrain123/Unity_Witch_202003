using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class Pause : MonoBehaviour
{
    [Header("暫停畫面")]
    public GameObject panelPause;
    [Header("死亡畫面")]
    public GameObject panelDead;

    public void PauseBtn()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("場景/學院介面");
        Time.timeScale = 1;
    }
    public void BackToGame()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1;
    }
  
}
