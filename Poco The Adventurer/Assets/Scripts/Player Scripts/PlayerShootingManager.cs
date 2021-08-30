using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShootingManager : MonoBehaviour
{
    [SerializeField] private float shootingTimerLimit = 0.2f;
    private float shootingTimer;

    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private Joystick joystick;

    private Animator shootingAnimator;
    private PlayerWeaponManager playerWeaponManager;

    [SerializeField] private FireButton fireButton;

    private void Awake()
    {
        playerWeaponManager = GetComponent<PlayerWeaponManager>();
        shootingAnimator = bulletSpawnPos.GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        HandleShooting();
    }

    public void HandleShooting()
    {
        if(fireButton.IsPressed)
        {
            if(Time.time > shootingTimer)
            {
                shootingTimer = Time.time + shootingTimerLimit;
                
                // animate muzzle flash
                shootingAnimator.SetTrigger(TagManager.SHOOT_ANIMATION_PARAMETER);
                CreateBullet();
            }
        }
    }

    private void CreateBullet()
    {
        playerWeaponManager.Shoot(bulletSpawnPos.position);
    }
}
