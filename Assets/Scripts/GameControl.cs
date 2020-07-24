using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    [SerializeField] private GameObject playerOne, playerTwo, canvas;
    private Player playerOneControl, playerTwoControl;
    private int activeTurn = 1;
    void Start()
    {
        playerOneControl = playerOne.GetComponent<Player>();
        playerTwoControl = playerTwo.GetComponent<Player>();
        playerOneControl.SetTurn(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeTurn()
    {

        if (activeTurn == 1)
        {
            activeTurn = 2;
            playerTwoControl.GetComponent<Player>().SetTurn(true);
            playerOneControl.SetCollision(true);
        }
        else
        {
            activeTurn = 1;
            playerOneControl.GetComponent<Player>().SetTurn(true);
            playerTwoControl.SetCollision(true);
        }
    }

    public void PlayerLost()
    {
        Text winnerText = canvas.GetComponentInChildren<Text>();

        switch (activeTurn)
        {
            case 1:
                winnerText.text = "Jogador 1 Venceu!";
                break;
            case 2:
                winnerText.text = "Jogador 2 Venceu";
                break;
        } 
        canvas.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
