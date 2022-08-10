using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerControls : MonoBehaviour
{
    public UnityEvent onEnter;

    private void OnTriggerEnter(Collider other)
    {
        onEnter?.Invoke();
    }
}
