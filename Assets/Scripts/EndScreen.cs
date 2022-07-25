using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    [SerializeField] TextMeshProUGUI scoreText;

    void Awake(){
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore(){
        scoreText.text = "Congratulations!\nYou Score: " + scoreKeeper.calculateScore() + "%";
    }
}
