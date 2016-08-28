using UnityEngine;
using System.Collections;

public class OpenToolTree : MonoBehaviour {

    [SerializeField]
    private GameObject _toolTreeWindow;


    // Use this for initialization
    public void enableToolTreeWindow()
    {
        _toolTreeWindow.SetActive(!_toolTreeWindow.activeInHierarchy);
    }
    void Start () {
        _toolTreeWindow.SetActive(false);

    }
}
