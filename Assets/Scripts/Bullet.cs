using UnityEngine;

public class Bullet : MonoBehaviour
{

    /// <summary>
    /// 子彈的傷害
    /// </summary>
    public float damage;
    public GameObject magicHit;

    /// <summary>
    /// 是否為玩家的武器  true 玩家的  false  敵人的
    /// </summary>
    public bool player;


  

    /// <summary>
    /// 有勾選 IsTrigger 的物件，偵測碰到其他物件執行一次
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (player && other.tag == "怪物")
        {
            print("打到怪");
            other.GetComponent<Monster>().hurt(damage);   //取得<敵人> 的 hurt 方法            
            Destroy(gameObject);
        }
        if (other.tag == "玩家" )
        {

            print("打到玩家");
            other.GetComponent<Witch>().hurt(damage);   //取得<敵人> 的 hurt 方法            
            
        }
    }

    
   
}
