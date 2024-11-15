using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTankController : MonoBehaviour
{
    public BulletScriptable[] selectedBullets = new BulletScriptable[4];
    [SerializeField] private Transform firePoint;

    [Header("Health")]
    public float currentHealth = 100;
    [SerializeField] private float startHealth = 100;
    [SerializeField] private float maxHealth = 150;

    [Header("Turns")]
    [SerializeField] private bool currentTurn = false;
    [SerializeField] private bool canShoot = true;

    public int numb = 0;
    private void Start()
    {
        currentHealth = startHealth;
        if(firePoint == null)
        {
            firePoint = transform.Find("Firepoint");
        }
        for(int i = 0; i < 10; i++)
        {
            print(firePoint.rotation.eulerAngles.z + (i * 5));

        }
    }
    bool autoFire = false;
    bool no = true;
    private void Update()
    {
#if UNITY_EDITOR
        no = !no;
        if (Input.GetKeyDown(KeyCode.C))
        {
            FireBullet();
        }
        if (Input.GetKey(KeyCode.V))
        {
            FireBullet();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            autoFire = !autoFire;
        }
        if(autoFire)
        {
            if (!no)
            {
                FireBullet();
            }
        }
        if (Input.GetKey(KeyCode.B))
        {
            FireBullet(numb);
            numb++;
            if(numb > 3)
            {
                numb = 0;
            }
        }
#endif
    }

    public void FireBullet(int type = 0)
    {
        BulletScriptable scrip = selectedBullets[type];
        GameObject bullet = Instantiate(
            selectedBullets[type].bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        NewBulletController controller = bullet.GetComponent<NewBulletController>();
        controller.stats = selectedBullets[type];
        Rigidbody2D rigidB = bullet.GetComponent<Rigidbody2D>();
        rigidB.AddForce(firePoint.up * scrip.speed, ForceMode2D.Impulse);
        bullet.transform.Find("ParticleTrail").gameObject.SetActive(true);
        DoDamage(-scrip.health);
    }

    public void DoDamage(float amount)
    {
        currentHealth -= amount;
        //print(currentHealth);
        if (currentHealth < 0)
        {
            print("You ded");
        }else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
