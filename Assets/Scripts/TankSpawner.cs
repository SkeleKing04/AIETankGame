using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    public Transform[] m_spawnPoints;
    public GameObject m_enemyTankPrefab;
    public float m_timer;
    private GameObject m_newTank;

    // Start is called before the first frame update
    public void spawnTank()
    {
        int i = Random.Range(1, 3);
        while (i >= 1)
        {
            i--;
            int randomIndex = Random.Range(0, m_spawnPoints.Length);
            Vector3 position = m_spawnPoints[randomIndex].position;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            m_newTank = Instantiate(m_enemyTankPrefab, position, rotation) as GameObject;
            GameManager addTank = Object.FindObjectOfType<GameManager>();
            addTank.m_Tanks.Add(m_newTank);

        }
    }
}
