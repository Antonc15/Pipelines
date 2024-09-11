using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPipe : MonoBehaviour
{

    [SerializeField] private ConnectionType type;

    private void Start()
    {
        Pipeline.Instance.SetTilePos(gameObject, transform.localPosition / 1.28f, type);
    }
}
