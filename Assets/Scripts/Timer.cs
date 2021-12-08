using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    /*Timer variables*/
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;
    
    public bool loadNextQuestion;
    public float fillFraction;

    bool isAnsweringQuestion;
    float timerValue;


    void Update()
    {
        
    }
}
