
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("載入畫面")]
    public GameObject panelLoading;
    [Header("載入畫面文字")]
    public Text textLoading;
    [Header("載入畫面讀條")]
    public Image imgLoading;



    public void StartLoading()
    {
        print("開始載入....");
        panelLoading.SetActive(true);
        textLoading.text = "99 %";
        imgLoading.fillAmount = 0.99f;
        //SceneManager.LoadScene("關卡1");
        StartCoroutine(Loading());
    }

    /// <summary>
    /// 協程方法:載入
    /// </summary>
    /// <returns></returns>
    public IEnumerator Loading()
    {
        print("載入中....");
        yield return new WaitForSeconds(1);
        print("載入中........123456");
    }
}
