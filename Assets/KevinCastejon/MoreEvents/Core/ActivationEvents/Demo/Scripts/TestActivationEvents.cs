using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestActivationEvents : MonoBehaviour
{
    public void OnActivation()
    {
        Debug.Log("Activated");
    }

    public void OnDeactivation()
    {
        Debug.Log("Deactivated");
    }

    public void OnChange(bool activated)
    {
        Debug.Log("Activation state changed to " + activated);
    }
}
