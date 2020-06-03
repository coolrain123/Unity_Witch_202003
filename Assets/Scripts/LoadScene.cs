using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class LoadScene : MonoBehaviour
{
   
    [Header("載入畫面音效")]
    public AudioClip audStart;

    public AudioSource aud;


    private void Start()
    {
        aud = GetComponent<AudioSource>();
       
    }
    public void LoadStage()
    {
       
        aud.PlayOneShot(audStart, 0.7f);              
       
        SceneManager.LoadScene("關卡1");
       
    }
}
