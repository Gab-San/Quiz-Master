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

    public string GetQuestion(){
        return question;
    }

    public string GetAnswer(int index){
        return answers[index];
    }
    
    public int GetCorrectAnswerIndex(){
        return correctAnswerIndex;
    }

    public int GetNumberofAnswers(){
        return answers.Length;
    }
}
