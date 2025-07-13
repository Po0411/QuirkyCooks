using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    [Header("스태미나 수치")]
    public float maxStamina = 100f;
    public float currentStamina;
    public float drainRate = 20f;      // 초당 감소량
    public float recoverRate = 10f;    // 초당 회복량

    [Header("UI")]
    public Image staminaFillImage;     // Fill 이미지 연결

    [Header("입력 키")]
    public KeyCode runKey = KeyCode.LeftShift;

    private bool isRunningAllowed = true;

    void Start()
    {
        currentStamina = maxStamina;
    }

    void Update()
    {
        bool isTryingToRun = Input.GetKey(runKey) && Input.GetAxis("Vertical") > 0;

        if (isTryingToRun && isRunningAllowed)
        {
            currentStamina -= drainRate * Time.deltaTime;

            if (currentStamina <= 0)
            {
                currentStamina = 0;
                isRunningAllowed = false;
            }
        }
        else
        {
            currentStamina += recoverRate * Time.deltaTime;

            if (currentStamina >= maxStamina)
            {
                currentStamina = maxStamina;
            }

            if (currentStamina > 10f) // 일정량 이상 회복되면 다시 달리기 가능
                isRunningAllowed = true;
        }

        UpdateStaminaUI();
    }

    void UpdateStaminaUI()
    {
        if (staminaFillImage != null)
        {
            staminaFillImage.fillAmount = currentStamina / maxStamina;
        }
    }

    public bool CanRun()
    {
        return isRunningAllowed;
    }
}
