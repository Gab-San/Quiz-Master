using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{

    /*With this variable the script can acces the Question Scriptable Object*/
    QuestionSO question;

    /*The next two variables are needed to set the question and the answer in the Start()*/
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject[] answerButtons;

    int correctAnswer_index;

    /*These two variables control which sprite to show with buttons interaction*/

    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;



    void Start()
    {
        GetNextQuestion();
        //DisplayQuestion();    
    }

    public void OnAnswerSelected(int index){

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

        SetButtonState(false);
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
