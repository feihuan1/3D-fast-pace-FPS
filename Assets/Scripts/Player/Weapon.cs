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
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayer, QueryTriggerInteraction.Ignore))
        {
            cinemachineImpulseSource.GenerateImpulse();
            Instantiate(weaponSO.HitVFXPrefab,hit.point,Quaternion.identity);
            EnemyHealth enemyHealth = hit.collider.gameObject.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);
        }
    }
}
