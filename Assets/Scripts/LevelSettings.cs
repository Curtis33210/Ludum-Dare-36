using UnityEngine;
using System.Collections.Generic;

public class LevelSettings : MonoBehaviour
{
    [SerializeField]
    private int _waterSourceAmount;

    [SerializeField]
    private GrowthMethods _growthMethod;

    [SerializeField]
    private PlantingToolmodifiers _plantingTool;

    [SerializeField]
    private HarvestingToolModifiers _harvestingTool;

    [SerializeField]
    private int _maxLabourForce;

    private int _currentLabourForce;

    private enum PlantingToolmodifiers
    {
        None,
        Hoe,
        Plough,
        HorsePlough
    }

    private enum HarvestingToolModifiers
    {
        None,
        Sickle,
        Scythe,
        Shovel,
        HandAxe,
        Knife,
    }


    private enum GrowthMethods
    {   
        None,
        Irrigation,
        Acqueduct,
        Hydroponics,
    }

    public int WaterSourceAmount { get { return _waterSourceAmount; } }

}