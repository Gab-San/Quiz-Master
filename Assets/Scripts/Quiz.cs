using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Section: Question")]
    /*The next variable is needed to set the question at runtime*/
    [SerializeField] TextMeshProUGUI questionText;

    /*With this variable the script can acces the Question Scriptable Object*/
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;
    



    [Header("Section: Answers")]
    /*The next variable is needed to set the answers at runtime*/
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

    [Header("Section: Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Section: Slider")]
    [SerializeField] Slider progressBar;
    public bool quizIsCompleted;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    private void AllButtonsSetDisactive()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].SetActive(false);
        }
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
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";

        if(progressBar.value == progressBar.maxValue){
            quizIsCompleted = true;
        }

    }

    void DisplayAnswer(int index){
        Image buttonImage;

        if( index == currentQuestion.GetCorrectAnswerIndex() ){
            questionText.text = "Corretto!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        } else {
            correctAnswer_index = currentQuestion.GetCorrectAnswerIndex();
            questionText.text = "Sbagliato! La risposta corretta è:\n\"" + currentQuestion.GetAnswer(correctAnswer_index) + "\"";
            buttonImage = answerButtons[correctAnswer_index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    /*Sets Up Question and Answers in GUI*/
    void DisplayQuestion(){

        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < currentQuestion.GetNumberofAnswers(); i++){

            answerButtons[i].SetActive(true);
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }

    }

    /*This method is untested*/
    void GetNextQuestion(){
        if (questions.Count > 0){
            SetButtonState(true);
            SetDefaultButtonSprite();
            AllButtonsSetDisactive(); 
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
    }

    void GetRandomQuestion(){
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        /*Checking that the currentQuestion exists*/
        if (questions.Contains(currentQuestion)){
            questions.Remove(currentQuestion);
        }
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
