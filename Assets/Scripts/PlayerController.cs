using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;

    [Header("Jump")]
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Drop")]
    public GameObject dropPrefab;
    public Transform dropSpawnPoint;

    [Header("UI & Inventory")]
    public InventoryManager inventoryManager;
    public GameObject scoreboardUI;
    public GameObject settingsUI;

    private CharacterController controller;
    private StaminaSystem staminaSystem;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        staminaSystem = GetComponent<StaminaSystem>();
        if (inventoryManager == null)
            inventoryManager = FindObjectOfType<InventoryManager>();

        if (controller == null)
            Debug.LogError("❌ CharacterController 컴포넌트가 Player에 없습니다!");
        if (staminaSystem == null)
            Debug.LogError("❌ StaminaSystem 컴포넌트가 Player에 없습니다!");
        if (inventoryManager == null)
            Debug.LogError("❌ InventoryManager를 찾을 수 없습니다!");
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleDrop();
        HandleSlotKeys();
        HandleScoreboardToggle();
        HandleSettingsToggle();
    }

    void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;

        bool isShift = Input.GetKey(KeyCode.LeftShift);
        bool canRun = staminaSystem != null && staminaSystem.CanRun();
        bool tryRun = isShift && v > 0f && canRun;
        if (staminaSystem != null) staminaSystem.SetRunningState(tryRun);

        float speed = tryRun ? runSpeed : walkSpeed;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleJump()
    {
        // 점프는 HandleMovement에서 velocity를 조정하므로, 별도 처리 필요 없음
    }

    void HandleDrop()
    {
        if (Input.GetKeyDown(KeyCode.Q) && inventoryManager != null)
        {
            var item = inventoryManager.GetSelectedItem();
            if (item != null && inventoryManager.RemoveItem(item, 1))
            {
                GameObject go = Instantiate(dropPrefab, dropSpawnPoint.position, Quaternion.identity);
                var pickup = go.GetComponent<PickupItem>();
                if (pickup != null) pickup.itemData = item;
            }
        }
    }

    void HandleSlotKeys()
    {
        for (int i = 1; i <= 7; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                inventoryManager.SelectSlot(i - 1);
        }
    }

    void HandleScoreboardToggle()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && scoreboardUI != null)
            scoreboardUI.SetActive(!scoreboardUI.activeSelf);
    }

    void HandleSettingsToggle()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && settingsUI != null)
            settingsUI.SetActive(!settingsUI.activeSelf);
    }
}
