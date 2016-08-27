using UnityEngine;
using System.Collections.Generic;

public class WaterChannel : MonoBehaviour
{
    private static int _totalPercentage; 

    [SerializeField]
    [Range(0, 100)]
    private int _percentageOfWaterSource;

    private void Awake() {
        _totalPercentage = 0;
    }

    private void Start() {
        _totalPercentage += _percentageOfWaterSource;

        if (_totalPercentage > 100) {
            Debug.LogError("The total percentage of Water entries is great than 100%");
        }
    }
}