using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;
    
    public int GetCorrectAnswers(){
        return correctAnswers;
    }
    public void IncrementCorrectAnswers() {
        Debug.Log("sumo buenas");
        correctAnswers ++;
    }
    public int GetQuestionsSeen(){
        return questionsSeen;
    }
    public void IncrementQuestionsSeen() {
        Debug.Log("sumo vistas");
        questionsSeen ++;
    }
    public int calculateScore(){
        Debug.Log(correctAnswers + ": " + questionsSeen);
        return Mathf.RoundToInt(correctAnswers/questionsSeen*100);
    }
}
