using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 7f;
    [SerializeField] private float tileSize;

    public bool controlsOpen = false;
    public GameObject pipeControls;

    [Header("Audio")]
    [SerializeField] private GameObject moveSound;
    [SerializeField] private GameObject rotateSound;
    [SerializeField] private float maxPitch;
    [SerializeField] private float minPitch;

    private Vector3 targetPos;
    bool hasMoved = false;

    private void Start()
    {
        targetPos = transform.localPosition;

        Pipeline.Instance.SetTilePos(gameObject, targetPos / 1.28f, ConnectionType.Normal);
    }

    private void Update()
    {
        HidePipeControls();

        MoveInput();
        RotateInput();
        LerpPipe();
    }

    private void OnMouseDown()
    {
        if(Pipeline.Instance.selectedPipe == transform)
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
        if(Pipeline.Instance.selectedPipe == transform) { return; }

        controlsOpen = false;
        pipeControls.SetActive(false);
    }

    private void MoveInput()
    {
        Vector3 lastPos = targetPos;

        if (!controlsOpen) { return; };

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            hasMoved = true;
            targetPos += new Vector3(0,1 * tileSize,0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            hasMoved = true;
            targetPos += new Vector3(-1 * tileSize, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            hasMoved = true;
            targetPos += new Vector3(0, -1 * tileSize, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            hasMoved = true;
            targetPos += new Vector3(1 * tileSize, 0, 0);
        }

        if (hasMoved)
        {
            hasMoved = false;

            if (!Pipeline.Instance.TileIsAvailable(targetPos / 1.28f))
            {
                targetPos = lastPos;
            }
            else
            {
                Pipeline.Instance.currentMovesUsed++;
                AudioHandler.Instance.MakeSound(moveSound, transform.position, Random.Range(minPitch, maxPitch));
            }

            Pipeline.Instance.SetTilePos(gameObject, targetPos / 1.28f, ConnectionType.Normal);
        }

    }

    private void RotateInput()
    {
        if (!controlsOpen) { return; };

        if (Input.GetKeyDown(KeyCode.R))
        {
            AudioHandler.Instance.MakeSound(rotateSound, transform.position, Random.Range(minPitch, maxPitch));

            transform.Rotate(0,0,90);
        }
    }

    private void LerpPipe()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * lerpSpeed);
    }
}
