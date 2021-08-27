using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    private float moveX, moveY;
    private Camera mainCam;
    private Animator anim;

    private Vector2 mousePosition;
    private Vector2 direction;
    private Vector3 tempScale;

    private PlayerWeaponManager playerWeaponManager;
    private CharacterHealth playerHealth;

    [SerializeField] private Joystick joystick;

    protected override void Awake()
    {
        base.Awake();

        mainCam = Camera.main;
        anim = GetComponent<Animator>();
        playerWeaponManager = GetComponent<PlayerWeaponManager>();
    }

    private void Start()
    {
        playerHealth = GetComponent<CharacterHealth>();    
    }

    private void FixedUpdate()
    {
        if (!playerHealth.IsAlive())
            return;

        if (GameplayController.Instance.runOnMobile)
        {
            if (joystick.Horizontal < 0)
                moveX = (joystick.Horizontal < -0.3) ? -1 : 0;
            else
                moveX = (joystick.Horizontal > 0.3) ? 1 : 0;

            if (joystick.Vertical < 0)
                moveY = (joystick.Vertical < -0.3) ? -1 : 0;
            else
                moveY = (joystick.Vertical > 0.3) ? 1 : 0;
        }
        else
        {
            moveX = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
            moveY = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);
        }

        HandlePlayerTurning();
        HandleMovement(moveX, moveY);
    }

    private void HandlePlayerTurning()
    {
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2(mousePosition.x - transform.position.x,
                                mousePosition.y - transform.position.y).normalized;

        HandlePlayerAnimation(direction.x,direction.y);
    }

    private void HandlePlayerAnimation(float x, float y)
    {
        x = Mathf.RoundToInt(x);
        y = Mathf.RoundToInt(y);

        tempScale = transform.localScale;
        if (x > 0)
            tempScale.x = Mathf.Abs(tempScale.x);
        else if (x < 0)
            tempScale.x =  -Mathf.Abs(tempScale.x);
        transform.localScale = tempScale;

        x = Mathf.Abs(x);

        anim.SetFloat(TagManager.FACE_X_ANIMATION_PARAMETER, x);
        anim.SetFloat(TagManager.FACE_Y_ANIMATION_PARAMETER, y);

        ActivateWeaponForSide(x, y);
    }

    private void ActivateWeaponForSide(float x, float y)
    {
        if (x == 1f && y == 0f)
            playerWeaponManager.ActivateGun(0);
        else if (x == 0f && y == 1f)
            playerWeaponManager.ActivateGun(1);
        else if (x == 0f && y == -1f)
            playerWeaponManager.ActivateGun(2);
        else if (x == 1f && y == 1f)
            playerWeaponManager.ActivateGun(3);
        else if (x == 1f && y == -1f)
            playerWeaponManager.ActivateGun(4);
    }
}
