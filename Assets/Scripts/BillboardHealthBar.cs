using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class BillboardHealthBar : MonoBehaviour
{
    [Header("Target & Offset")]
    public Transform target;
    public Vector3 offset = new Vector3(0, 2.5f, 0);

    [Header("UI Components")]
    public Image fillImage;

    [Header("Camera (optional)")]
    [Tooltip("MainCamera �±׸� ������� ���� �� ���� ����")]
    public Camera overrideCamera;

    private Transform cam;

    void Start()
    {
        // 1) ���� ������ ī�޶� ������ ���, �ƴϸ� Camera.main ���
        cam = (overrideCamera != null ? overrideCamera.transform : Camera.main?.transform);

        if (cam == null) Debug.LogError("BillboardHealthBar: ����� ī�޶� �����ϴ�!");
        if (target == null) Debug.LogError("BillboardHealthBar: target ���Ҵ�!");
        if (fillImage == null) Debug.LogError("BillboardHealthBar: fillImage ���Ҵ�!");
    }

    void LateUpdate()
    {
        if (cam == null || target == null || fillImage == null)
            return;

        transform.position = target.position + offset;
        transform.rotation = Quaternion.LookRotation(transform.position - cam.position);
    }

    public void SetHealthFraction(float fraction)
    {
        if (fillImage != null)
            fillImage.fillAmount = Mathf.Clamp01(fraction);
    }
}