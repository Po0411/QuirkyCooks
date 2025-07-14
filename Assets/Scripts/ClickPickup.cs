using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPickup : MonoBehaviour
{
    public float pickupRange = 5f;
    public LayerMask pickupLayer;
    public Camera playerCamera;
    public InventoryManager inventory;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (inventory == null)
            {
                Debug.LogError("InventoryManager가 할당되지 않았습니다!");
                return;
            }

            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * pickupRange, Color.green, 1f);

            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, pickupLayer))
            {
                // ① 자신에게 PickupItem이 있는지
                PickupItem item = hit.collider.GetComponent<PickupItem>();
                // ② 없으면 부모에서 찾아보기
                if (item == null)
                    item = hit.collider.GetComponentInParent<PickupItem>();

                if (item != null)
                {
                    Debug.Log("아이템 감지 → " + item.itemData.itemName);
                    inventory.AddItem(item.itemData);
                    Destroy(item.gameObject);
                }
                else
                {
                    Debug.Log("PickupItem 컴포넌트가 Collider나 부모에 없습니다.");
                }
            }
            else
            {
                Debug.Log("아무 것도 맞지 않음");
            }
        }
    }
}