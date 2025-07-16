using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonSceneLoader : MonoBehaviour
{
    [Tooltip("이동할 씬의 이름 (Build Settings에 등록된 이름)")]
    public string sceneName;

    /// <summary>
    /// 버튼 OnClick 이벤트에 할당하세요.
    /// </summary>
    public void LoadScene()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("ButtonSceneLoader: sceneName이 비어 있습니다!");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}
