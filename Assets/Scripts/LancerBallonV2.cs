using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LancerBallonV2 : MonoBehaviour
{
    //Positions du ballon
    public Transform startPos;      //Position du ballon dans les mains 
    private Transform ballPos;      //Position actuelle du ballon

    Rigidbody rb;

    //Etat et stats pour le chargement du ballon pour le tir 
    public bool isCharging; 
    public float chargingTime;      //Temps de chargement qui sera pris en compte dans la puissance de lancer
    public float chargingMax;       //Puissance max du chargement que le ballon peut être lancé
    public float chargingSpeed;     //Multiplicateur pour la vitesse du chargement

    public bool canThrow;       //boolean qui empeche le joueur de tirer quand il ne tient pas le ballon
    public bool isThrowing;     //boolean quand le joueur tire
    public float chargingPower;

    public Camera cam;      //reference à la cam du joueur

    public Image chargeBarre;  // L'UI Image qui représente la jauge


    // Start is called before the first frame update
    void Start()
    {
        //On recupere les reference des components du ballon
        rb = GetComponent<Rigidbody>();
        ballPos = GetComponent<Transform>();
        ResetBall();

    }


    public void ResetBall ()
    {
        //Au debut du jeu et quand le ballon retourne dans les mains du joueur

        ballPos.position = startPos.position;       //teleporte le ballon dans les mains du joueur

        //vu qu'on teleporte le ballon, on s'occupe de desactiver la physique du rigidbody
        rb.velocity = Vector3.zero; 
        rb.useGravity = false;
        rb.isKinematic = true;

        canThrow = true;        //Après avoir récupéré le ballon, le joueur peut tirer à nouveau
        chargingTime = 0;       //Reset le chargement du tir du joueur

        // Réinitialise la jauge à 0
        if (chargeBarre != null)
            chargeBarre.fillAmount = 0f;
    }


    // Update is called once per frame
    void Update()
    {
        if (canThrow == true)
        {
            ballPos.position = startPos.position;       //tant que le joueur n'a pas tiré, le ballon reste dans ses mains

            if (isCharging == true)         //dès que le joueur commence à charger le tir
            {
                chargingTime += Time.deltaTime * chargingSpeed;         //la puissance du tir augmente avec le temps 

                if (chargeBarre != null)
                    chargeBarre.fillAmount = chargingTime / chargingMax;
            }

            if (isThrowing == true)     //dès que le joueur arrête de charger le tir
            {
                ThrowBall();
            }
        }

    }


    public void OnThrowInput(InputAction.CallbackContext context)
    {
        if (context.started)        //quand le joueur maintient le clique gauche
        {
            isCharging = true;      //démarre le chargement du tir
        }

        if (context.canceled)       //quand le joueur relâche le clique gauche
        {
            isThrowing = true;      //change l'etat pour dire que le joueur à l'intention de tirer
            isCharging = false;     //arrête le chargement du tir
        }
    }


    public void ThrowBall()
    {
        //Avant d'appliquer une force au ballon, on réactive la physique du rigidbody
        rb.isKinematic = false;
        rb.useGravity = true;

        //Si le joueur charge trop longtemps le ballon, fixe la puissance sur la puissance maximale établie
        if (chargingTime > chargingMax)     
        {
            chargingTime = chargingMax;
        }

        //Quand le joueur tire, il ne plus tirer à nouveau (pas avant de récupérer le ballon dans ses mains)
        if (isThrowing == true)
        {
            canThrow = false; 
            isThrowing = false;
        }

        //Application de la force sur le ballon
        Vector3 throwDir = cam.transform.forward;       //on prend en compte la direction forward/avant de la caméra
        rb.AddForce(throwDir.normalized * chargingTime, ForceMode.Impulse);      //on applique une force de type "Impulse"(=explosion) dans la direction forward de la caméra, multiplié par le temps de chargement du tir
    }


    private void OnTriggerEnter(Collider collision)
    {
        //Fonction qui permet de redonner le ballon dans les mains du joueur (reset) quand il rentre en contact avec le sol (collision qui a un layer)
        if (collision.gameObject.layer == 3)
        {
            if (canThrow == false)
            {
                ResetBall();
            }
            
        }
    }

    }
