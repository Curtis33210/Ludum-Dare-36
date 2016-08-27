using UnityEngine;
using System.Collections.Generic;

public class Fertile : Tile
{
    [SerializeField]
    private float _baseFetilityLevel;

    [SerializeField]
    private float fertilityModifiers;

    private WaterChannel _waterSource;

    private int _waterLevel;
    
    private void Start() {
        CheckforWaterSource();
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

        Debug.Log(_waterSource.name);
    }
}