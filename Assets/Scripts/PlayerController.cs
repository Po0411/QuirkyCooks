using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;

    private CharacterController controller;
    private StaminaSystem staminaSystem;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        staminaSystem = GetComponent<StaminaSystem>();

        if (controller == null)
            Debug.LogError("❌ CharacterController 컴포넌트가 Player에 없습니다!");

        if (staminaSystem == null)
            Debug.LogError("❌ StaminaSystem 컴포넌트가 Player에 없습니다!");
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;

        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift);
        bool canRun = staminaSystem != null && staminaSystem.CanRun();
        bool isTryingToRun = isShiftPressed && v > 0f && canRun;

        if (staminaSystem != null)
            staminaSystem.SetRunningState(isTryingToRun);

        float speed = isTryingToRun ? runSpeed : walkSpeed;

        if (controller != null)
            controller.Move(move * speed * Time.deltaTime);
    }
}
