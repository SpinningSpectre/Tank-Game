using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
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
    [SerializeField]
    Transform upgrades;
    void Start()
    {
        upgrades = GameObject.Find("Upgrades").transform;
    }
    void Update()
    {
        bulletTtl -= Time.deltaTime;
        if (bulletTtl <= 0)
        {
            Destroy(gameObject);
            bringBackButton();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Kill"))
        {
            Instantiate(explosion, bullet.position, bullet.rotation);
            Instantiate(explosion2, Explodepoint2.position, bullet.rotation);
            Instantiate(explosion3, Explodepoint3.position, bullet.rotation);
            bringBackButton();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(explosion, bullet.position, bullet.rotation);
            Instantiate(explosion2, Explodepoint2.position, bullet.rotation);
            Instantiate(explosion3, Explodepoint3.position, bullet.rotation);
            bringBackButton();
            Destroy(gameObject);
        }
    }
    public void bringBackButton()
    {
        upgrades.transform.position = new Vector2(387, 357);
    }
}
