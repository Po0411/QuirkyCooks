using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBoardUI : MonoBehaviour
{
    public GameObject statusBoardPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            statusBoardPanel.SetActive(!statusBoardPanel.activeSelf);
        }
    }
}
