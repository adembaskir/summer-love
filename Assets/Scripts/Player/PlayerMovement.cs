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
        Time.timeScale = 1;
    }


    void Update()
    {
        FallCheck();
        
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
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        if (jump)
        {
            rb.velocity = new Vector3(rb.velocity.x,jumpSpeed);
            anim.SetBool("Jump", true);
            jump = false;
            grounded = false;
            anim.SetBool("Fall", true);
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
            
            if (anim !=null) { anim.SetBool("Jump", false); anim.SetBool("Fall",false); }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        onEnter?.Invoke();

        if (other.tag == "Heart")
        {
            UIManager.instance.score+=1;
            grounded = true;
            anim.SetBool("Jump", false);
            anim.SetBool("Fall", false);
            Handheld.Vibrate();
            StartCoroutine(WaitForEnd(other.gameObject));
            
        }
        if(other.tag=="Heart1")
        {
	    UIManager.instance.score+=1;
            StartCoroutine(WaitForEnd(other.gameObject));	
        }
        if(other.tag == "Obstacle")
        {
            onDead?.Invoke();
            anim.SetTrigger("Dead");
            UIManager.instance.elements[1].SetActive(true);
            PlayerMovement.instance.enabled = false;
        }
        if(other.tag == "Area")
        {
            finish = true;
            anim.SetTrigger("Kiss");
            UIManager.instance.HighScore();
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
            if(Time.timeScale == 0)
            {
                UIManager.instance.elements[1].SetActive(true);
            }
        }
    }
    IEnumerator WaitForEnd(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.7f);
        gameObject.SetActive(false);
    }
}
