using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider sliderHP;
    [SerializeField] public PlayerController playerController;

    void Start()
    {
        sliderHP.minValue = 0;
        sliderHP.maxValue = playerController.MaxHP;
    }

    void Update()
    {
        sliderHP.value = playerController.CurHP;
    }
}
