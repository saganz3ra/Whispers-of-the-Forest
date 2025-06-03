using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    public float enemySpeed;
    public GameObject enemyBullet; // Prefab da bala do inimigo
    public Transform spawnPoint; // Local de spawn da bala

    private float bulletTime = 0;
    [SerializeField] private float timer = 5f;

    void Start()
    {
        bulletTime = timer;
    }

    void Update()
    {
        enemy.SetDestination(player.position);
        ShootAtPlayer();
    }

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        bulletTime = timer; // Reseta o tempo de disparo

        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.position, spawnPoint.rotation);
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(spawnPoint.forward * enemySpeed, ForceMode.Impulse);

        Destroy(bulletObj, 5f); // Destroi a bala após 5 segundos
    }
}
