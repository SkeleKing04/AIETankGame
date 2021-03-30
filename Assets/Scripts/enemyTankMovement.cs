using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyTankMovement : MonoBehaviour
{
    public float m_CloseDistance = 8f;
    public Transform m_Turret;
    public List<GameObject> _waypointsList = new List<GameObject>();
    private int currentWaypoint;
    private GameObject m_Player;
    private NavMeshAgent m_NavAgent;
    private Rigidbody m_Rigidbody;
    private bool m_Follow;
    // Start is called before the first frame update
    private void Awake()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_Rigidbody = GetComponent<Rigidbody>();
        _waypointsList.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
        m_Follow = false;
    }
    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
    }
    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Follow = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_Follow = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (m_Follow == false)
        {
            if (_waypointsList.Count <= 0)
            {
                return;
            }
            if  (currentWaypoint < _waypointsList.Count)
            {
                if (Vector3.Distance(transform.position, _waypointsList[currentWaypoint].transform.position) > 10)
                {
                    m_NavAgent.SetDestination(_waypointsList[currentWaypoint].transform.position);
                }
                else
                {
                    currentWaypoint++;
                }
            }
            else
            {
                currentWaypoint = 0;
            }
        }
        else
        {
            float distance = (m_Player.transform.position - transform.position).magnitude;
            if (distance > m_CloseDistance)
            {
                m_NavAgent.SetDestination(m_Player.transform.position * Time.deltaTime);
                m_NavAgent.isStopped = false;
            }
            else
            {
                m_NavAgent.isStopped = true;
            }

            if (m_Turret != null)
            {
                m_Turret.LookAt(m_Player.transform);
            }
        }
    }
}
