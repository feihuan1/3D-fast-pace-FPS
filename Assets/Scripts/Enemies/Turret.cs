using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform turretHead;
    [SerializeField] Transform playerTargetPoint;

    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float spawnTime = 5f;
    [SerializeField] float rotationSpeed = 90f;
    [SerializeField] int damage = 2;

    PlayerHealth player;


    private void Start() 
    {
        player = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(FireCorutine());    
    }

    private void Update()
    {
        LookatPlayer();
    }

    private void LookatPlayer()
    {
        // turretHead.LookAt(playerTargetPoint); look to player in realtime loke a owl
        if(playerTargetPoint)
        {
            Vector3 direction = playerTargetPoint.position - turretHead.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            turretHead.rotation = Quaternion.RotateTowards(turretHead.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }

    IEnumerator FireCorutine()
    {
        while(player)
        {
            yield return new WaitForSeconds(spawnTime);
            Projectile newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.transform.LookAt(playerTargetPoint);
            newProjectile.Init(damage);
        }
    }
}
