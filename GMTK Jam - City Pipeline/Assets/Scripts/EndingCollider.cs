using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCollider : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponent<ConnectorCollider>() && other.transform.GetComponent<ConnectorCollider>().isWaterLogged)
        {
            Pipeline.Instance.endIsWaterlogged = true;
        }
    }
}
