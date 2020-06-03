using UnityEngine;
using UnityEngine.UI;
using System.Collections;


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

    private HpValueManager hpValueManager;

    private float hp;
    private float hpMax;
    private float r;
    public Image imgHP;
    public Text textDmg;

    void Start()
    {
        hp = Data.HP;
        hpMax = Data.HpMax;
        hpValueManager = GetComponentInChildren<HpValueManager>();
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

        if (r < 0) walk();
        else idle();

    }

    private void walk()
    {
        ani.SetBool("Walk", true);

        float walk = Random.Range(0f, 1f);
        transform.eulerAngles = new Vector3(0, 180, 0);  //0或180??
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
  
    private IEnumerator Dead()
    {
        ani.SetTrigger("Dead");
        GetComponent<Monster>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        Destroy(this);
    }

    public Material monMaterial;

    public void hurt(float damage)
    {
        hp -= damage;
        monMaterial.color = Color.red;
       // imgHP.fillAmount = hp / hpMax;
        Invoke("reColor", 0.2f);
        
        hpValueManager.SetHp(hp, hpMax);
        StartCoroutine(hpValueManager.ShowValue(damage, "-", Color.white));
        if (hp == 0) StartCoroutine(Dead());
              
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
