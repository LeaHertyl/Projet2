using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinCanvas : MonoBehaviour
{
    [SerializeField] GameObject Winner;
    [SerializeField] GameObject Loser;

    private string winner;
    private string loser;

    // Start is called before the first frame update
    void Start()
    {
        var winnerf = Winner.GetComponent<TextMeshProUGUI>();
        winner = winnerf.text;

        var loserf = Loser.GetComponent<TextMeshPro>();
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

        if (Player1currentHealth == 0)
        {
            winner = "Player 2";
        }
    }
}
