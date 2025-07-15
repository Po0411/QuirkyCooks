using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CraftingZone : MonoBehaviour
{
    void Reset()
    {
        // Collider가 Trigger로 설정되어 있는지 체크
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    // 씬 뷰에서 영역이 보이도록 Gizmo 그리기 (선택)
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
        var box = GetComponent<BoxCollider>();
        if (box != null)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(box.center, box.size);
        }
    }
}