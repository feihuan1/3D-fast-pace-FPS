using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] GameObject explodeVFXPrefab;
    [SerializeField] int startingHealth = 3;

    int currentHealth;

    GameManager gameManager;

    private void Awake() 
    {
        currentHealth = startingHealth;
    }

    private void Start() 
    {
        gameManager = FindFirstObjectByType<GameManager>();
        gameManager.AdjestEnemiesLeft(1);
    }

    public void TakeDamage(int amount) 
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            gameManager.AdjestEnemiesLeft(-1);
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        Instantiate(explodeVFXPrefab,transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }

}
