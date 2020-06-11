using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Monster : MonoBehaviour
{
    public Animator ani;
    private float timer;
    private float walkTimer;
    int walkDirection = 0;
    
    

    //[Header("攻擊觸發")]
    //public GameObject atkTri;
    //[Header("視野觸發")]
    //public GameObject seeTri;
    [Header("攻擊特效")]
    public GameObject atkEff;
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
    public Transform atkPos;
    public Material monMaterial;

    private bool Atk;

    void Start()
    {
        //如何設定攻擊觸發與視野觸發
        hp = Data.HP;
        hpMax = Data.HpMax;
        hpValueManager = GetComponentInChildren<HpValueManager>();
        //atkTri = GameObject.Find("攻擊觸發");
        //seeTri = GameObject.Find("視野觸發");
        Atk = false;
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
            
        }

        if (r < 0.5) walk();
        else idle();
        attackCheck();
    }

    private void walk()
    {
        ani.SetBool("Walk", true);
        walkTimer += Time.deltaTime;
        
        if (walkTimer>3)
        {
            walkTimer = 0;
            walkDirection = Random.Range(1, 3);
                      
        }

        if (walkDirection == 1)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);  //0或180??
            transform.Translate(-3 * Time.deltaTime, 0, 0);
            hpValueManager.transform.localEulerAngles = new Vector3(0, 180, 0);
            //attackCheck();
        }
        else if (walkDirection == 2)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);  //0或180??
            transform.Translate(-3 * Time.deltaTime, 0, 0);
            hpValueManager.transform.localEulerAngles = new Vector3(0, 0, 0);
            //attackCheck();
        }


    }
    

    private IEnumerator Attack()
    {
        if(Atk == false)
        {

            Atk = true;
            ani.SetBool("Walk", false);
            
            ani.SetTrigger("Attack");
            yield return new WaitForSeconds(0.5f);
            Instantiate(atkEff, new Vector3(atkPos.position.x , atkPos.position.y), Quaternion.identity);

            yield return new WaitForSeconds(Data.cd);
            Atk = false;

        }
       
        
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
        Destroy(gameObject);
    }

   

    public void hurt(float damage)
    {
        hp -= damage;
        monMaterial.color = Color.red;      
        Invoke("reColor", 0.2f);
        
        hpValueManager.SetHp(hp, hpMax);
        StartCoroutine(hpValueManager.ShowValue(damage, "-", Color.white));
        if (hp <= 0) StartCoroutine(Dead());
              
    }

    public IEnumerator StartPoison(float damage,int duration)
    {
        print("毒瓶");
        print(duration);
        for (int i = 0; i < duration; i++)
        {
            hp -= damage;
            monMaterial.color = Color.green;
            Invoke("reColor", 0.2f);
            hpValueManager.SetHp(hp, hpMax);
            StartCoroutine(hpValueManager.ShowValue(damage, "-", Color.white));
            if (hp <= 0) StartCoroutine(Dead());
            yield return new WaitForSeconds(1);

            
        }       

       
    }

    

   
    private void reColor()
    {
        monMaterial.color = Color.white;
       
    }


    
    private void attackCheck()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position - transform.right + new Vector3(0, 5, 0), -transform.right, 3);



        if (hit2D.collider.tag == "玩家")
        {
            print("遇到玩家方法");
            StartCoroutine(Attack());
        }
    }




  

    //繪製圖示，僅在場景顯示給開發者觀看
    private void OnDrawGizmos()
    {
        //圖示顏色
        Gizmos.color = Color.red;

        //前方Z transform.forward
        //右方X transform.right
        //上方Y transform.up
        //繪製射線(起點,方向*長度)
        Gizmos.DrawRay(transform.position - transform.right + new Vector3(0, 5, 0), transform.right * -3);


    }

}
