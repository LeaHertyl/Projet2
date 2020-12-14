using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    //[SerializeField] Scene EndScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var Player1 = GameObject.FindWithTag("Player1");
        var Player1Script = Player1.GetComponent<PlayerBehaviour>();

        var Player1currentHealth = Player1Script.currentHealth1;

        var Player2 = GameObject.FindWithTag("Player2");
        var Player2Script = Player2.GetComponent<Player2Behaviour>();

        var Player2currentHealth = Player2Script.currentHealth2;

        if(Player1currentHealth == 0 || Player2currentHealth == 0)
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}
