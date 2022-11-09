using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalDespawn : MonoBehaviour
{
    [SerializeField]
    float particalTtl = 1;
    void Update()
    {
        particalTtl -= Time.deltaTime;
        if (particalTtl <= 0)
        {
            Destroy(gameObject);
        }
    }
}