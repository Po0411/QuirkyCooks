using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class UIBGScroller : MonoBehaviour
{
    [Tooltip("��ũ�� �ӵ� (UV ����/��)")]
    public float speed = 0.5f;

    private float offset = 0f;
    private RawImage rawImage;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        // UV �ʱⰪ ��������
        offset = rawImage.uvRect.x;
    }

    void Update()
    {
        // ������ ����
        offset += Time.deltaTime * speed;
        // 0~1 ������ ����(optional)
        offset %= 1f;

        // ���� UV ũ��� Y ��ġ�� ����
        Rect uv = rawImage.uvRect;
        uv.x = offset;
        rawImage.uvRect = uv;
    }
}
