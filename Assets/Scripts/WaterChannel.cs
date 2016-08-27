using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class WaterChannel : MonoBehaviour
{
    private static int _totalPercentage; 

    [SerializeField]
    [Range(0, 100)]
    private int _percentageOfWaterSource;
    public int TotalPercentOfWaterSource { get { return _percentageOfWaterSource; } }

    [SerializeField]
    private int _currentWaterLevel;
    
    private void Awake() {
        _totalPercentage = 0;
    }

    private void Start() {
        _totalPercentage += _percentageOfWaterSource;

        if (_totalPercentage > 100) {
            Debug.LogError("The total percentage of Water entries is great than 100%");
        }

        RefillWaterLevel();

        GetComponentInChildren<Text>().text = _percentageOfWaterSource + "%";
    }

    public void RefillWaterLevel() { // Refills the channel up to where it should be. 
        // NOTE: Should this build up? So if all the water wasn't used, then let it fill up more than it should?
        _currentWaterLevel = FindObjectOfType<LevelSettings>().WaterSourceAmount * (1 / _percentageOfWaterSource);
    }

    public int TakeWater(int amount) {
        if (amount > _currentWaterLevel) {
            SetWaterImageToEmpty(); // This will make it so it's obvious that all the water has been used
            return _currentWaterLevel;
        }

        _currentWaterLevel -= amount;
        return amount;
    }

    private void SetWaterImageToEmpty() {   
        // Will need a separate water image where there is no water left
    }
}