using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVisibleEvents : MonoBehaviour
{
    public void OnVisible()
    {
        Debug.Log("Visible");
    }

    public void OnInvisible()
    {
        Debug.Log("Invisible");
    }

    public void OnChange(bool visible)
    {
        Debug.Log("Visible state changed to " + visible);
    }
}
