using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject[] elements;
    public float score;
    int highScore;
    public Text scoreText;
    public Text highScoreText;
   

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        scoreText.text = "" + score;
        highScore = (int)score;
        scoreText.text = highScore.ToString();

        if (PlayerPrefs.GetInt("Score") <= highScore)
            PlayerPrefs.SetInt("score", highScore);
        
    }
    public void HighScore()
    {
        highScoreText.text = PlayerPrefs.GetInt("score").ToString();
        PlayerPrefs.Save();
        StartCoroutine(ScoreToText());
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void StartGame()
    {
        PlayerMovement.instance.enabled = true;
        elements[0].SetActive(false);
        PlayerMovement.instance.GetComponentInChildren<Animator>().enabled = true;
        PlayerMovement.instance.GetComponentInChildren<Animator>().SetBool("Run",true);
    }

    public IEnumerator ScoreToText()
    {
        yield return new WaitForSeconds(3f);
        elements[2].SetActive(true);
    }
}
