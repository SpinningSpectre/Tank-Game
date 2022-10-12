using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float bulletTtl = 10;
    [SerializeField]
    GameObject explosion;
    [SerializeField]
    GameObject explosion2;
    [SerializeField]
    GameObject explosion3;
    [SerializeField]
    Transform bullet;
    [SerializeField]
    Transform Explodepoint2;
    [SerializeField]
    Transform Explodepoint3;
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
            Instantiate(explosion2, Explodepoint2.position, bullet.rotation);
            Instantiate(explosion3, Explodepoint3.position, bullet.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(explosion, bullet.position, bullet.rotation);
            Instantiate(explosion2, Explodepoint2.position, bullet.rotation);
            Instantiate(explosion3, Explodepoint3.position, bullet.rotation);
            Destroy(gameObject);
        }
    }
}
