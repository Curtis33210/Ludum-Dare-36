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
    [Range(1, 100)]
    private int _waterRequirement; // Is this taken every time the plant grows? Cause thats what im assuming
    //Note: Do not let this equal zero.
    
    [SerializeField]
    private Fertile _occupiedTile;

    //private Plant _adjacentPlant; // I think the bean multipler will be done in a different script

    public void Start()
    {
        var _raycastFerTile = Physics2D.Raycast(transform.position + new Vector3(0.5f, 0.5f), Vector2.zero, 1, 1 << LayerMask.NameToLayer("Fertile"));

        if (_raycastFerTile.collider == null) {
            Debug.LogError("No Fertile found below plant");
            return;
        }

        //Debug.Log(_raycastFerTile.transform.GetComponent<Fertile>());
        _occupiedTile = _raycastFerTile.transform.GetComponent<Fertile>();
        _occupiedTile.toggleCropRotation();
        _occupiedTile.setCurrentCrop(this);

        SetFertileFertilityModifier(_occupiedTile);

        FindObjectOfType<PlantManager>().AddPlant(this);
    }

    //private void beanMaizeMultiplyer() //the bean maize phenomenon
    //{
    //    if (this.name.Equals("Bean")){
    //        //left
    //        var _raycastMaize = Physics2D.Raycast(transform.position+transform.right *-0.5f, Vector2.zero);
    //        if (_raycastMaize.collider != null)
    //        {
    //            _adjacentPlant = _raycastMaize.transform.GetComponent<Plant>();
    //        }
    //        //right
    //        _raycastMaize = Physics2D.Raycast(transform.position + transform.right * 1.5f, Vector2.zero);
    //        if (_raycastMaize.collider != null)
    //        {
    //            _adjacentPlant = _raycastMaize.transform.GetComponent<Plant>();
    //        }
    //        if (_adjacentPlant.name.Equals("Maize"))
    //        {
    //            _fertilityModifier += 0.25f;
    //        }
    //    }
    //}

    private void SetFertileFertilityModifier(Fertile _ferTile)
    {
        _fertilityModifier = _ferTile.getFertilityModifier();
    }
    public float HarvestPlant() {
        //TODO: Fix the return amount (I think I fixed it.....)

        return _totalPercentageGrown * _maturityTime / (_harvestModifier - _fertilityModifier); // Probably some base multiplier and specific multipler per plant type?
    }

    public bool GrowPlant(float seasonsPast) { // Returns true if plant grew, false if it didn't.
        //TODO: Check the algorithm for plant growth percentage (checked and ready Cap'n)
        // Need to check that there is enough water for this plant to grow before actually growing it. 

        //if (_occupiedTile.GiveWaterToPlant(_waterRequirement) < _waterRequirement) {
        //    Debug.LogError("Plant did not get enough water");
        //    return false; // Plant did not grow
        //}

        Debug.Log(_waterRequirement * seasonsPast);
        SetFertileFertilityModifier(_occupiedTile);

        UpdateWaterFertilityModifier();

        _occupiedTile.GiveWaterToPlant(_waterRequirement * seasonsPast);

        var percentGrown = (seasonsPast*(1+_fertilityModifier))/ _maturityTime;
        
        _totalPercentageGrown = Mathf.Clamp(_totalPercentageGrown + percentGrown, 0, 100);

        return true; // Plant did grow
    }

    private void UpdateWaterFertilityModifier()
    {
        var tileWaterLevel = _occupiedTile.WaterLevel();

        if (tileWaterLevel < _waterRequirement && !(tileWaterLevel < 0))
        {
            _fertilityModifier *= tileWaterLevel / _waterRequirement;
        }
        else if (tileWaterLevel == 0)
        {
            _fertilityModifier *= 0;
        }
    }
}