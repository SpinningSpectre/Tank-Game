using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBossDirection : MonoBehaviour
{
    [SerializeField]
    int triggerNumber;
    GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Boss").GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (triggerNumber == 1)
        {
            GameObject.Find("Boss").GetComponent<Boss>().ToLeft();
        }
        if (triggerNumber == 2)
        {
            GameObject.Find("Boss").GetComponent<Boss>().ToRight();
        }
        if (triggerNumber == 3)
        {
            GameObject.Find("Boss").GetComponent<Boss>().ToLeft();
        }
    }
}
