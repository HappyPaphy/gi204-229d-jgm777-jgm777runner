using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Collectible
{
    HealthItem,
    BoatPart
};

public class CollectibleManager : MonoBehaviour
{
    [SerializeField] private int num_BoatPart;
    public int Num_BoatPart { get { return num_BoatPart; } set { num_BoatPart = value; } }

    [SerializeField] private float curHP;
    public float CurHP { get { return curHP; } }

    [SerializeField] private float maxHP;
    public float MaxHP { get { return maxHP; } }

    [SerializeField] private Text text_NumBoatPart;

    public static CollectibleManager instance;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        text_NumBoatPart.text = $"{num_BoatPart}";
    }
}
