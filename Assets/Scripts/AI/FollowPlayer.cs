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
        }
        if (PlayerMovement.instance.finish == true)
        {
            myAgent.SetDestination(nextTarget.position);
            transform.rotation = nextTarget.rotation;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            onEnter?.Invoke();  
            playerHasTouched = true;
            Handheld.Vibrate();
            anim2.SetTrigger("AA");
           
        }
    }
}
