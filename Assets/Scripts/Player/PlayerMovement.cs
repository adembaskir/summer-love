using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement instance;
    public float moveSpeed,jumpSpeed,jumpShortSpeed;
    Rigidbody rb;
    float touchPosY;
    public bool jump,jumpCancel;
    public bool grounded;
    public bool finish;
    Animator anim;

    public UnityEvent onDead;
    public UnityEvent onEnter;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        anim = this.GetComponentInChildren<Animator>();
        rb = this.GetComponent<Rigidbody>();
    }


    void Update()
    {
        FallCheck();
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        //if (TouchController.Instance.isTouch == true)
        //{
        //    if (canJump == true)
        //    {
        //        rb.velocity = Vector3.zero;
        //        rb.velocity += jumpSpeed * Vector3.up;
        //        anim.SetBool("Jump", true);
        //        StartCoroutine(WaitForJump());
        //    }

        //}
        if (Input.GetMouseButton(0) && grounded)
        {
            jump = true;
        }
        if (Input.GetMouseButtonUp(0) && !grounded)
        {
            jumpCancel = true;
        }
    }
    void FixedUpdate()
    {
        if (jump)
        {
            rb.velocity = new Vector3(rb.velocity.x,jumpSpeed);
            jump = false;
            grounded = false;
        }
        if (jumpCancel)
        {
            if (rb.velocity.y > jumpShortSpeed)
                rb.velocity = new Vector2(rb.velocity.x, jumpShortSpeed);
            jumpCancel = false;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag =="Ground")
        {
            grounded = true;
            
            if (anim !=null) { anim.SetBool("Jump", false); }
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        onEnter?.Invoke();
        if(other.tag == "Heart")
        {
            grounded = true;
            anim.SetBool("Jump", false);
            Handheld.Vibrate();
            
        }
        if(other.tag == "Obstacle")
        {
            onDead?.Invoke();
            anim.SetTrigger("Dead");
            PlayerMovement.instance.enabled = false;
        }
        if(other.tag == "Area")
        {
            finish = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Heart")
        {
            grounded = false;
        }
    }
    public void FallCheck()
    {
        if (transform.position.y <= -1)
        {
            Time.timeScale = 0;
        }
    }
}
