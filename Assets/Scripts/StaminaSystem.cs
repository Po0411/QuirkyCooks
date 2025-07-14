using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    [Header("스태미나 수치")]
    public float maxStamina = 100f;
    public float currentStamina;
    public float drainRate = 20f;
    public float recoverRate = 10f;

    [Header("UI")]
    public Image staminaFillImage;

    private bool isRunningAllowed = true;
    private bool isTryingToRun = false;

    void Start()
    {
        currentStamina = maxStamina;
    }

    void Update()
    {
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
            if (currentStamina > 10f)
            {
                isRunningAllowed = true;
            }
        }

        UpdateStaminaUI();
    }

    void UpdateStaminaUI()
    {
        if (staminaFillImage != null)
            staminaFillImage.fillAmount = currentStamina / maxStamina;
    }

    public bool CanRun() => isRunningAllowed;
    public void SetRunningState(bool value) => isTryingToRun = value;
}
