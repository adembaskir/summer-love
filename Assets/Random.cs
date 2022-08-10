using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random : MonoBehaviour
{
	Transform scaleHolder;
	Animator scaleAnimator;
	void Start()
	{
		scaleHolder=transform.parent;
		scaleAnimator=GetComponent <Animator>();
		
	
	}
	public void RandomizeScale()
	{

	float scale=UnityEngine.Random.Range(0.1f,1f);
	scaleHolder.localScale=new Vector3(scale,scale,scale);	
	float speed = UnityEngine.Random.Range (0,1);	

	scaleAnimator.speed=speed;
	}
        
    }

