using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Monster : MonoBehaviour
{
    public Animator ani;
    private float timer;
    private float walkTimer;
    int walkDirection = 0;
    
    

   
    [Header("攻擊特效")]
    public GameObject atkEff;
    [Header("怪物資料")]
    public MonsterData Data;

    [Header("普通掉落")]
    public GameObject propDrop;
    [Header("罕見掉落")]
    public GameObject propRare;
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

    public Transform Target;

   
    private bool SeePlayer;
    private bool actionAtk;

    void Start()
    {
        //如何設定攻擊觸發與視野觸發
        hp = Data.HP;
        hpMax = Data.HpMax;
        hpValueManager = GetComponentInChildren<HpValueManager>();
        HandleCollision();


        //如何抓材質變為群組並變色
        //Transform monTrans = GetComponent<Transform>();
        //for (int i = 1; i < 8; i++)
        //{
        //    transform.GetChild(i).GetComponent<SpriteRenderer>().material = Instantiate(transform.GetChild(i).GetComponent<SpriteRenderer>().material);
        //    Material[] m = monTrans.GetChild(i).GetComponent<SpriteRenderer>().material;   //取得材質
        //}

        actionAtk = false;
    }

    /// <summary>
    /// 控制忽略碰撞
    /// </summary>
    private void HandleCollision()
    {
       
        Physics2D.IgnoreLayerCollision(9, 9);
        Physics2D.IgnoreLayerCollision(9, 10);
        Physics2D.IgnoreLayerCollision(10,10);
    }

    
    void Update()
    {

        seeCheck();
        
        if (SeePlayer) return;
        timer += Time.deltaTime;
        if (timer > 3)
        {
            print(timer);
            timer = 0;
            r = Random.Range(0f, 1f);                
           
        }

        if (r < 0.5) walk();
        else idle();


        
        //如果攻擊  Walk取消  變follow
    }

    


    /// <summary>
    /// 閒晃
    /// </summary>
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
          
        }
        else if (walkDirection == 2)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);  //0或180??
            transform.Translate(-3 * Time.deltaTime, 0, 0);
            hpValueManager.transform.localEulerAngles = new Vector3(0, 0, 0);
           
        }
        
    }
    

    /// <summary>
    /// 攻擊
    /// </summary>
    /// <returns></returns>
    private IEnumerator Attack()
    {                  
        if(actionAtk==false)
        {
            actionAtk = true;
            ani.SetBool("Walk", false);

            ani.SetTrigger("Attack");
            yield return new WaitForSeconds(0.8f);
            GameObject temp =  Instantiate(atkEff, new Vector3(atkPos.position.x, atkPos.position.y), Quaternion.identity);

            int rDmg = Random.Range(1, 8); 
            temp.AddComponent<Bullet>().damage = Data.damage +rDmg ;
            temp.GetComponent<Bullet>().player = false;
            yield return new WaitForSeconds(Data.cd);
            actionAtk = false;
        }
                       
                
    }

    /// <summary>
    /// 閒置
    /// </summary>
    private void idle()
    {
        ani.SetBool("Walk", false);
        transform.Translate(0, 0, 0);
    }
    /// <summary>
    /// 死亡
    /// </summary>
    /// <returns></returns>
    private IEnumerator Dead()
    {
        ani.SetTrigger("Dead");
        GetComponent<Monster>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        
       
        dropProp();
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }

    public void dropProp()
    {
        float rDrop = Random.Range(0f, 1f);
        if (rDrop < Data.propDropPer) Instantiate(propDrop, transform.position + Vector3.up *10, Quaternion.identity);

        float rRareDrop = Random.Range(0f, 1f);
        if (rRareDrop < Data.propDropRarePer) Instantiate(propRare, transform.position + Vector3.up * 15, Quaternion.identity);
    }


    public void hurt(float damage)
    {
        hp -= damage;
        //monMaterial.color = Color.red;      
        //Invoke("reColor", 0.2f);
        
        hpValueManager.SetHp(hp, hpMax);
        StartCoroutine(hpValueManager.ShowValue(damage, "-", Color.white));
        if (hp <= 0) StartCoroutine(Dead());

              
    }

    public IEnumerator StartPoison(float damage,int duration)
    {
       
        for (int i = 0; i < duration; i++)
        {
            
            hp -= damage;
            if (hp <= 0) StartCoroutine(Dead());
            //monMaterial.color = Color.green;
            //Invoke("reColor", 0.5f);
            hpValueManager.SetHp(hp, hpMax);
            StartCoroutine(hpValueManager.ShowValue(damage, "-", Color.white));
            
            yield return new WaitForSeconds(1);
                        
        }       
               
    }
   
    private void reColor()
    {
        monMaterial.color = Color.white;       
    }


    
   
    float closeSpeed;
    bool atkIn;
    private void seeCheck()
    {
        RaycastHit2D hit2DSee = Physics2D.Raycast(transform.position - transform.right + new Vector3(0, 8, 0), -transform.right, Data.seeRange);
        
        if (hit2DSee.transform != null)
        {
            //if (atkIn) return;
            if (hit2DSee.collider.tag == "玩家")
            {
                print("看見玩家");
                SeePlayer = true;
                ani.SetBool("Walk", true);
                transform.Translate(closeSpeed * Time.deltaTime, 0, 0);
                attackCheck();
                if (atkIn) closeSpeed = 0;
                else closeSpeed = -5;
            }
        }
        else if (hit2DSee.transform == null)
        {
            SeePlayer = false;
        }

    }

    private void attackCheck()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position - transform.right + new Vector3(0, 8, 0), -transform.right, Data.atkRange);

        if (hit2D.transform != null)
        {
            if (hit2D.collider.tag == "玩家")
            {
                atkIn = true;
                print("碰到玩家");
                ani.SetBool("Walk", false);
                closeSpeed = 0;
                StartCoroutine(Attack());               
            }           
        }
        else if (hit2D.transform == null)
        {
            atkIn = false;
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
        Gizmos.DrawRay(transform.position - transform.right + new Vector3(0, 8, 0), transform.right * -Data.atkRange);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position - transform.right + new Vector3(0, 8, 0), transform.right * -Data.seeRange);


    }

   

}
