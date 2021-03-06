using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Basic Asset for Question Scriptable Object*/

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{

    /*The variables set are:
        -The Question
        -The Possible Answers
        -And the index of the correct answer*/
    [TextArea(2,6)][SerializeField] string question = "Enter new question text here";

    [TextArea(1,2)][SerializeField] string[] answers = new string[4];

    [SerializeField] int correctAnswerIndex;

    [TextArea(2,4)][SerializeField] string correctAnswerText;

    public string GetQuestion(){
        return question;
    }

    public string GetAnswer(int index){
        return answers[index];
    }

    public string GetCorrectAnswer(){
        return correctAnswerText;
    }
    
    public int GetCorrectAnswerIndex(){
        return correctAnswerIndex;
    }

    public int GetNumberofAnswers(){
        Debug.Log(answers.Length);
        return answers.Length;
    }
}
