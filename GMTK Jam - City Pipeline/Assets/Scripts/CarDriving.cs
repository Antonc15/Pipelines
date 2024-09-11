using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarDriving : MonoBehaviour
{
    [SerializeField] private Color32[] colors;
    [SerializeField] private GameObject car;

    private float currentTimer = 0f;


    private void Update()
    {
        if(Time.time > currentTimer)
        {

            currentTimer = Time.time + Random.Range(0.1f, 0.5f);

            GameObject tempCar = Instantiate(car, transform.position, Quaternion.identity, transform);
            SpriteRenderer carImg = tempCar.GetComponent<SpriteRenderer>();

            carImg.color = colors[Random.Range(0, colors.Length)];
        }
    }
}
