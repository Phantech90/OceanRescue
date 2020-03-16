using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescueBoat : MonoBehaviour
{

    public GameObject m_passenger = null;
    private float m_timer = 0;
    public float m_rescueTime = 3;
    public float m_dropoffTime = 3;

    private bool m_hasPassenger = false;

    private void Start()
    {
        m_passenger.SetActive(false);
        m_hasPassenger = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        //if you get near a swimmer and you dont have a passenger the timer is reset
        if (collider.gameObject.tag == "Swimmer" && m_hasPassenger == false)
        {
            m_timer = 0;
        }
        //if you enter the dropzone and you have a passenger then the timer is reset
        else if (collider.gameObject.tag == "DropZone" && m_hasPassenger == true)
        {
            m_timer = 0;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Swimmer" && m_hasPassenger == false)
        {
            m_timer += Time.deltaTime;
            Debug.Log((m_rescueTime / m_timer));
            if (m_timer >= m_rescueTime)
            {
                PickupSwimmer(collider.gameObject);
            }
        }
        else if (collider.gameObject.tag == "DropZone" && m_hasPassenger == true)
        {
            m_timer += Time.deltaTime;
            if (m_timer >= m_dropoffTime)
            {
                DropoffSwimmer();
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        m_timer = 0;
    }

    public void PickupSwimmer(GameObject swimmer)
    {
        swimmer.SetActive(false);
        m_passenger.SetActive(true);
        m_hasPassenger = true;
    }

    public void DropoffSwimmer()
    {
        m_passenger.SetActive(false);
        m_hasPassenger = false;
    }
}
