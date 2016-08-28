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
    private float _currentWaterLevel;

    [SerializeField]
    private Text _percentageDisplay;

    [SerializeField]
    private Sprite _dryTexture;
    [SerializeField]
    private Sprite _filledTexture;

    private void Awake() {
        _totalPercentage = 0;
    }

    private void Start() {
        _totalPercentage += _percentageOfWaterSource;

        if (_totalPercentage > 100) {
            Debug.LogError("The total percentage of Water entries is great than 100%");
        }

        RefillWaterLevel();

        _percentageDisplay = GetComponentInChildren<Text>();
        UpdatePercentageDisplay();
    }

    public void RefillWaterLevel() { // Refills the channel up to where it should be. 
        // NOTE: Should this build up? So if all the water wasn't used, then let it fill up more than it should?
        _currentWaterLevel = FindObjectOfType<LevelSettings>().WaterSourceAmount * (_percentageOfWaterSource / 100.0f);

        SetWaterImagesTo(_filledTexture);
    }

    public float TakeWater(float amount) {
        if (amount > _currentWaterLevel) {
            SetWaterImagesTo(_dryTexture);
            _currentWaterLevel = 0;
            UpdatePercentageDisplay();
            return _currentWaterLevel;
        }

        _currentWaterLevel -= amount;

        UpdatePercentageDisplay();
        return amount;
    }

    private void SetWaterImagesTo(Sprite targetSprite) {
        var waterTilesTransform = transform.FindChild("WaterTiles");

        for (int i = 0; i < waterTilesTransform.childCount; i++) {
            waterTilesTransform.GetChild(i).GetComponent<SpriteRenderer>().sprite = targetSprite;
        }
    }
    
    private void UpdatePercentageDisplay() {
        var totalWater = FindObjectOfType<LevelSettings>().WaterSourceAmount * (_percentageOfWaterSource / 100.0f);

        _percentageDisplay.text = ((_currentWaterLevel / totalWater) * 100) * (_percentageOfWaterSource / 100.0f) + "% Of " + _percentageOfWaterSource + "%";
    }
}