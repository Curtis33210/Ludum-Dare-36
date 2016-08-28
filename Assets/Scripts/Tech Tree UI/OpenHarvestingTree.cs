using UnityEngine;
using System.Collections;

public class OpenHarvestingTree : MonoBehaviour {


    [SerializeField]
    private GameObject _harvestingToolsPanel;
    public void eneablePlantingTools()
    {
        _harvestingToolsPanel.SetActive(!_harvestingToolsPanel.activeInHierarchy);
    }
    // Use this for initialization
    void Start()
    {
        _harvestingToolsPanel.SetActive(false);
    }
}
