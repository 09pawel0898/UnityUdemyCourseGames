using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    private float moveX, moveY;
    private Camera mainCam;
    private Vector2 mousePosition;
    private Vector2 direction;
    private Vector3 tempScale;

    private Animator anim;

    protected override void Awake()
    {
        base.Awake();

        mainCam = Camera.main;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        moveX = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
        moveY = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);

        HandlePlayerTurning();
        HandleMovement(moveX, moveY);
    }

    void HandlePlayerTurning()
    {
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2(mousePosition.x - transform.position.x,
                                mousePosition.y - transform.position.y).normalized;

        HandlePlayerAnimation(direction.x,direction.y);
    }

    void HandlePlayerAnimation(float x, float y)
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
    }
}
