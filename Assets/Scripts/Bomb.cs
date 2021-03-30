using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float m_MaxLifeTime = 60f;
    public float m_MaxDamage = 68f;
    public float m_ExplosionRadius = 15f;
    public float m_ExplosionForce = 200f;

    public ParticleSystem m_ExplosionParticles;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }
    private void OnCollisionEnter(Collision other)
    {
        // find the rigidbody of the collision object
        Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();
        if (targetRigidbody.tag == "Enemy")
        {
            // only tanks will have rigidbody scripts
            if (targetRigidbody != null)
            {
                // Add an explosion force
                targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

                //find the TankHealth script associated with the rigidbody
                tankHealth targetHealth = targetRigidbody.GetComponent<tankHealth>();

                if (targetHealth != null)
                {
                    // calculate the amount of damge the target should take
                    // based on it's distance from the shell.
                    float damage = CalculateDamage(targetRigidbody.position);

                    // Deal this damage to the tank
                    targetHealth.TakeDamage(damage);
                }
            }

            // Unparent the particles from the shell
            m_ExplosionParticles.transform.parent = null;

            // Play the particle system
            m_ExplosionParticles.Play();

            // Once the particles have finished, destroy the gameObject they are on
            Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);

            // Destroy the shell
            Destroy(gameObject);
        }
    }
    private float CalculateDamage(Vector3 targetPosition)
    {
        // Create a vector from the shell to the target
        Vector3 explosionToTarget = targetPosition - transform.position;

        //Calculate the distance from the shell to he target
        float explosionDistance = explosionToTarget.magnitude;

        //Calculate the proportion of the maximum distance (the explosionRadius) the target is away
        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

        //Calculate damage as this proportion of the maximum possible damage
        float damage = relativeDistance * m_MaxDamage;

        // Make sure that the minimum damage is always 0
        damage = Mathf.Max(0f, damage);

        return damage;
    }
    public void Kill()
    {
        Destroy(gameObject);
    }
}
