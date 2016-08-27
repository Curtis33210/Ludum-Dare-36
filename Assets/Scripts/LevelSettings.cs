using UnityEngine;
using System.Collections.Generic;

public class LevelSettings : MonoBehaviour
{
    [SerializeField]
    private int _waterSourceAmount;
    public int WaterSourceAmount { get { return _waterSourceAmount; } }

}