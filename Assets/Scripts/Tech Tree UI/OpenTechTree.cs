using UnityEngine;
using System.Collections;

public class OpenTechTree : MonoBehaviour {
    
    [SerializeField]
    private GameObject _techTreeWindow;


    public void enableTechWindow()
    {
        _techTreeWindow.SetActive(!_techTreeWindow.activeInHierarchy);
    }
	// Use this for initialization
	void Start () {
        _techTreeWindow.SetActive(false);
    }
	
}
