using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float m_DampTime = 0.2f;
    public Transform m_target;

    private Vector3 m_MoveVelocity;
    private Vector3 m_DesiredPosition;
    private Quaternion m_DesiredRotation;

    private void Awake()
    {
        m_target = GameObject.FindGameObjectWithTag("CameraLockOn").transform;
    }
    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0 && Camera.main.fieldOfView >= 0)
        {
            Camera.main.fieldOfView += scroll * -10;
        }
        else if (Camera.main.fieldOfView <= 34)
        {
            Camera.main.fieldOfView += 1;
        }
        else if (Camera.main.fieldOfView >= 121)
        {
            Camera.main.fieldOfView -= 1;
        }
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        m_DesiredPosition = m_target.position;
        m_DesiredRotation = m_target.rotation;
        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, m_DesiredRotation, 360);
    }
}
