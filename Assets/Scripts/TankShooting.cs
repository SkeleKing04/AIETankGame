using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour
{
    // Prefab of the Shell
    public Rigidbody m_Shell;
    public Rigidbody m_bomb;
    // A child of the tank where the shells are spawned
    public Transform m_FireTransform;
    public Transform m_DropTransform;
    // The force given to the shell when firing
    public float m_LaunchForce = 30f;
    public int m_bombCount = 3;
    // Update is called once per frame
    private void Update()
    {
        // TODO: Later on, we'll check with the 'Game Manager' to make
        // sure the game isn't over
        if (Input.GetButtonUp("Fire1"))
        {
            Fire();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            bombDrop();
        }
    }
    private void Fire()
    {
        // Create an instance of the shell and store a reference to its rigidbody
        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        // Set the shell's velocity to the launch force in the fire
        // position's forward direction
        shellInstance.velocity = m_LaunchForce * m_FireTransform.forward;
    }
    private void bombDrop()
    {
        if (m_bombCount >= 1)
        {
            m_bombCount--;
            Rigidbody shellInstance = Instantiate(m_bomb, m_DropTransform.position, m_DropTransform.rotation) as Rigidbody;
            shellInstance.velocity = (m_LaunchForce / 5) * (m_DropTransform.up * -1);
        }
    }
}
