using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    [Header("Toggle Settings")]
    [Tooltip("ǥ��/��ǥ���� UI ������Ʈ")]
    public GameObject uiObject;

    [Tooltip("����� Ű (Default = F)")]
    public KeyCode toggleKey = KeyCode.F;

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (uiObject != null)
            {
                uiObject.SetActive(!uiObject.activeSelf);
            }
            else
            {
                Debug.LogWarning("ToggleUI: uiObject�� �Ҵ���� �ʾҽ��ϴ�!");
            }
        }
    }
}
