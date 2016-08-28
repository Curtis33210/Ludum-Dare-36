using UnityEngine;
using System.Collections;

public class OpenPlantingTree : MonoBehaviour {

    [SerializeField]
    private GameObject _plantingPanel;
    public void eneablePlantingTools()
    {
        _plantingPanel.SetActive(!_plantingPanel.activeInHierarchy);
    }
    // Use this for initialization
    void Start () {
        _plantingPanel.SetActive(false);
    }
	
}
