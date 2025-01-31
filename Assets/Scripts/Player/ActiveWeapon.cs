using UnityEngine;
using StarterAssets;
using Cinemachine;
using TMPro;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO startingWeapon;
    [SerializeField] GameObject ZoomVignette;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] Camera weaponCamera;
    [SerializeField] TMP_Text ammoText;

    WeaponSO currentWeaponSO;
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    Weapon currentWeapon;
    FirstPersonController firstPersonController;

    float timeSinceLastShot = 0;
    float defaultFOV;
    float defaultRotationSped;
    int currentAmmo;


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
        SwitchWeapon(startingWeapon);
        AdjustAmmo(currentWeaponSO.MagzineSize);
    }


    void Update()
    {

        handleShoot();
        HandleZoom();

    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;
        if(currentAmmo > currentWeaponSO.MagzineSize) {
            currentAmmo = currentWeaponSO.MagzineSize;
        }

        ammoText.text = currentAmmo.ToString("D2");
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }

        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();// tramsform will defaul child this into current class and use preset transform
        currentWeapon = newWeapon;
        this.currentWeaponSO = weaponSO;
        AdjustAmmo(currentWeaponSO.MagzineSize);
    }

    private void handleShoot()
    {
        timeSinceLastShot += Time.deltaTime;

        if (!starterAssetsInputs.shoot) return;

        if (timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            currentWeapon.Shoot(currentWeaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
            timeSinceLastShot = 0;
            AdjustAmmo(-1);
        }
        if (!currentWeaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }
    }

    void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom) return;

        if (starterAssetsInputs.zoom)
        {
            cinemachineVirtualCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;
            ZoomVignette.SetActive(true);
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
        }
        else
        {
            cinemachineVirtualCamera.m_Lens.FieldOfView = defaultFOV;
            weaponCamera.fieldOfView = defaultFOV;
            ZoomVignette.SetActive(false);
            firstPersonController.ChangeRotationSpeed(defaultRotationSped);

        }

    }
}
