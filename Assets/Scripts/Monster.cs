
using UnityEngine;
using UnityEngine.UI;


public class Monster : MonoBehaviour
{
    public Animator ani;
    private float timer;

    [Header("攻擊觸發")]
    public Transform atkTri;
    [Header("怪物資料")]
    public MonsterData Data;
    //[Header("補血藥水")]
    //public GameObject propHp;
    //[Header("加速藥水")]
    //public GameObject propCd;
    //[Header("子彈")]
    //public GameObject bullet;
    //public SpriteRenderer[] spr;

    private float hp;
    private float hpMax;
    private float r;
    public Image imgHP;

    void Start()
    {
        hp = Data.HP;
        hpMax = Data.HpMax;
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer > 3)
        {
            print(timer);
            timer = 0;

            r = Random.Range(0f, 1f);

            print(r);
            
        }

        if (r < 0.5) walk();
        else idle();

    }

    private void walk()
    {
        ani.SetBool("Walk", true);
        transform.Translate(-3 * Time.deltaTime, 0, 0);

    }
    private void attack()
    {
        ani.SetBool("Walk", false);

        ani.SetTrigger("Attack");

    }
    private void idle()
    {
        ani.SetBool("Walk", false);
    }
    private void dead()
    {
        ani.SetTrigger("Dead");
        
    }

    public Material monMaterial;

    public void hurt(float damage)
    {
        hp -= damage;
        monMaterial.color = Color.red;
        imgHP.fillAmount = hp / hpMax;
        Invoke("reColor", 0.2f);

        if (hp == 0) dead();
              
    }
    private void reColor()
    {
        monMaterial.color = Color.white;
       
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "玩家")
        {
            attack();
        }
      
    }
   

}
