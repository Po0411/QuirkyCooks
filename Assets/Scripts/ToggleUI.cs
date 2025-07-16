using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    [Header("Toggle Settings")]
    [Tooltip("표시/비표시할 UI 오브젝트")]
    public GameObject uiObject;

    [Tooltip("토글할 키 (Default = F)")]
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
                Debug.LogWarning("ToggleUI: uiObject가 할당되지 않았습니다!");
            }
        }
    }
}
