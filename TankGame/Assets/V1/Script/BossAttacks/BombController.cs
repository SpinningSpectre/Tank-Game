using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombController : MonoBehaviour
{
    [SerializeField]
    float bulletTtl = 10;
    [SerializeField]
    GameObject explosion;
    [SerializeField]
    Transform bomb;
    void Start()
    {
    }
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
            Instantiate(explosion, bomb.position, bomb.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Lazer"))
        {
            Instantiate(explosion, bomb.position, bomb.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(explosion, bomb.position, bomb.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(explosion, bomb.position, bomb.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("SnipeBullet"))
        {
            Instantiate(explosion, bomb.position, bomb.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("PoisonBullet"))
        {
            Instantiate(explosion, bomb.position, bomb.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("CloseBullet"))
        {
            Instantiate(explosion, bomb.position, bomb.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("SpreadBullet"))
        {
            Instantiate(explosion, bomb.position, bomb.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("DamageBullet"))
        {
            Instantiate(explosion, bomb.position, bomb.rotation);
            Destroy(gameObject);
        }
    }
}
