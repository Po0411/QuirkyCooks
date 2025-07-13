using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    private CharacterController controller;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDrainRate = 10f;
    public float staminaRegenRate = 5f;
    private bool isRunningAllowed = true;

    [Header("Stamina UI")]
    public Image staminaFillImage; 

    [Header("Combat")]
    public GameObject airGunPrefab;
    public Transform firePoint;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentStamina = maxStamina;
    }

    void Update()
    {
        HandleMovement();
        HandleCombat();
        HandleInteraction();
        HandleDrop();
        UpdateStaminaUI();  // ✅ UI 반영
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;

        bool isTryingToRun = Input.GetKey(KeyCode.LeftShift) && v > 0f;

        if (isTryingToRun && isRunningAllowed && currentStamina > 0f)
        {
            controller.Move(move * runSpeed * Time.deltaTime);
            currentStamina -= staminaDrainRate * Time.deltaTime;

            if (currentStamina <= 0f)
            {
                currentStamina = 0f;
                isRunningAllowed = false;
            }
        }
        else
        {
            controller.Move(move * walkSpeed * Time.deltaTime);
            currentStamina += staminaRegenRate * Time.deltaTime;

            if (currentStamina >= maxStamina)
            {
                currentStamina = maxStamina;
            }

            if (currentStamina > 10f)
            {
                isRunningAllowed = true;
            }
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }

    void UpdateStaminaUI()
    {
        if (staminaFillImage != null)
        {
            staminaFillImage.fillAmount = currentStamina / maxStamina;
        }
    }

    void HandleCombat()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(airGunPrefab, firePoint.position, firePoint.rotation);
        }
    }

    void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // 상호작용 로직
        }
    }

    void HandleDrop()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 아이템 드랍 로직
        }
    }
}
