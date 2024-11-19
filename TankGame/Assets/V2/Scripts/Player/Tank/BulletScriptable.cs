using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Bullet", menuName = "Bullet")]
[System.Serializable]
public class BulletScriptable : ScriptableObject
{
    public GameObject bulletPrefab;

    [Header("Main stats")]
    public float directDamage = 10;
    public float splashDamage = 5;
    public float speed = 1;
    public float deathTime = 5;

    [Header("Special stats")]
    public float selfHealing = 0;
    public int amountOfBullets = 1;
    public float airTimeDamageIncrease = 0;

    [Header("Explosion")]
    public GameObject explosion;
    public Gradient explosionColor1;
    public Gradient explosionColor2;
    public float explosionSize = 1;
    public bool doKnockback = true;
    public List<BulletScriptable> doesntKnockback;

    [Header("Bullets on death")]
    public int amountOfDeathBullets;
    public Vector3 deathBulletLaunchDirection;
    public BulletScriptable deathBulletStats;
}
