using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour // 포톤 없이 실행되는 움직임
{
    public JoyStick joystick;
    public float MoveSpeed;
    SpriteRenderer spriteRenderer;

    public Vector3 _moveVector;
    private Transform _transform;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        _moveVector = Vector3.zero;
       
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        joystick = GameObject.FindGameObjectWithTag("JoyStick").GetComponent<JoyStick>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        AnimRPC();
    }

    void FixedUpdate()
    {
        Move();
        this.animator.SetBool("isWalking", false);
    }

  
    public void HandleInput()
    {
        _moveVector = PoolInput();
    }

    
    void FlipRPC(Vector3 mv)
    {
        this.animator.SetBool("isWalking", false);

        if (mv.x < 0)
        {
            // Debug.Log("뒤집기1");
            //spriteRenderer.flipX = true; 
            transform.localScale = new Vector3(-0.08f, 0.08f, 1f);
            animator.SetBool("isWalking", true);
        }

        if (mv.x > 0)
        {
            //Debug.Log("뒤집기2");
            //spriteRenderer.flipX = false;
            transform.localScale = new Vector3(0.08f, 0.08f, 1f);
            animator.SetBool("isWalking", true);
        }
    }

   
    void AnimRPC()
    {
        float h = joystick.GetHorizontalValue();
        float v = joystick.GetVerticalValue();

        if (h == 0 || v == 0)
        {
            //Debug.Log("정지");
            this.animator.SetBool("isWalking", false);
        }
        else
        {
            Debug.Log("움직임");
            this.animator.SetBool("isWalking", true);
        }
    }

    
    public Vector3 PoolInput()
    {
        float h = joystick.GetHorizontalValue();
        float v = joystick.GetVerticalValue();
        Vector3 moveDIr = new Vector3(h, v, 0).normalized;

        if (moveDIr.x < 0)
        {
            //Debug.Log("뒤집기1");
            //spriteRenderer.flipX = true; 
            transform.localScale = new Vector3(-0.08f, 0.08f, 1f);
        }

        if (moveDIr.x > 0)
        {
            //Debug.Log("뒤집기2");
            //spriteRenderer.flipX = false;
            transform.localScale = new Vector3(0.08f, 0.08f, 1f);
        }

        /* if(h == 0 || v == 0)
         {
             //Debug.Log("정지");
             this.animator.SetBool("isWalking", false);
         }
         else
         {
             Debug.Log("움직임");
             this.animator.SetBool("isWalking", true);
         } */

        return moveDIr;
    }

    public void Move()
    {
        _transform.Translate(_moveVector * MoveSpeed * Time.deltaTime);
    }
}
