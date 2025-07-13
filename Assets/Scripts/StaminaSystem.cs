using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    [Header("���¹̳� ��ġ")]
    public float maxStamina = 100f;
    public float currentStamina;
    public float drainRate = 20f;      // �ʴ� ���ҷ�
    public float recoverRate = 10f;    // �ʴ� ȸ����

    [Header("UI")]
    public Image staminaFillImage;     // Fill �̹��� ����

    [Header("�Է� Ű")]
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

            if (currentStamina > 10f) // ������ �̻� ȸ���Ǹ� �ٽ� �޸��� ����
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
