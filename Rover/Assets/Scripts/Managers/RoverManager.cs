using System;
using UnityEngine;

[Serializable]
public class RoverManager
{
    public Color m_PlayerColor;      //цвет игрока      
    public Transform m_Spawn;     //точка возрождения    
    [HideInInspector] public int m_PlayerNumber;   //номер игрока          
    [HideInInspector] public string m_ColoredPlayerText;  //цвет текста игрока
    [HideInInspector] public GameObject m_Instance;    //игровой объект ровера      
    [HideInInspector] public int m_Wins;            //колличество побед ровера         


    private RoverMovement m_Movement;       //ссылка на скрипт движения ровера
    private RoverShooting m_Shooting;       //ссылка на скрипт стрельбы ровера
    private GameObject m_CanvasGameObject;   //ссылка на объект интерфейса


    public void Setup()
    {
        m_Movement = m_Instance.GetComponent<RoverMovement>(); //запускаем экземпляр движения 
        m_Shooting = m_Instance.GetComponent<RoverShooting>();  //запускаем экземпляр стрельбы 
        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;  //запускаем экземпляр интерфейса

        m_Movement.m_PlayerNumber = m_PlayerNumber; //устанавливаем на номер игрока скрипты движения и стрельбы
        m_Shooting.m_PlayerNumber = m_PlayerNumber;

        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">ИГРОК " + m_PlayerNumber + "</color>"; //Создаем строку, используя правильный цвет с надписью «ИГРОК 1», основываясь на цвете ровера и номере игрока.

        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = m_PlayerColor;
        }
    }


    public void DisableControl() //отключаем управление 
    {
        m_Movement.enabled = false; //отключаем движение 
        m_Shooting.enabled = false; //отключаем стрельбу 

        m_CanvasGameObject.SetActive(false); //отключаем интерфейс
    }


    public void EnableControl() //включаем управление
    {
        m_Movement.enabled = true; //включаем движение 
        m_Shooting.enabled = true; //включаем стрельбу 

        m_CanvasGameObject.SetActive(true); //включаем интерфейс
    }


    public void Reset() //сброс. Используется в начале каждого раунда, чтобы перевести ровер в состояние по умолчанию.
    {
        m_Instance.transform.position = m_Spawn.position; //устанавливаем расположение по умолчанию
        m_Instance.transform.rotation = m_Spawn.rotation; //устанавливаем вращение по умолчанию

        m_Instance.SetActive(false); //отключаем игровой объект
        m_Instance.SetActive(true); //включаем
    }
}
