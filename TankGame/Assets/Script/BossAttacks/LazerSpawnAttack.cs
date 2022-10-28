using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSpawnAttack : MonoBehaviour
{
    bool attackActive = false;
    [SerializeField]
    int spawnedLazers = 0;
    [SerializeField]
    GameObject lazer;
    [SerializeField]
    GameObject lazer2;
    [SerializeField]
    Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackActive == true)
        {
            if (spawnedLazers == 0)
            {
                spawnedLazers++;
                GameObject Lazer = Instantiate(lazer, spawnPoint.position, spawnPoint.rotation);
            }
            if (spawnedLazers == 1)
            {
                spawnedLazers++;
                GameObject Lazer2 = Instantiate(lazer2, spawnPoint.position, spawnPoint.rotation);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("AttackActive");
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                InActivateAttack();
            }
        }
    }
    public void ActivateAttack()
    {
        attackActive = true;
    }
    public void ResetLazers()
    {
        spawnedLazers = 0;
    }
    public void InActivateAttack()
    {
        GameObject.Find("Boss").GetComponent<Boss>().InActiveAttackIs1();
        attackActive =false;
        Debug.Log("Attack Turned Off");
    }
}
