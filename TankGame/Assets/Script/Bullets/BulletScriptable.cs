using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Bullet", menuName = "Bullet")]
[System.Serializable]
public class BulletScriptable : ScriptableObject
{
    public GameObject bulletPrefab;
    public float damage = 10;
    public float speed = 1;
    public float health = 0;
    public int amountOfBullets = 1;

    [Header("Explosion")]
    public GameObject explosion;
    public Gradient explosionColor1;
    public Gradient explosionColor2;
    public float explosionSize = 1;
}
