using UnityEngine;
using System.Collections.Generic;

public enum GrowthStatus
{
    Empty,
    Preparing,
    Growing,
    Harvesting,
}

public class Plant : MonoBehaviour
{
    [SerializeField]
    private GrowthStatus _currentStatus;

    [SerializeField]
    private Sprite _displaySprite;


}