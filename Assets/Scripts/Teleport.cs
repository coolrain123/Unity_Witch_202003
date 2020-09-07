using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Teleport : MonoBehaviour
{

    private Image imgcross;
    public GameObject magicTP;


    void Start()
    {
        imgcross = GameObject.Find("轉場效果").GetComponent<Image>();


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "玩家")
        {
            print("傳送");
            Instantiate(magicTP, transform.position, transform.rotation);
            StartCoroutine(NextLevel());
        }
    }
   

    public IEnumerator Fadeout()
    {

        for (int k = 0; k < 50; k++)
        {
            imgcross.color -= new Color(0, 0, 0, 0.03f);
            yield return new WaitForSeconds(0.01f);
        }

    }


    public IEnumerator NextLevel()
    {
        for (int k = 0; k < 50; k++)
        {
            imgcross.color += new Color(0, 0, 0, 0.03f);
            yield return new WaitForSeconds(0.02f);
        }

        SceneManager.LoadScene("學院介面");
        yield return new WaitForSeconds(1f);

    }
}