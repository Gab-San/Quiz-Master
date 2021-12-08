using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Section: Question")]
    /*With this variable the script can acces the Question Scriptable Object*/
    QuestionSO question;

    /*The next two variables are needed to set the question and the answer in the Start()*/
    [SerializeField] TextMeshProUGUI questionText;

    [Header("Section: Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswer_index;


    /*These two variables control which sprite to show with buttons interaction*/
    [Header("Section: Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;


    /*Accessing Timer Components*/
    [Header("Section: Timer")]
    [SerializeField] Image timerImage;
    Timer timer;
    [SerializeField] bool hasAnswered_inTime;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();
        //DisplayQuestion();    
    }

    void Update(){
        ChangeFillAmount();
        /*If timeToShowAnswer has finished*/
        if(timer.loadNextQuestion){
            hasAnswered_inTime = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        } 
        /*If Player didn't answer in time*/
        else if( !hasAnswered_inTime && !timer.GetAnsweringQuestionBool() ){
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    /*Changes Graphical FillAmount of the Timer Image*/
    void ChangeFillAmount(){
        timerImage.fillAmount = timer.fillFraction;
    }

    public void OnAnswerSelected(int index){

        hasAnswered_inTime = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
    }

    void DisplayAnswer(int index){
        Image buttonImage;

        if( index == question.GetCorrectAnswerIndex() ){
            questionText.text = "Corretto!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        } else {
            correctAnswer_index = question.GetCorrectAnswerIndex();
            questionText.text = "Sbagliato! La risposta corretta è:\n\"" + question.GetAnswer(correctAnswer_index) + "\"";
            buttonImage = answerButtons[correctAnswer_index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    /*Sets Up Question and Answers in GUI*/
    void DisplayQuestion(){

        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++){

            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }

    }

    /*This method is untested*/
    void GetNextQuestion(){
        SetButtonState(true);
        SetDefaultButtonSprite();
        DisplayQuestion();
    }

    /*Sets Button State*/
    void SetButtonState(bool state){

        Button button;
        for(int i = 0; i < answerButtons.Length; i++){
            button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprite(){
        Image buttonImage;
        for(int i = 0; i < answerButtons.Length; i++){
            buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }


}
