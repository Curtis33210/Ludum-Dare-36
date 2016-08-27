using UnityEngine;
using System.Collections.Generic;

public enum GrowthStatus
{
    Preparing,
    Growing,
    Harvesting,
}

public class Plant : MonoBehaviour
{
    [SerializeField]
    private GrowthStatus _currentStatus;

    [SerializeField]
    private int _growthRate; // How many seasons it takes to grow

    [SerializeField]
    private int _totalPercentageGrown; // This will determine how much is harvestable

    public int HarvestPlant() {
        //TODO: Fix the return amount

        return _totalPercentageGrown * _growthRate; // Probably some base multiplier and specific multipler per plant type?
    }

    public bool GrowPlant(int seasonsPast) {
        //TODO: Check the algorithm for plant growth percentage
        var percentGrown = seasonsPast / _growthRate;

        _totalPercentageGrown = Mathf.Clamp(_totalPercentageGrown + percentGrown, 0, 100);

        return _totalPercentageGrown == 100;
    }
}