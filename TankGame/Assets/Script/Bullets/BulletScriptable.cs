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
    public GameObject explosion;
    public Color explosionColor;
}
