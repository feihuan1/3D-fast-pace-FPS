using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    [SerializeField] int damage = 3;

    const string PLAYER_STRING = "Player";

    private void Start() {
        Explode();
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Explode(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var hitCollider in hitColliders)
        {
            PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
            if(!playerHealth) continue;
            playerHealth.TakeDamage(damage);

            break;
            // playerHealth?.TakeDamage(damage);

            // if(playerHealth){
            //     playerHealth.TakeDamage(damage)
            // }
        }
    }
}
