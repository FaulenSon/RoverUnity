﻿using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask m_RoverMask;     
    public ParticleSystem m_ExplosionParticles;     
    public AudioSource m_ExplosionAudio;     
    public float m_MaxDamage = 10f;      
    public float m_ExplosionForce = 1000f;       
    public float m_MaxLifeTime = 2f;            
    public float m_ExplosionRadius = 8f;         


    private void Start()     
    {
        Destroy(gameObject, m_MaxLifeTime);     
    }


    private void OnTriggerEnter(Collider other)     
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_RoverMask);     
        for (int i = 0; i < colliders.Length; i++)     
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();     
            if (!targetRigidbody)     
                continue;

            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);     
            RoverHealth roverHealth = targetRigidbody.GetComponent<RoverHealth>();     
            if (!roverHealth)     
                continue;
            float damage = CalculateDamage(targetRigidbody.position);     
            roverHealth.TakeDamage(damage);     
        }

        m_ExplosionParticles.transform.parent = null;     
        m_ExplosionParticles.Play();     
        m_ExplosionAudio.Play();     
        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);     
        Destroy(gameObject);     
    }


    private float CalculateDamage(Vector3 targetPosition)     
    {
        Vector3 explosionToTarget = targetPosition - transform.position;     
        float explosionDistance = explosionToTarget.magnitude;     
        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius; ;     
        float damage = relativeDistance * m_MaxDamage;     
        damage = Mathf.Max(0f, damage);     
        return damage;

    }
}