using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject[] elements;

    private void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        PlayerMovement.instance.enabled = true;
        elements[0].SetActive(false);
        PlayerMovement.instance.GetComponentInChildren<Animator>().enabled = true;
    }
}
