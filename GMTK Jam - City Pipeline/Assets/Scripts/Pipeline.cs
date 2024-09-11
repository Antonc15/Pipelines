using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pipeline : MonoBehaviour
{

    #region Singleton
    private static Pipeline _instance;

    public static Pipeline Instance { get { return _instance; } }

    private void Awake()
    {
            _instance = this;
    }
    #endregion

    public PipePath startingPipe;
    public List<PipePos> pipePos = new List<PipePos>();
    public Transform selectedPipe;
    public bool endIsWaterlogged = false;
    public bool pipelineInitiated = false;
    public bool exposedPipe = false;

    [Header("UI")]
    public TextMeshProUGUI counterText;

    [Header("Difficulty")]
    [SerializeField] private int maxMovesAllowed = 10;
    public int currentMovesUsed;

    [Header("Scenes")]
    [SerializeField] private string currentSceneName;
    [SerializeField] private string nextSceneName;    

    [Header("Audio")]
    [SerializeField] private GameObject winSound;
    [SerializeField] private GameObject loseSound;
    [SerializeField] private GameObject waterRun;

    bool hasWon = false;
    bool hasLost = false;

    private float timer = 2f;

    private void Update()
    {
        if (endIsWaterlogged && !hasWon && !exposedPipe)
        {
            hasWon = true;
            StartCoroutine(Victory());
        }

        if (pipelineInitiated && selectedPipe != null)
        {
            selectedPipe = null;
        }

        if((pipelineInitiated && !hasWon && timer < Time.time && !hasLost) || (exposedPipe && !hasLost))
        {
            hasLost = true;
            StartCoroutine(Defeat());
        }

        if(currentMovesUsed > maxMovesAllowed && !hasLost)
        {
            pipelineInitiated = true;
            hasLost = true;
            StartCoroutine(Defeat());
        }

        CounterText();

    }

    private void CounterText()
    {
        counterText.text = string.Format("Moves: {0}", maxMovesAllowed - currentMovesUsed);
    }

    IEnumerator Victory()
    {
        AudioHandler.Instance.MakeSound(winSound, transform.position, 1);
        Debug.Log("Victory!!!");

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }

    IEnumerator Defeat()
    {
        AudioHandler.Instance.MakeSound(loseSound, transform.position, 1);
        Debug.Log("Lose!!!");

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(currentSceneName, LoadSceneMode.Single);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(currentSceneName, LoadSceneMode.Single);
    }

    public void SetSelectedPipe(Transform _pipe)
    {
        if (pipelineInitiated) { return; }

        selectedPipe = _pipe;

        if (selectedPipe != null)
        {
            DisplayControls();
        }
    }

    public void SetTilePos(GameObject _pipe, Vector3 _pos, ConnectionType type)
    {
        for (int i = 0; i < pipePos.Count; i++)
        {
            if (pipePos[i].pipe == _pipe)
            {
                pipePos[i].pos = _pos;
                return;
            }
        }

        PipePos newPipe = new PipePos();

        newPipe.pipe = _pipe;
        newPipe.pos = _pos;

        pipePos.Add(newPipe);
    }

    public void InitiatePipeline()
    {
        timer += Time.time;
        pipelineInitiated = true;
        AudioHandler.Instance.MakeSound(waterRun, transform.position, 1);
        startingPipe.SetWaterLogged();
    }

    public bool TileIsAvailable(Vector2 _pos)
    {
        for (int i = 0; i < pipePos.Count; i++)
        {
            if (pipePos[i].pos == _pos)
            {
                return false;
            }
        }

        return true;
    }

    private void DisplayControls()
    {
        if (selectedPipe.GetComponent<PivotPipe>())
        {
            selectedPipe.GetComponent<PivotPipe>().controlsOpen = true;
            selectedPipe.GetComponent<PivotPipe>().pipeControls.SetActive(true);
        }

        if (selectedPipe.GetComponent<PipeController>())
        {
            Debug.Log("s");
            selectedPipe.GetComponent<PipeController>().controlsOpen = true;
            selectedPipe.GetComponent<PipeController>().pipeControls.SetActive(true);
        }
    }

    [System.Serializable]
    public class PipePos
    {
        public ConnectionType type;
        public GameObject pipe;
        public Vector2 pos;
    }
}

[System.Serializable]
public enum ConnectionType
{
    Start,
    End,
    Normal
}
