using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public Transform[] m_spawnPoints;
    public GameObject m_powerUpPrefab;
    public float m_timer;

    private GameObject m_currentInstance;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    //This is a constant loop
    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_timer);
            if (m_currentInstance)
            {
                Destroy(m_currentInstance);
            }
            int randomIndex = Random.Range(0, m_spawnPoints.Length);
            Vector3 position = m_spawnPoints[randomIndex].position;
            Quaternion rotation = Quaternion.Euler(new Vector3(0,0,0));
            m_currentInstance = Instantiate(m_powerUpPrefab, position, rotation) as GameObject;
        }
    }
}
