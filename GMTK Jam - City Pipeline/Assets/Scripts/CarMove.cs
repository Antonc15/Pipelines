using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        transform.Translate(10 * Time.deltaTime,0,0);
    }
}
