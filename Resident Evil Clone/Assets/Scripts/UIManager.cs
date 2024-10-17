using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthText;
    private int globalScore = 0;
    public static UIManager instance;

    // public delegate void OnZombieDie(int score);
    // public static OnZombieDie zombieDeath;


    // public Action<int> zombieDeath;
    void Start()
    {
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }

        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        healthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();

        // zombieDeath += TestFunction;
        // zombieDeath += TestFunction1;
        // zombieDeath += TestFunction2;

        // zombieDeath += UpdateScore;

        // if(zombieDeath != null){
        //     zombieDeath.Invoke(10);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void TestFunction(){
    //     Debug.Log("Test");
    // }

    // public void TestFunction1(){
    //     Debug.Log("Test1");
    // }

    // public void TestFunction2(){
    //     Debug.Log("Test2");
    // }

    public void UpdateScore(int score){
        globalScore += score;
        scoreText.text = "Score: " + globalScore;
        Debug.Log("Score was updated to: " + globalScore);
    }
}
