using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKeyOrButtonEvents : MonoBehaviour
{
    public void OnDown()
    {
        Debug.Log("Down");
    }

    public void OnUp()
    {
        Debug.Log("Up");
    }
}
