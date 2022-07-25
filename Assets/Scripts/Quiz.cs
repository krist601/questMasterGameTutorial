using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO question;

    [Header("Answers")]
    [SerializeField] GameObject[] answersButtons;

    [Header("ButtonColors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    Timer timer;
    bool hasAnswerEarly = true;
    [SerializeField] Image timerImage;
    
    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake(){
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();
    }
    void Update(){
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion){
            if(questions.Count == 0){
                return;
            }
            hasAnswerEarly = false;
            timer.loadNextQuestion = false;
        }else if(!hasAnswerEarly && !timer.isAnsweringQuestion){
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }
    void GetRandomQuestion(){
        int index = Random.Range(0, questions.Count);
        question = questions[index];
        if(questions.Contains(question))
            questions.Remove(question); 
    }
    void DisplayAnswer(int index){
        if(index == question.GetCorrectAnswerIndex()){
            scoreKeeper.IncrementCorrectAnswers();
            questionText.text = "Correcto Ingeniero!";
            Image buttonImage = answersButtons[index].GetComponentInChildren<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }else{
            questionText.text = "pero por diooooooos! la respuesta era: " + question.GetAnswer(question.GetCorrectAnswerIndex());
            Image buttonImage = answersButtons[question.GetCorrectAnswerIndex()].GetComponentInChildren<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        timer.isAnsweringQuestion = true;
    }

    public void OnAnswerSelected(int index){
        hasAnswerEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.calculateScore() + "%";
    }
    void GetNextQuestion(){
        if(questions.Count > 0){
            scoreKeeper.IncrementQuestionsSeen();
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
        }
    }
    void DisplayQuestion(){
        questionText.text = question.GetQuestion();
        for(int i = 0; i < answersButtons.Length; i++){
            TextMeshProUGUI buttonText = answersButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }
    void SetButtonState(bool state){
        for(int i = 0; i < answersButtons.Length; i++){
            Button button = answersButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
    void SetDefaultButtonSprites(){
        for(int i = 0; i < answersButtons.Length; i++){
            Image buttonImage = answersButtons[i].GetComponentInChildren<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
    public bool isComplete(){
        if (questions.Count == 0) return true;
        return false;
    }
}
