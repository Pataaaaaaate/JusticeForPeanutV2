using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Cette fonction sera appel�e lorsque le joueur appuie sur le bouton "D�marrer"
    public void StartGame()
    {
        // Charge la sc�ne du jeu (assurez-vous d'avoir ajout� la sc�ne du jeu � la build)
        SceneManager.LoadScene("BasketScene");  // Remplacez "GameScene" par le nom de votre sc�ne de jeu
    }

    // Cette fonction sera appel�e pour quitter le jeu
    public void QuitGame()
    {
        Debug.Log("Quitter le jeu");
        Application.Quit();
    }
}