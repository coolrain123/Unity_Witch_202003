using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HpValueManager : MonoBehaviour
{
    private Image HpBar;
    private Text HpText;
    private RectTransform HpRect;
    private Vector2 original;
    

    // Start is called before the first frame update
    void Start()
    {
        HpBar = transform.GetChild(1).GetComponent<Image>();  //GetChild()   ()內跟陣列一樣  0 是第一個物件  1 才是第二個
        HpText = transform.GetChild(2).GetComponent<Text>();
        HpRect = transform.GetChild(2).GetComponent<RectTransform>();
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    private void FixedAngle()
    {
        transform.eulerAngles = new Vector3(40, 0, 0);
    }



    /// <summary>
    /// 血量調整
    /// </summary>
    /// <param name="hpCurrent">目前血量</param>
    /// <param name="hpMax">最大血量</param>
    public void SetHp(float hpCurrent, float hpMax)
    {
        HpBar.fillAmount = hpCurrent / hpMax;

    }


    public IEnumerator ShowValue(float value, string mark, Color color)
    {
        HpText.text = mark + value;
        color.a = 0;                           //透明度=0
        HpText.color = color;
        HpRect.anchoredPosition = original;

        for (int i = 0; i < 20; i++)
        {
            HpText.color += new Color(0, 0, 0, 0.05f);
            HpRect.anchoredPosition += Vector2.up * 0.3f;
            yield return new WaitForSeconds(0.01f);
        }

        HpText.color = new Color(0, 0, 0, 0);
    }


}
