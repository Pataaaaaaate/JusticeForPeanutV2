using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonManager : MonoBehaviour
{
    public PanierManager panierManager;

    //quand le ballon rencontre la zone de collision cela d�clenche la fonction "OnDunk"//
    private void OnTriggerEnter(Collider collision)
    {
        
        //permet de d�tecter seulement la zone de collision qui a le tag "panier"//
        if (collision.gameObject.tag == "Panier")
        {
            panierManager.OnDunk();
        }
        
    }
}
