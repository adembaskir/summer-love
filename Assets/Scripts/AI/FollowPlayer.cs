using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class FollowPlayer : MonoBehaviour
{
    public bool playerHasTouched;
    [Header("Takip Edeceði Hedef: ")]
    public Transform target;
    public NavMeshAgent myAgent;
    public Animator anim2;
    Animator anim;
    public Transform nextTarget;
    public UnityEvent onEnter;
    public int hit;
    public GameObject confetti;
   
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (playerHasTouched == true)
        {

            myAgent.SetDestination(target.position);
            transform.rotation = target.rotation;
            anim.SetBool("Running",true);
            StartCoroutine(WaitForHeartAnim());
        }
        if (PlayerMovement.instance.finish == true && playerHasTouched)
        {
            myAgent.SetDestination(nextTarget.position);
            transform.rotation = nextTarget.rotation;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hit += 1;
            if(hit == 1)
                UIManager.instance.score += 5;

            onEnter?.Invoke();  
            playerHasTouched = true;
            Handheld.Vibrate();
            anim2.SetBool("Disolve",true);
            //this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
    private IEnumerator WaitForHeartAnim()
    {
        yield return new WaitForSeconds(0.7f);
        confetti.SetActive(false);
    }

}
