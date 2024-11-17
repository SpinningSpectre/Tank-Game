using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextScene : MonoBehaviour
{
    public float untilNextScene = 5;
    GameObject boss;
    GameObject player1;
    GameObject player2;
    bool bossDead = false;
    bool p1Dead = false;
    bool p2Dead = false;
    bool playersLose = false;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss");
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
    }

    // Update is called once per frame
    void Update()
    {
        if (bossDead == true)
        {
            untilNextScene -= Time.deltaTime;
            if (untilNextScene <= 0)
            {
                SceneManager.LoadScene(5);
            }
        }
        if (p1Dead == true)
        {
            untilNextScene -= Time.deltaTime;
            if (untilNextScene <= 0)
            {
                SceneManager.LoadScene(6);
            }
        }
        if (p2Dead == true)
        {
            untilNextScene -= Time.deltaTime;
            if (untilNextScene <= 0)
            {
                SceneManager.LoadScene(7);
            }
        }
        if (playersLose == true)
        {
            untilNextScene -= Time.deltaTime;
            if (untilNextScene <= 0)
            {
                SceneManager.LoadScene(4);
            }
        }
    }
    public void BossDead()
    {
        bossDead = true;
    }
    public void P1Dead()
    {
        p1Dead = true;
    }
    public void P2Dead()
    {
        p2Dead = true;
    }
    public void PlayerLose()
    {
        playersLose = true;
    }
}
