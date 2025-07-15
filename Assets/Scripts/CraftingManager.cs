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
        public ItemData result;
        public List<ItemData> ingredients;
    }

    [Header("Recipes")]
    public List<Recipe> recipes;

    [Header("References")]
    public InventoryManager inventoryManager;
    public TextMeshProUGUI alertText;

    private bool isInCraftingZone = false;

    void Start()
    {
        if (alertText != null)
            alertText.gameObject.SetActive(false);
    }

    void Update()
    {
        // C 키로 첫 번째 레시피 제작 시도 (필요시 UI 선택 로직 추가)
        if (Input.GetKeyDown(KeyCode.C) && recipes.Count > 0)
        {
            TryCraft(recipes[0]);
        }
    }

    /// <summary>
    /// 레시피에 필요한 재료가 있는지, 요리 공간에 있는지 확인 후 제작합니다.
    /// </summary>
    public void TryCraft(Recipe recipe)
    {
        if (!isInCraftingZone)
        {
            StartCoroutine(ShowAlert("요리 공간 안에 있어야 제작할 수 있습니다."));
            return;
        }

        // 재료 체크
        foreach (var ing in recipe.ingredients)
        {
            if (!inventoryManager.HasItem(ing, 1))
            {
                StartCoroutine(ShowAlert($"재료 부족: {ing.itemName}"));
                return;
            }
        }

        // 재료 제거
        foreach (var ing in recipe.ingredients)
        {
            inventoryManager.RemoveItem(ing, 1);
        }

        // 결과 아이템 추가
        inventoryManager.AddItem(recipe.result);
        StartCoroutine(ShowAlert($"{recipe.result.itemName} 제작 완료!"));
    }

    private IEnumerator ShowAlert(string msg)
    {
        if (alertText == null)
            yield break;

        alertText.text = msg;
        alertText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        alertText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CraftingZone"))
        {
            Debug.Log("▶️ 플레이어가 요리 공간에 들어왔습니다.");
            isInCraftingZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CraftingZone"))
        {
            Debug.Log("◀️ 플레이어가 요리 공간을 벗어났습니다.");
            isInCraftingZone = false;
        }
    }
}
