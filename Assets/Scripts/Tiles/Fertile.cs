using UnityEngine;
using System.Collections.Generic;

public class Fertile : Tile
{
    [SerializeField]
    private float _baseFetilityLevel;

    [SerializeField]
    private float _fertilityModifiers;

    [SerializeField]
    public float _burntStatus; //1 or 0 not bool because need for math (less if statements for you)

    [SerializeField]
    public float _hoeStatus; //1 or 0 not bool because need for math (less if statements for you)

    [SerializeField]
    public float _ploughStatus;//1 or 0 not bool because need for math (less if statements for you)

    [SerializeField]
    public float _irrigationStatus;//1 or 0 not bool because need for math (less if statements for you)

    [SerializeField]
    public float _aqueductStatus;//1 or 0 not bool because need for math (less if statements for you)

    [SerializeField]
    public float _cropRotationStatus = 1 ;//1 or 0 not bool because need for math (less if statements for you)

    [SerializeField]
    private string _previousCrop;

    [SerializeField]
    private string _currentCrop;

    private WaterChannel _waterSource;

    private int _waterLevel;

    private void Start() {
        CheckforWaterSource();
        checkCropRotation();
        _fertilityModifiers = (_burntStatus * 0.25f + _hoeStatus * 0.05f + _ploughStatus * 0.10f + _irrigationStatus * 0.15f + _aqueductStatus * 0.20f)*_cropRotationStatus;
       // string  test = "hoeStatus";
       // int value = 1;
        //changeModifiers(test, value);
        //VALUES NEED INVESTIGATING
    }

    public float getFertilityModifier()
    {
        return _fertilityModifiers;
    }
    public void changeModifiers(string status, int value){ 
        //external editting of modifiers (tech tree stuff so i'm clueless as to where exactly you want this)
        if (status.Equals("hoeStatus"))
        {
            _hoeStatus = 1;
            // Debug.LogError("hoeStatus is 1");
        }
        else if (status.Equals("ploughStatus"))
        {
            _ploughStatus = 1;
        }
        else if (status.Equals("irrigationStatus"))
        {
            _irrigationStatus = 1;
        }
        else if (status.Equals("aqueductStatus"))
        {
            _aqueductStatus = 1;
        }
        else if (status.Equals("burntStatus"))
        {
            _burntStatus = 1;
        }
    }
    //Here's the if statements you know and love
    
        public void toggleCropRotation()
    {
        _previousCrop = _currentCrop;
    }

    public void setCurrentCrop(string cropName)
    {
        _currentCrop = cropName;
    }

    private void checkCropRotation()
    {
        if (_previousCrop != null)
        {
            if (_currentCrop.Equals(_previousCrop))
            {
                _cropRotationStatus *= 0.9f;
            }
            else
            {
                _cropRotationStatus = 1;
            }
        }
    }
    
    private void CheckforWaterSource() {
        // Up
        var raycast = Physics2D.Raycast(transform.position + (transform.up * 1.5f), Vector2.zero);

        if (raycast.collider != null)
            _waterSource = raycast.transform.GetComponent<WaterChannel>();

        // Down
        raycast = Physics2D.Raycast(transform.position - (transform.up / 2), Vector2.zero);

        if (raycast.collider != null)
            _waterSource = raycast.transform.GetComponent<WaterChannel>();
    }
    public int getFertileWaterPercentage()
    {
        return _waterSource.getPercentage();
    }
}