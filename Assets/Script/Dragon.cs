using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public static Dragon MainInstance = null;

    private void Awake()
    {
        MainInstance = this;
    }

    private void OnDestroy()
    {
        MainInstance = null;
    }
}
