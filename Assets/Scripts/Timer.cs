using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    Quiz quizScript;

    /*Timer variables*/
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;
    float timerValue;
    
    /*bools to procede with Timer or Question*/
    [SerializeField] bool isAnsweringQuestion;
    public bool loadNextQuestion;

    /*This variable is used to modify the timer sprite*/
    public float fillFraction;



    void Awake(){
        quizScript = FindObjectOfType<Quiz>();
    }


    void Update()
    {
        if(!quizScript.quizIsCompleted){
            UpdateTimer();
        }
    }

    public void CancelTimer(){
        timerValue = 0;
    }

    /*Logic behind timer*/
    void UpdateTimer(){

        timerValue -= Time.deltaTime;
//        Debug.Log(timerValue);

        if(isAnsweringQuestion){
            if(timerValue > 0){
                fillFraction = timerValue / timeToCompleteQuestion;
            } else {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }

        } else {
            if(timerValue > 0){
                fillFraction = timerValue / timeToShowCorrectAnswer;
            } else {
                loadNextQuestion = true;
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
            }
        }


    }

    public bool GetAnsweringQuestionBool(){
        return isAnsweringQuestion;
    }
}
