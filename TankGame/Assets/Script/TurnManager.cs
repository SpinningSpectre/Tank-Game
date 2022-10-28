using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private int playerTurn = 1;
    public GameObject player1;
    public GameObject player2;
    void Start()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject g in player)
        {
            if (g.GetComponent<TankController>().playerNumber == 1)
            {
                player1 = g;
            }
            else if (g.GetComponent<TankController>().playerNumber == 2)
            {
                player2 = g;
            }
        }
        Invoke("Init", 0.1f);
    }
    void Init()
    {
        if (playerTurn == 1)
        {
            player1.GetComponent<TankController>().IsActive(true);
            player2.GetComponent<TankController>().IsActive(false);
        }
        else if (playerTurn == 2)
        {
            player1.GetComponent<TankController>().IsActive(true);
            player2.GetComponent<TankController>().IsActive(false);
        }
    }
    public void ChangeTurn()
    {
        if (playerTurn == 1)
        {
            playerTurn = 2;
            player1.GetComponent<TankController>().IsActive(false);
            player2.GetComponent<TankController>().IsActive(true);
        }
        else if (playerTurn == 2)
        {
            playerTurn = 1;
            player1.GetComponent<TankController>().IsActive(true);
            player2.GetComponent<TankController>().IsActive(false);
        }
    }
}

