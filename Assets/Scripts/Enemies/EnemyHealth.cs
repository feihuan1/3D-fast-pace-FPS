using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] GameObject explodeVFXPrefab;
    [SerializeField] int startingHealth = 3;

    int currentHealth;

    private void Awake() {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount) 
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        Instantiate(explodeVFXPrefab,transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }

}
