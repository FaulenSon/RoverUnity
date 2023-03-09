using UnityEngine;
using UnityEngine.UI;

public class RoverHealth : MonoBehaviour
{
    public float m_StartingHealth = 150f;     
    public Slider m_Slider;                     
    public Image m_FillImage;                     
    public Color m_FullHealthColor = Color.blue;      
    public Color m_ZeroHealthColor = Color.yellow;        
    public GameObject m_ExplosionPrefab;     
    
    
    private AudioSource m_Baang;      
    private ParticleSystem m_RoverDestruktion;       
    private float m_CurrentHealth;      
    private bool m_Dead;     


    private void Awake()     
    {


            m_RoverDestruktion = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();     
         m_Baang = m_RoverDestruktion.GetComponent<AudioSource>();     

        m_RoverDestruktion.gameObject.SetActive(false);      
    }


    private void OnEnable()     
    {
        m_CurrentHealth = m_StartingHealth;     

            m_Dead = false;     

      SetHealthUI();     
    }
    

    public void TakeDamage(float amount)     
    {
        

                m_CurrentHealth -= amount;     

        SetHealthUI();     

        if (m_CurrentHealth <= 0f && !m_Dead)       
        {
            OnDeath();
        }
    }


    private void SetHealthUI()     
    {
        m_Slider.value = m_CurrentHealth;     
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);     
    }


    private void OnDeath()     
    {
        m_Dead = true;
        m_RoverDestruktion.transform.position = transform.position;     
        m_RoverDestruktion.gameObject.SetActive(true);     
        m_RoverDestruktion.Play();     
        m_Baang.Play();     
        gameObject.SetActive(false);     
    }
}