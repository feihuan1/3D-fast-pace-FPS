using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] LayerMask interactionLayer;

    CinemachineImpulseSource cinemachineImpulseSource;

    private void Awake() {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shoot(WeaponSO weaponSO)
    {

        muzzleFlash.Play();
        cinemachineImpulseSource.GenerateImpulse();

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayer, QueryTriggerInteraction.Ignore))
        {
            Instantiate(weaponSO.HitVFXPrefab,hit.point,Quaternion.identity);
            // get comp in parent start check with it self , and only retur nthe first find :)
            EnemyHealth enemyHealth = hit.collider.gameObject.GetComponentInParent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);
        }
    }
}
