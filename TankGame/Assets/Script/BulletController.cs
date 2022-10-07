using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float bulletTtl = 10;
    [SerializeField]
    GameObject explosion;
    [SerializeField]
    Transform bullet;
    void Update()
    {
        bulletTtl -= Time.deltaTime;
        if (bulletTtl <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Kill"))
        {
            Instantiate(explosion, bullet.position, bullet.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(explosion, bullet.position, bullet.rotation);
            Destroy(gameObject);
        }
    }
}
