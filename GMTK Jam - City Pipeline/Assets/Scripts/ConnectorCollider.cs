using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorCollider : MonoBehaviour
{

    public PipePath pipePath;
    public bool isWaterLogged = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.GetComponent<ConnectorCollider>() && !isWaterLogged && other.transform.GetComponent<ConnectorCollider>().isWaterLogged)
        {
            pipePath.SetWaterLogged();
        }
    }

    public void HasCollidedWithAnything()
    {

        GetComponent<SphereCollider>().enabled = false;
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, 0.2f);
        GetComponent<SphereCollider>().enabled = true;

        if (hitCollider.Length < 1)
        {
            Pipeline.Instance.exposedPipe = true;
        }
    }
}
