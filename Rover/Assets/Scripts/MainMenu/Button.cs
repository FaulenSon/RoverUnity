using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System;

public class Button : MonoBehaviour {

    public GameObject option;
    public GameObject helpgame;
    public GameObject avtors;
    public GameObject confirmationpanel;
    public GameObject confirmationpanel1;

    public Slider Music;
    public Text valueMusic;

    private void Update()   
    {
        valueMusic.text = Music.value.ToString();
        AudioListener.volume = Music.value;
    }

    public void StartGame()    
    {
        confirmationpanel.SetActive(!confirmationpanel.activeSelf);
    }

    public void ConfirmationPanelClose()   
    {
        confirmationpanel.SetActive(!confirmationpanel.activeSelf);
    }

    public void ConfirmationPanlPlay()   
    {
        Application.LoadLevel(1);
    }

    public void Option()   
    {
        option.SetActive(!option.activeSelf);
    }

    public void HelpGame()   
    {
        helpgame.SetActive(!helpgame.activeSelf);
    }

    public void Quit()    
    {
        confirmationpanel1.SetActive(!confirmationpanel1.activeSelf);
      
    }

    public void QuitClose()    
    {
        confirmationpanel1.SetActive(!confirmationpanel1.activeSelf);
    }

    public void QuitPlay()   
    {
        Application.Quit();
    }

    public void PanelHelpGameClose()   
    {
        helpgame.SetActive(!helpgame.activeSelf);
    }

    public void Avtors()   
    {
       avtors.SetActive(!avtors.activeSelf);
    }

    public void Manual()   
    {
        Process SysInfo = new Process();
        SysInfo.StartInfo.ErrorDialog = true;
        string path = AppDomain.CurrentDomain.BaseDirectory + "NewProject_Help.exe";
        SysInfo.StartInfo.FileName = path;
        SysInfo.Start();
    }
}
