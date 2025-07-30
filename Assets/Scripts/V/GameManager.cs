using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player; // 임시 코드
    public GameObject boss;
    public BossController bossController;

    public GameObject gameOverText;
    public GameObject retryButton;
    private bool isGameOver = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        bossController.CreateBoss(0); // 임시 코드
    }


    

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        gameOverText.SetActive(true);
        retryButton.SetActive(true);
        
    }

    public void Retry()
    {
        Debug.Log("Retry called");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
