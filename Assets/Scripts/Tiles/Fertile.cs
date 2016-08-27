using UnityEngine;
using System.Collections.Generic;

public enum FertileStatus
{
    BurntStatus,
    HoeStatus,
    PloughStatus,
    IrrigationStatus,
    AqueductStatus,
    //CropRotationStatus, // Not a huge fan of this.
    Length = AqueductStatus + 1
}

public class Fertile : Tile
{
    [SerializeField]
    private float _baseFetilityLevel;

    [SerializeField]
    private float _fertilityModifier;

    [SerializeField]
    private int[] _statusModifiers;

    [SerializeField]
    private Plant _previousCrop;

    [SerializeField]
    private Plant _currentCrop;

    private WaterChannel _waterSource;

    [SerializeField]
    private int _targetWaterAbsorbtion;

    [SerializeField]
    private int _currentWaterLevel;

    private void Awake() {
        CheckforWaterSource();
    }

    private void Start() {
        _statusModifiers = new int[(int)FertileStatus.Length];
        //checkCropRotation();
        _fertilityModifier = (_statusModifiers[(int)FertileStatus.BurntStatus] * 0.25f + _statusModifiers[(int)FertileStatus.HoeStatus] * 0.05f + _statusModifiers[(int)FertileStatus.BurntStatus] * 0.10f +
                            _statusModifiers[(int)FertileStatus.IrrigationStatus] * 0.15f + _statusModifiers[(int)FertileStatus.AqueductStatus] * 0.20f)* _statusModifiers[(int)FertileStatus.AqueductStatus];

       // string  test = "hoeStatus";
       // int value = 1;
        //changeModifiers(test, value);
        //VALUES NEED INVESTIGATING
    }

    public float getFertilityModifier()
    {
        return _fertilityModifier;
    }
    public void changeModifiers(FertileStatus status, int value){
        _statusModifiers[(int)status] = value;
    }
    //Here's the if statements you know and love

    public void toggleCropRotation() {
        _previousCrop = _currentCrop;
    }

    public void setCurrentCrop(Plant cropType)
    {
        _currentCrop = cropType;
    }

    //private void checkCropRotation()
    //{
    //    if (_previousCrop != null)
    //    {
    //        if (_currentCrop.Equals(_previousCrop))
    //        {
    //            _cropRotationStatus *= 0.9f;
    //        }
    //        else
    //        {
    //            _cropRotationStatus = 1;
    //        }
    //    }
    //}
    
    private void CheckforWaterSource() {
        // Up
        var raycast = Physics2D.Raycast(transform.position + (transform.up * 1.5f), Vector2.zero);

        if (raycast.collider != null)
            _waterSource = raycast.transform.GetComponent<WaterChannel>();

        // Down
        raycast = Physics2D.Raycast(transform.position - (transform.up / 2), Vector2.zero);

        if (raycast.collider != null)
            _waterSource = raycast.transform.GetComponent<WaterChannel>();

        Debug.Log(_waterSource);
    }

    private void Updater() {
        if (_currentWaterLevel < _targetWaterAbsorbtion) {
            AbsorbWater();
        }
    }

    public int TakeWater(int amount) { // Don't like this naming. This is for plants to take water from the tile
        if (amount > _currentWaterLevel) {
            return _currentWaterLevel;
        }

        _currentWaterLevel -= amount;
        return amount;
    }

    public void AbsorbWater() { // This is for the fertile to absorb water from the water source
        _waterSource.TakeWater(_targetWaterAbsorbtion - _currentWaterLevel); // Fills it back up to the target amount (Difference between target and current level)
    }
}