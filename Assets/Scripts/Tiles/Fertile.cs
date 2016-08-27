using UnityEngine;
using System.Collections.Generic;

public class Fertile : Tile
{
    [SerializeField]
    private float _baseFetilityLevel;

    [SerializeField]
    private float fertilityModifiers;
    
    private Plant _activePlant;

    public bool HasPlant { get { return _activePlant != null; } }

    
}