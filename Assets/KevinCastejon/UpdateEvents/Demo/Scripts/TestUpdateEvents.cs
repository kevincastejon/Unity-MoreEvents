using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUpdateEvents : MonoBehaviour
{
    public void OnUpdate(float deltaTime)
    {
        Debug.Log("Update");
    }

    public void OnFixedUpdate(float deltaTime)
    {
        Debug.Log("FixedUpdate");
    }

    public void OnLateUpdate(float deltaTime)
    {
        Debug.Log("LateUpdate");
    }
}
