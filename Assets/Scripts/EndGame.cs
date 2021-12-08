using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI endgameText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore(){
        endgameText.text = "Congratulazioni!\nIl tuo punteggio è di " + 
                                        scoreKeeper.CalculateScore() + "%";
    }

}
