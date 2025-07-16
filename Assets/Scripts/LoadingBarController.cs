using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBarController : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("Fill Image (type=Filled, Fill Method=Horizontal)")]
    public Image gaugeImage;

    [Header("Loading Settings")]
    [Tooltip("Duration in seconds for gauge to fill from 0→1")]
    public float fillDuration = 3f;
    [Tooltip("Name of the Scene to load when gauge is full")]
    public string sceneToLoad;

    private void Start()
    {
        if (gaugeImage == null)
        {
            Debug.LogError("LoadingBarController: gaugeImage is not assigned!");
            enabled = false;
            return;
        }
        gaugeImage.type = Image.Type.Filled;
        gaugeImage.fillMethod = Image.FillMethod.Horizontal;
        gaugeImage.fillOrigin = (int)Image.OriginHorizontal.Left;
        gaugeImage.fillAmount = 0f;

        StartCoroutine(FillAndLoad());
    }

    private IEnumerator FillAndLoad()
    {
        float elapsed = 0f;
        while (elapsed < fillDuration)
        {
            elapsed += Time.deltaTime;
            gaugeImage.fillAmount = Mathf.Clamp01(elapsed / fillDuration);
            yield return null;
        }

        // 완료 시 씬 전환
        if (!string.IsNullOrEmpty(sceneToLoad))
            SceneManager.LoadScene(sceneToLoad);
        else
            Debug.LogWarning("LoadingBarController: sceneToLoad is empty!");
    }
}
