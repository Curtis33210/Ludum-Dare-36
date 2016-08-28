using UnityEngine;
using System.Collections;

public class OpenMethodsTree : MonoBehaviour {

    [SerializeField]
    private GameObject _methodTreeWindow;


    public void enableTechWindow()
    {
        _methodTreeWindow.SetActive(!_methodTreeWindow.activeInHierarchy);
    }
    // Use this for initialization
    void Start()
    {
        _methodTreeWindow.SetActive(false);
    }

}
