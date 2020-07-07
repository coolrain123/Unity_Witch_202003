using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class MenuManager : MonoBehaviour
{
    [Header("載入畫面")]
    public GameObject panelLoading;
    [Header("暫停畫面")]
    public GameObject panelPause;
    [Header("載入畫面文字")]
    public Text textLoading;
    [Header("載入畫面讀條")]
    public Image imgLoading;
    [Header("載入畫面音效")]
    public AudioClip audStart;

    public AudioSource aud;

    public void StartLoading()
    {
        aud = GetComponent<AudioSource>();
        aud.PlayOneShot(audStart, 0.7f);
        print("開始載入....");
        panelLoading.SetActive(true);        
        SceneManager.LoadScene("學院介面");
        
    }

    /// <summary>
    /// 協程方法:載入
    /// </summary>
    /// <returns></returns>
    public IEnumerator Loading()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync("學院介面");
       
        ao.allowSceneActivation = false; //是否自動載入畫面 = 否
        while (ao.progress < 1)
        {
            print("關卡進度" + ao.progress);
            yield return null;

            textLoading.text = (ao.progress / 0.9 * 100).ToString("F2") + "  %";
            imgLoading.fillAmount = ao.progress;

            if (ao.progress == 0.9f)
                ao.allowSceneActivation = true;
        }
    }

  
}
