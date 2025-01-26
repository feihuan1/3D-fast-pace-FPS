using NUnit.Framework.Internal;
using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] int damageAmount = 1;

    StarterAssetsInputs starterAssetsInputs;

    private void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        handleShoot();
        starterAssetsInputs.ShootInput(false);
    }

    private void handleShoot()
    {
        if (!starterAssetsInputs.shoot) return;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            EnemyHealth enemyHealth = hit.collider.gameObject.GetComponent<EnemyHealth>();

            enemyHealth?.TakeDamage(damageAmount);

        }
    }
}
