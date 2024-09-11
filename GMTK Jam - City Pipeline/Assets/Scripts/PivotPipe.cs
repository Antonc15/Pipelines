using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotPipe : MonoBehaviour
{
    public bool controlsOpen = false;
    public GameObject pipeControls;

    [Header("Audio")]
    [SerializeField] private GameObject rotateSound;
    [SerializeField] private float maxPitch;
    [SerializeField] private float minPitch;

    private Vector3 targetPos;

    private void Start()
    {
        targetPos = transform.localPosition;

        Pipeline.Instance.SetTilePos(gameObject, targetPos / 1.28f, ConnectionType.Normal);
    }

    private void Update()
    {
        HidePipeControls();
        RotateInput();
    }

    private void OnMouseDown()
    {
        if (Pipeline.Instance.selectedPipe == transform)
        {
            Pipeline.Instance.SetSelectedPipe(null);
        }
        else
        {
            Pipeline.Instance.SetSelectedPipe(transform);
        }
    }

    private void HidePipeControls()
    {
        if (Pipeline.Instance.selectedPipe == transform) { return; }

        controlsOpen = false;
        pipeControls.SetActive(false);
    }

    private void RotateInput()
    {
        if (!controlsOpen) { return; };

        if (Input.GetKeyDown(KeyCode.R))
        {
            AudioHandler.Instance.MakeSound(rotateSound, transform.position, Random.Range(minPitch, maxPitch));

            transform.Rotate(0, 0, 90);
        }
    }
}
