using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollisionEvents : MonoBehaviour
{
    public void OnEnter(Collider collider)
    {
        Debug.Log("ENTER");
    }

    public void OnStay(Collider collider)
    {
        Debug.Log("STAY");
    }

    public void OnExit(Collider collider)
    {
        Debug.Log("EXIT");
    }
    
    public void OnEnter(Collider2D collider)
    {
        Debug.Log("ENTER");
    }

    public void OnStay(Collider2D collider)
    {
        Debug.Log("STAY");
    }

    public void OnExit(Collider2D collider)
    {
        Debug.Log("EXIT");
    }
}
