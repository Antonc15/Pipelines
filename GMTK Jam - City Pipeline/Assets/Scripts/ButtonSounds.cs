using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    [SerializeField] private GameObject hoverSound;
    [SerializeField] private GameObject clickSound;
    [SerializeField] private GameObject startGameSound;

    public void ButtonHover()
    {
        AudioHandler.Instance.MakeSound(hoverSound, transform.position, 1);
    }

    public void ButtonClick()
    {
        AudioHandler.Instance.MakeSound(clickSound, transform.position, 1);
    }

    public void StartGame()
    {
        AudioHandler.Instance.MakeSound(startGameSound, transform.position, 1);
    }
}
