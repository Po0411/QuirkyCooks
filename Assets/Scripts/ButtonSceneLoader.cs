using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonSceneLoader : MonoBehaviour
{
    [Tooltip("�̵��� ���� �̸� (Build Settings�� ��ϵ� �̸�)")]
    public string sceneName;

    /// <summary>
    /// ��ư OnClick �̺�Ʈ�� �Ҵ��ϼ���.
    /// </summary>
    public void LoadScene()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("ButtonSceneLoader: sceneName�� ��� �ֽ��ϴ�!");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}
