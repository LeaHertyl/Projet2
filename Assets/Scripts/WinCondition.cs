using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //on recupere le composant PlayerBehaviour du GameObject ayant le tag "Player1"
        var Player1 = GameObject.FindWithTag("Player1");
        var Player1Script = Player1.GetComponent<PlayerBehaviour>();

        //on associe la valeur de la variable currentHealth1 du script PlayerBehaviour a la variable Player1currentHealth
        var Player1currentHealth = Player1Script.currentHealth1;

        //on recupere le composant Player2Behaviour du GameObject ayant le tag "Player2"
        var Player2 = GameObject.FindWithTag("Player2");
        var Player2Script = Player2.GetComponent<Player2Behaviour>();
        
        //on associe la valeur de la variable currentHealth2 du script Player2Behaviour a la variable Player2currentHealth
        var Player2currentHealth = Player2Script.currentHealth2;

        //si la vie du Player 1 tombe a 0 
        if(Player1currentHealth == 0)
        {
            SceneManager.LoadScene("EndSceneP1"); //on lance la scene indiquant que le Player2 a gagne
        }
        //si la vie du Player 2 tombe a 0
        else if(Player2currentHealth == 0)
        {
            SceneManager.LoadScene("EndSceneP2"); //on lance la scene indiquant que le Player1 a gagne
        }
    }
}
