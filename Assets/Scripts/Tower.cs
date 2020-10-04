using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [HideInInspector]
    public BoxCollider Collider;
    [HideInInspector]
    public LODGroup TowerMeshes;

    protected string etat;


    public void Awake()
    {
        Collider = GetComponent<BoxCollider>();
        TowerMeshes = GetComponent<LODGroup>();
    }

    public void SetMaterial(Material mat)
    {
        var lods = TowerMeshes.GetLODs();
        for (var i = 0; i < lods.Length; i++)
        {
            for (var j = 0; j < lods[i].renderers.Length; j++)
            {
                lods[i].renderers[j].material = mat;
            }
        }
    }

    public void SetEtat(string etatTower)
    {
       this.etat = etatTower;
    }

    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]

    public float range = 15f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";


    public Transform firePoint;

    // Use this for initialization
    void Start()
    {
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        if (etat == "built")
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;
                targetEnemy = nearestEnemy.GetComponent<Enemy>();
            }
            else
            {
                target = null;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
         }

         fireCountdown -= Time.deltaTime;
        

    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Arrow bullet = bulletGO.GetComponent<Arrow>();

        if (bullet != null)
            bullet.Seek(target);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}