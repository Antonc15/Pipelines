using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePath : MonoBehaviour
{
    [SerializeField] private ConnectorCollider[] colliders;

    private void Awake()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<ConnectorCollider>().pipePath = this;
        }
    }

    public void SetWaterLogged()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].isWaterLogged = true;
            colliders[i].HasCollidedWithAnything();
        }
    }
}
