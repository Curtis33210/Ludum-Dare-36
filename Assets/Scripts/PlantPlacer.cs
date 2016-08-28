using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlantPlacer : MonoBehaviour
{
    [SerializeField]
    private Transform _hotbarTransform;

    [SerializeField]
    private GameObject _hotbarButtonPrefab;

    [SerializeField]
    private GameObject[] _plantPrefabs;

    [SerializeField]
    private GameObject _ghostPrefab;

    [SerializeField]
    private GameObject _ghostObject;

    private void Awake() {
        for (int i = 0; i < _plantPrefabs.Length; i++) {
            var newHotbarButton = Instantiate(_hotbarButtonPrefab);
            newHotbarButton.name = "HotKey_" + i;
            newHotbarButton.transform.SetParent(_hotbarTransform);
            newHotbarButton.GetComponent<Image>().sprite = _plantPrefabs[i].GetComponent<SpriteRenderer>().sprite;
            newHotbarButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            newHotbarButton.GetComponentInChildren<Text>().text = (i + 1).ToString();

            var index = i;

            newHotbarButton.GetComponent<Button>().onClick.AddListener(() => {
                OnButtonPressed(index);
            });
        }

        _ghostObject = Instantiate(_ghostPrefab);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            _ghostObject.SetActive(false);
        }
    }

    public void OnButtonPressed(int buttonIndex) {
        _ghostObject.GetComponent<SpriteRenderer>().sprite = _plantPrefabs[buttonIndex].GetComponent<SpriteRenderer>().sprite;

        _ghostObject.SetActive(true);
    }
}