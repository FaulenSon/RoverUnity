using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
   public GameObject panel;

    public void PanelOpen()
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void MainMenu()
    {
        Application.LoadLevel(0);
    }
}
