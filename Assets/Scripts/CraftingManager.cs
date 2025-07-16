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
    [Tooltip("잘못된 조합 시 나오는 쓰레기 아이템")]
    public ItemData trashItem;

    [Header("Input Keys")]
    public KeyCode depositKey = KeyCode.E;
    public KeyCode craftKey = KeyCode.C;

    private bool isInCraftingZone = false;
    private List<ItemData> containerItems = new List<ItemData>();

    void Start()
    {
        if (alertText != null)
            alertText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isInCraftingZone) return;

        // 아이템 투입
        if (Input.GetKeyDown(depositKey))
        {
            DepositSelectedItem();
        }
        // 조리 시도
        if (Input.GetKeyDown(craftKey))
        {
            if (recipes.Count > 0)
                TryCraft(recipes[0]);
        }
    }

    void DepositSelectedItem()
    {
        var item = inventoryManager.GetSelectedItem();
        if (item == null)
        {
            StartCoroutine(ShowAlert("투입할 아이템이 없습니다."));
            return;
        }
        if (inventoryManager.RemoveItem(item, 1))
        {
            containerItems.Add(item);
            StartCoroutine(ShowAlert(item.itemName + " 투입 완료"));
        }
    }

    public void TryCraft(Recipe recipe)
    {
        // 투입된 개수와 레시피 개수 비교
        if (containerItems.Count != recipe.ingredients.Count)
        {
            StartCoroutine(ShowAlert("레시피 재료 개수가 일치하지 않습니다."));
            ReturnAllIngredients();
            return;
        }

        // 재료 일치 검사
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

        if (match)
        {
            inventoryManager.AddItem(recipe.result);
            StartCoroutine(ShowAlert(recipe.result.itemName + " 제작 완료!"));
        }
        else
        {
            inventoryManager.AddItem(trashItem);
            StartCoroutine(ShowAlert("잘못된 조합! 쓰레기 생성"));
        }

        containerItems.Clear();
    }

    void ReturnAllIngredients()
    {
        foreach (var ing in containerItems)
            inventoryManager.AddItem(ing);
        containerItems.Clear();
    }

    private IEnumerator ShowAlert(string msg)
    {
        if (alertText == null) yield break;
        alertText.text = msg;
        alertText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        alertText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CraftingZone"))
        {
            isInCraftingZone = true;
            StartCoroutine(ShowAlert("요리 공간에 입장했습니다."));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CraftingZone"))
        {
            isInCraftingZone = false;
            StartCoroutine(ShowAlert("요리 공간을 벗어났습니다."));
            // 미사용 재료 반환
            ReturnAllIngredients();
        }
    }
}
