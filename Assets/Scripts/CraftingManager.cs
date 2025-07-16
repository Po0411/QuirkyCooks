using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CraftingManager : MonoBehaviour
{
    [System.Serializable]
    public class Recipe
    {
        public string recipeName;
        public ItemData result;                 // 완성품
        public List<ItemData> ingredients;      // 필요한 재료 리스트
    }

    [Header("Recipes")]
    public List<Recipe> recipes;

    [Header("References")]
    public InventoryManager inventoryManager;   // 인벤토리 매니저
    public TextMeshProUGUI alertText;           // 알림용 텍스트
    public ItemData trashItem;                  // 실패 시 생성할 쓰레기

    [Header("Control Keys")]
    public KeyCode depositKey = KeyCode.E;      // 재료 투입
    public KeyCode craftKey = KeyCode.C;      // 제작 시도

    private bool isInZone = false;
    private List<ItemData> containerItems = new List<ItemData>();

    void Start()
    {
        if (alertText != null)
            alertText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isInZone) return;

        // 1) 재료 투입 (E)
        if (Input.GetKeyDown(depositKey))
            DepositItem();

        // 2) 제작 시도 (C)
        if (Input.GetKeyDown(craftKey) && recipes.Count > 0)
            TryCraft(recipes[0]);
    }

    void DepositItem()
    {
        var item = inventoryManager.GetSelectedItem();
        if (item == null)
        {
            ShowAlert("투입할 재료가 없습니다.");
            return;
        }
        if (inventoryManager.RemoveItem(item, 1))
        {
            containerItems.Add(item);
            ShowAlert(item.itemName + " 투입 완료");
        }
        else
        {
            ShowAlert("재료 수량 부족");
        }
    }

    void TryCraft(Recipe recipe)
    {
        // 투입된 개수 체크
        if (containerItems.Count != recipe.ingredients.Count)
        {
            ShowAlert("투입 재료 개수가 일치하지 않습니다.");
            ReturnAll();
            return;
        }

        // 재료 매칭 체크
        var temp = new List<ItemData>(containerItems);
        bool match = true;
        foreach (var ing in recipe.ingredients)
        {
            if (!temp.Remove(ing))
            {
                match = false;
                break;
            }
        }

        // 결과 처리
        if (match)
        {
            inventoryManager.AddItem(recipe.result);
            ShowAlert(recipe.result.itemName + " 제작 완료!");
        }
        else
        {
            inventoryManager.AddItem(trashItem);
            ShowAlert("잘못된 조합! 쓰레기 생성");
        }

        containerItems.Clear();
    }

    void ReturnAll()
    {
        // 투입해둔 재료 전부 반환
        foreach (var ing in containerItems)
            inventoryManager.AddItem(ing);
        containerItems.Clear();
    }

    void ShowAlert(string msg)
    {
        if (alertText == null)
        {
            Debug.LogWarning("CraftingManager: alertText 미할당 – 메시지: " + msg);
            return;
        }
        StopAllCoroutines();
        alertText.text = msg;
        alertText.gameObject.SetActive(true);
        StartCoroutine(ClearAlert());
    }

    IEnumerator ClearAlert()
    {
        yield return new WaitForSeconds(2f);
        if (alertText != null)
            alertText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CraftingZone"))
            isInZone = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CraftingZone"))
        {
            isInZone = false;
            ReturnAll();
        }
    }
}