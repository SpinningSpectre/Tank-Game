using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorActive : MonoBehaviour
{
    bool IsActive = false;
    [SerializeField]
    Transform moveToPart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive == true)
        {
            Vector2 toPlayer1 = moveToPart.position - transform.position;
            transform.Translate(new Vector2(0f ,toPlayer1.normalized.y * Time.deltaTime * 1));
        }
    }
    public void ActivateAtk2()
    {
        IsActive = true;
    }
}
