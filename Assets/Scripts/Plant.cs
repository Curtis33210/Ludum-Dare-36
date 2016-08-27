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
    //***************************************************************************************************
    //****************************************************************************************************
    //AM I aLLowed to do this?1?!?!!?!?!?!?!?!?!!?????????????????????
    //***************************************************************************************************
    //****************************************************************************************************

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
    //Note: Do not let this equal zero.


    private Fertile _occupiedTile;

    private Plant _adjacentPlant;

    public void Start()
    {

        var _raycastFerTile = Physics2D.Raycast(Vector2.zero, Vector2.zero);
        if (_raycastFerTile.collider != null)
        {
            _occupiedTile = _raycastFerTile.transform.GetComponent<Fertile>();
            _occupiedTile.toggleCropRotation();
            _occupiedTile.setCurrentCrop(_plant.name);
        }
        setFertilityModifier(_occupiedTile);
        checkFerTileWaterSupply(); //Note this order of function matters.
       
    }
    private void beanMaizeMultiplyer() //the bean maize phenomenon
    {
        if (_plant.name.Equals("Bean")){
            //left
            var _raycastMaize = Physics2D.Raycast(transform.position+transform.right *-0.5f, Vector2.zero);
            if (_raycastMaize.collider != null)
            {
                _adjacentPlant = _raycastMaize.transform.GetComponent<Plant>();
            }
            //right
            _raycastMaize = Physics2D.Raycast(transform.position + transform.right * 1.5f, Vector2.zero);
            if (_raycastMaize.collider != null)
            {
                _adjacentPlant = _raycastMaize.transform.GetComponent<Plant>();
            }
            if (_adjacentPlant.name.Equals("Maize"))
            {
                _fertilityModifier += 0.25f;
            }
        }
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
        //TODO: Check the algorithm for plant growth percentage (checked and ready Cap'n)
        var percentGrown = (seasonsPast*(1+_fertilityModifier))/ _maturityTime;

        _totalPercentageGrown = Mathf.Clamp(_totalPercentageGrown + percentGrown, 0, 100);

        return _totalPercentageGrown == 100;
    }
    public void checkFerTileWaterSupply() // Check for water supply Note This really needs work since multiple plants are going to take from the same source.
    {
        if (_occupiedTile.getFertileWaterPercentage() / _waterRequirement > 1)
        {
            _fertilityModifier *= 1;
        }
        else if (_occupiedTile.getFertileWaterPercentage() / _waterRequirement < 0)
        {
            _fertilityModifier *= 0;
        }
        else
        {
            _fertilityModifier *= _occupiedTile.getFertileWaterPercentage() / _waterRequirement;
        }
    }
}