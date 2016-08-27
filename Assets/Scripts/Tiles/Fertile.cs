using UnityEngine;
using System.Collections.Generic;

public class Fertile : Tile
{
    [SerializeField]
    private float _baseFetilityLevel;

    [SerializeField]
    private double _fertilityModifiers;

    [SerializeField]
    public int _burntField; //1 or 0 not bool because need for math (less if statements for you)

    [SerializeField]
    public int _hoeStatus; //1 or 0 not bool because need for math (less if statements for you)

    [SerializeField]
    public int _ploughStatus;//1 or 0 not bool because need for math (less if statements for you)

    [SerializeField]
    public int _irrigationStatus;//1 or 0 not bool because need for math (less if statements for you)

    [SerializeField]
    public int _aqueductStatus;//1 or 0 not bool because need for math (less if statements for you)

    private WaterChannel _waterSource;

    private int _waterLevel;

    public enum FerTileStatus {
        hoeStatus,
        burntStatus,
        ploughStatus,
        irrigationStatus,
        aqueductStatus,
    }
    private void Start() {
        CheckforWaterSource();
        _fertilityModifiers = _burntField * 0.25 + _hoeStatus * 0.05 + _ploughStatus * 0.10 + _irrigationStatus * 0.15 + _aqueductStatus * 0.20;
        string  test = "hoeStatus";
        int value = 1;
        changeModifiers(test, value);
    }
    public void changeModifiers(string status, int value){
        if (status.Equals("hoeStatus"))
        {
            _hoeStatus = 1;
            Debug.LogError("hoeStatus is 1");
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