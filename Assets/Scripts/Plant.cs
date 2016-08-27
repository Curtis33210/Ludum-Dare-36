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
    private Plant _plant;
    [SerializeField]
    private GrowthStatus _currentStatus;

    [SerializeField]
    private int _maturityTime; // How many seasons it takes to grow

    [SerializeField]
    private float _totalPercentageGrown; // This will determine how much is harvestable

    [SerializeField]
    private float _fertilityModifier; //The external affects acting upon the plant
    //This is presumable a value from 0-(1 for example) i.e. 0.05 is +5% fertility

    [SerializeField]
    private int _harvestModifier; 
    //The intrinsic worth that the plant is "worth" economically (i.e. with respect to the scoring system
//_harvestModifier is going to need some mad tinkering depending on what value we want to put on plants
//PS... for the mean time _harvestModifier is presumed to be some percentage above 100%.... i.e. 1.05 for +5% _harvest bonus
//NEED TO DO!!! Plant Values

    [SerializeField]
    private int _waterRequirement;


    private Fertile _occupiedTile;

    public void Start()
    {

        var _raycast = Physics2D.Raycast(Vector2.zero, Vector2.zero);
        if (_raycast.collider != null)
        {
            _occupiedTile = _raycast.transform.GetComponent<Fertile>();
            _occupiedTile.toggleCropRotation();
            _occupiedTile.setCurrentCrop(_plant.name);
        }
        setFertilityModifier(_occupiedTile);
        checkFerTileWaterSupply();
    }
    
    private void setFertilityModifier(Fertile _ferTile)
    {
        _fertilityModifier = _ferTile.getFertilityModifier();
    }
    public float HarvestPlant() {
        //TODO: Fix the return amount (I think I fixed it.....)

        return _totalPercentageGrown * _maturityTime / (_harvestModifier - _fertilityModifier); // Probably some base multiplier and specific multipler per plant type?
    }

    public bool GrowPlant(int seasonsPast) {
        //TODO: Check the algorithm for plant growth percentage (checked and reasy Cap'n)
        var percentGrown = (seasonsPast*(1+_fertilityModifier))/ _maturityTime;

        _totalPercentageGrown = Mathf.Clamp(_totalPercentageGrown + percentGrown, 0, 100);

        return _totalPercentageGrown == 100;
    }
    public void checkFerTileWaterSupply()
    {
        _fertilityModifier *= _occupiedTile.getFertileWaterPercentage() / _waterRequirement;
    }
}