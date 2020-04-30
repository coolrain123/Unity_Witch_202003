using UnityEngine;
using System.Collections;

public class Witch : MonoBehaviour
{
    [Header("移動速度"), Range(1, 1000)]
    public float speed = 5;
    [Header("跳躍高度"), Range(1, 1000)]
    public float jumpHeight = 3;
    [Header("閃現距離"), Range(1, 1000)]
    public float flashLength = 3;

    private Animator ani;
    private Rigidbody2D rig;
    
    private bool isAttack;
    private bool isFlash;

    private void Start()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
       
        isAttack = false;
        isAttack = false;
    }

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
           
        }
        Move();
        if (Input.GetMouseButtonDown(0) )
        {
            Attack();
            isAttack = false;
        }
        if (Input.GetMouseButtonDown(1)&&isFlash ==false)
        {
            StartCoroutine(Flash());
            isFlash = false;
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

        //float joyv = joy.Vertical;
        //float joyh = joy.Horizontal;
        //transform.Translate(speed * Time.deltaTime * joyh, 0, speed * Time.deltaTime * joyv);

        Vector2 pos = transform.position;    

    }

    
    public void Jump()
    {
       
            rig.AddForce(new Vector2(0, jumpHeight));
            ani.SetTrigger("Jump");
                   
    }

    public void Attack()
    {
        if(isAttack == false)
        {
            isAttack = true;
            ani.SetTrigger("Attack");
        }
        
    }

    public IEnumerator Flash()
    {
       
        ani.SetTrigger("Flash");
        yield return new WaitForSeconds(0.6f);
        transform.Translate(flashLength, 0, 0);       
    }

   
    
}
