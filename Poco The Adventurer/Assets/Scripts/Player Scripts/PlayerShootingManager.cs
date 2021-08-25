using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingManager : MonoBehaviour
{
    [SerializeField] private float shootingTimerLimit = 0.2f;
    private float shootingTimer;

    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private Joystick joystick;

    private PlayerWeaponManager playerWeaponManager;
    

    private void Awake()
    {
        playerWeaponManager = GetComponent<PlayerWeaponManager>();
    }

    private void LateUpdate()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        if(Input.GetMouseButton(0) && !joystick.IsTouched)
        {
            if(Time.time > shootingTimer)
            {
                shootingTimer = Time.time + shootingTimerLimit;
                
                // animate muzzle flash

                CreateBullet();
            }
        }
    }

    private void CreateBullet()
    {
        playerWeaponManager.Shoot(bulletSpawnPos.position);
    }
}
