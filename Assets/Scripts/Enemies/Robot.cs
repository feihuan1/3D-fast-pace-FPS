using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    FirstPersonController player;
    NavMeshAgent agent;

    const string PLAYER_STRING = "Player";

    private void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!player) return;
        agent.SetDestination(player.transform.position);
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag(PLAYER_STRING))
        {
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
            enemyHealth.SelfDestruct();
        }
    }
}
