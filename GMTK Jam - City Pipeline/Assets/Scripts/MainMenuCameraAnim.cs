using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraAnim : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetLevelSelecting(false);
        }
    }

    public void SetLevelSelecting(bool set)
    {
        anim.SetBool("LevelSelecting", set);
    }
}
