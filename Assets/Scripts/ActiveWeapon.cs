using UnityEngine;
using StarterAssets;
using Cinemachine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] GameObject ZoomVignette;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;

    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    Weapon currentWeapon;
    FirstPersonController firstPersonController;

    float timeSinceLastShot = 0;
    float defaultFOV;
    float defaultRotationSped;


    const string SHOOT_STRING = "Shoot";

    private void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();
        defaultFOV = cinemachineVirtualCamera.m_Lens.FieldOfView;
        defaultRotationSped = firstPersonController.RotationSpeed;
    }

    private void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
    }


    void Update()
    {

        handleShoot();
        HandleZoom();
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }

        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();// tramsform will defaul child this into current class and use preset transform
        currentWeapon = newWeapon;
        this.weaponSO = weaponSO;
    }

    private void handleShoot()
    {
        timeSinceLastShot += Time.deltaTime;

        if (!starterAssetsInputs.shoot) return;

        if (timeSinceLastShot >= weaponSO.FireRate)
        {
            currentWeapon.Shoot(weaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            timeSinceLastShot = 0;
        }
        if (!weaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }
    }

    void HandleZoom()
    {
        if (!weaponSO.CanZoom) return;

        if (starterAssetsInputs.zoom)
        {
            ZoomVignette.SetActive(true);
            cinemachineVirtualCamera.m_Lens.FieldOfView = weaponSO.ZoomAmount;
            firstPersonController.ChangeRotationSpeed(weaponSO.ZoomRotationSpeed);
        }
        else
        {
            cinemachineVirtualCamera.m_Lens.FieldOfView = defaultFOV;
            ZoomVignette.SetActive(false);
            firstPersonController.ChangeRotationSpeed(defaultRotationSped);

        }

    }
}
