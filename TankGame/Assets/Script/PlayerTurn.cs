using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    public int playerNumber;
    [SerializeField]
    Material activeMat;
    [SerializeField]
    Material inactiveMat;
    bool ActiveTurn = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().material = inactiveMat;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1) && ActiveTurn == true)
        {
            Invoke("ChangeTurn", 0.1f);
        }
    }
    void ChangeTurn()
    {
        GameObject.Find("Player1").GetComponent<TurnManager>().ChangeTurn();
    }
    public void SetActive(bool b)
    {
        if (b == true)
        {
            ActiveTurn = true;
            GetComponent<SpriteRenderer>().material = activeMat;
        }
        else
        {
            ActiveTurn = false;
            GetComponent<SpriteRenderer>().material = inactiveMat;
        }
    }
}
