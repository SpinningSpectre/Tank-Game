using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    Transform upgrades;
    [SerializeField]
    Transform teleporter;
    [SerializeField]
    Transform teleporterSlider;
    [SerializeField]
    Transform schootSliderSpeed;
    [SerializeField]
    Transform slider;
    void Start()
    {
        upgrades = GameObject.Find("Upgrades").transform;
        schootSliderSpeed = GameObject.Find("Slider").transform;
        teleporter = GameObject.Find("TPActive").transform;
        teleporterSlider = GameObject.Find("TPSlider").transform;
        slider = GameObject.Find("FirespeedSlider").transform;
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
            bringBackButton();
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(explosion, bullet.position, bullet.rotation);
            bringBackButton();
            Destroy(gameObject);
        }
    }
    public void bringBackButton()
    {
        upgrades.transform.position = new Vector2(teleporter.position.x , teleporter.position.y);
        schootSliderSpeed.transform.position = new Vector2(teleporterSlider.position.x, teleporterSlider.position.y);
        slider.transform.position = new Vector2(teleporterSlider.position.x, teleporterSlider.position.y);
    }
}
