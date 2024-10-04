using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBulletController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(KillBulletOverTime());
    }
    void Update()
    {
        
    }

    private IEnumerator KillBulletOverTime()
    {
        yield return new WaitForSeconds(10);
        Explode();
    }

    private void Explode()
    {
        Destroy(gameObject);
    }
}
