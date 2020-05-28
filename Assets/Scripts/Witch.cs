using UnityEngine;
using System.Collections;

public class Witch : MonoBehaviour
{
    [Header("移動速度"), Range(1, 1000)]
    public float speed = 10;
    [Header("跳躍力度"), Range(1, 1000)]
    public float jumpHeight = 400;
    [Header("閃現距離"), Range(1, 1000)]
    public float flashLength = 3;
    [Header("玩家資料")]
    public PlayerData Data;

    [Header("魔法彈")]
    public GameObject magicBall;   
    [Header("閃現")]
    public GameObject magicFlash;

    [Header("魔法彈音效")]
    public AudioClip audMagicBall;


    public Transform magicPos;
    public Transform flashPos;

    AudioSource aud;
    public Joystick joy;
    private Animator ani;
    private Rigidbody2D rig;

    private bool isGrounded;
    private bool isAttack;
    private bool isFlash;
    

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();      
        
    }

    

    private void Update()
    {
       
        Move();
        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded ==true)
        {
            Jump();           
        }       
        if (Input.GetMouseButtonDown(0) && isAttack == false)
        {
            StartCoroutine(Attack());            
        }
        if (Input.GetMouseButtonDown(1) && isFlash ==false)
        {
            StartCoroutine(Flash());
            
        }
        if (Input.GetMouseButtonDown(2))
        {
            ani.SetBool("Cast", true);
        }
       
    }
    public void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {            
            ani.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            ani.SetBool("Walk", false);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {            
            ani.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            ani.SetBool("Walk", false);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //float v = Input.GetAxis("Vertical");   //Vertical:WS上下
        float h = Input.GetAxis("Horizontal"); //Horizontal:AD左右
        transform.Translate(speed * Time.deltaTime *Mathf.Abs(h), 0, 0);

       // float joyv = joy.Vertical;
       // float joyh = joy.Horizontal;
       // transform.Translate(speed * Time.deltaTime * joyh, 0, speed * Time.deltaTime * joyv);

        Vector2 pos = transform.position;    

    }

    
    

    public IEnumerator Attack()
    {
        
        ani.SetBool("Walk", false);
        speed = 0;
        isAttack = true;        
        ani.SetTrigger("Attack");
        aud.PlayOneShot(audMagicBall, 0.7f);
        yield return new WaitForSeconds(0.5f);
        GameObject temp =  Instantiate(magicBall, new Vector3(magicPos.position.x, magicPos.position.y, 0), transform.rotation);
       
        temp.GetComponent<Rigidbody2D>().AddForce(temp.transform.right * 1000);
        temp.AddComponent<Bullet>().damage = Data.damage;
        temp.GetComponent<Bullet>().player = true;
        
        yield return new WaitForSeconds(0.5f);
        isAttack = false;
        speed = 10;
        
    }           

    public IEnumerator Flash()
    {
        speed = 0;
        Instantiate(magicFlash, new Vector3(flashPos.position.x, flashPos.position.y, 0), Quaternion.Euler(-41,0,0));
        isFlash = true;
        ani.SetTrigger("Flash");
       
        yield return new WaitForSeconds(0.6f);
        speed = 10;
        transform.Translate(flashLength, 0, 0);
        yield return new WaitForSeconds(Data.flashCD);
        
        isFlash = false;
        
    }

    public void Jump()
    { 
        if(isGrounded == true)
        {
            isGrounded = false;
            rig.AddForce(new Vector2(0, jumpHeight));
            ani.SetTrigger("Jump");
        }       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "地板")
        {
            isGrounded = true;
            print("在地上");
        }
    }
}
