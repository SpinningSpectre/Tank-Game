using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSpawned : MonoBehaviour
{
    [SerializeField]
    Transform moveToP1;
    [SerializeField]
    Transform moveToP2;
    [SerializeField]
    int lazernumber = 0;
    [SerializeField]
    float lazerTtl = 5;
    bool lazerDamage = false;
    // Start is called before the first frame update
    void Start()
    {
        moveToP1 = GameObject.Find("Player1").GetComponent<Transform>();
        moveToP2 = GameObject.Find("Player2").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lazernumber == 1)
        {
            Vector2 toPlayer1 = moveToP1.position - transform.position;
            transform.Translate(new Vector2(toPlayer1.normalized.x * Time.deltaTime * 4, 0f));
        }
        if (lazernumber == 2)
        {
            Vector2 toPlayer2 = moveToP2.position - transform.position;
            transform.Translate(new Vector2(toPlayer2.normalized.x * Time.deltaTime * 4, 0f));
        }
        lazerTtl -= Time.deltaTime;
        if (lazerTtl <= 0)
        {
            GameObject.Find("Boss").GetComponent<Boss>().InActiveBossAttack();
            Destroy(gameObject);
        }

    }
}
