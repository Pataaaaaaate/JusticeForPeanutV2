using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

public class ScoreSystem : MonoBehaviour
{
    public TMP_Text scoreText;

    private int score = 0;

    //Augmente le score
    public void AugmenteScore()
    {
        void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == "Ring")
            {

                score += 1;
                scoreText.SetText(score.ToString());
                //Modifie le texte du canva
            }
            else
            {
                score += 3;
                scoreText.SetText(score.ToString());
                //Modifie le texte du canva
            }
        }

    

    score += 1;
        scoreText.SetText(score.ToString());
        //Modifie le texte du canva
    }
}