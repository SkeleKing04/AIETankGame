using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tankHealth : MonoBehaviour
{
    // The amount of health each tank starts with
    public float m_StartingHealth = 100f;

    //A prefab that will be instantiated in Awake, then used whenever the tank dies
    public GameObject m_ExplosionPrefab;

    public float m_CurrentHealth;
    private bool m_Dead;
    // The particale sysyem tahat will play when the tank is destroyed
    private ParticleSystem m_ExplosionParticles;
    public Image m_HealthBar;
    public Image m_HealthBarBackdrop;
    public Image m_lowHealthEdge;
    //public float m_HealthBarBlinksize;
    //public float m_HealthBarBlinkspeed;
    private void Awake()
    {
        // Instantiate the explosion prefab and get a reference to the particle system on it
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();

        //Disable the prefab so it can be activated when it's required
        m_ExplosionParticles.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        //when the tank is enabled, reset the tank's health and whether or not it's dead
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;
        SetHealthUI();
    }
    private void SetHealthUI()
    {
        if (m_HealthBar)
        {
            m_HealthBar.fillAmount = m_CurrentHealth / m_StartingHealth;
        }
        if (tag == "Player")
        {
            m_lowHealthEdge.transform.localScale = Vector3.one * ((m_CurrentHealth / m_StartingHealth) + 0.72f);
            if (m_CurrentHealth <= (m_StartingHealth * 0.25))
            {
                m_HealthBarBackdrop.color = Color.red;
            }
        }
        else 
        {
            m_HealthBarBackdrop.color = Color.white;
        }
    }
    public void TakeDamage(float amount)
    {
        // Reduce current health by the amount of damage done
        m_CurrentHealth -= amount;

        // Change the UI elements appropriately
        SetHealthUI();

        // if the current health is ar or below zero and it has not yet been registered, call OnDeath
        if (m_CurrentHealth <= 0f && !m_Dead)
        {
            OnDeath();
        }
    }
    private void OnDeath()
    {
        if (tag == "Enemy")
        {
            TankSpawner spTank = Object.FindObjectOfType<TankSpawner>();
            spTank.spawnTank();
            TankShooting giveBomb = Object.FindObjectOfType<TankShooting>();
            giveBomb.m_bombCount++;
        }
        // Set the flag so that this function is only called once
        m_Dead = true;

        // Move the instantiated explosion prefab to the tank's position and turn it on
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        // play the particle system of the tank exploding
        m_ExplosionParticles.Play();
        // Turn the tank off
        gameObject.SetActive(false);
    }
    IEnumerator healthBlink()
    { 
        if (tag == "Player")
        {
            while (true)
            {
                //float healthblinker = (Mathf.Sin((Time.time * m_HealthBarBlinkspeed) * m_HealthBarBlinksize));
                //m_HealthBar.rectTransform.localScale = new Vector3(healthblinker + 2f, healthblinker + 2f, healthblinker + 2f);
                //m_HealthBarBackdrop.rectTransform.localScale = new Vector3(healthblinker + 2f, healthblinker + 2f, healthblinker + 2f);
                //m_lowHealthEdge.transform.localScale = new Vector3(healthblinker + 1.2f, healthblinker + 1.2f, healthblinker + 1.2f);
                yield return new WaitForSeconds(0);
            }
        }
    }
}
