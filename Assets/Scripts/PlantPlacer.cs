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
    private FollowMouse _ghostObject;

    private int _ghostIndex;

    private void Awake() {
        for (int i = 0; i < _plantPrefabs.Length; i++) {
            var newHotbarButton = Instantiate(_hotbarButtonPrefab);
            newHotbarButton.name = "HotKey_" + i;
            newHotbarButton.transform.SetParent(_hotbarTransform);
            newHotbarButton.GetComponent<Image>().sprite = _plantPrefabs[i].GetComponent<SpriteRenderer>().sprite;
            newHotbarButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            newHotbarButton.GetComponentInChildren<Text>().text = (i + 1).ToString();

            var index = i; // Need to create a local variable. Don't delete this

            newHotbarButton.GetComponent<Button>().onClick.AddListener(() => {
                OnButtonPressed(index);
            });
        }

        _ghostObject = Instantiate(_ghostPrefab).GetComponent<FollowMouse>();
        _ghostObject.gameObject.SetActive(false);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            _ghostObject.gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0) && _ghostObject.OverValidPosition) {
            var newPlant = Instantiate(_plantPrefabs[_ghostIndex]);

            var ghostGameObject = _ghostObject.gameObject;

            newPlant.transform.position = ghostGameObject.transform.position;
        }
    }

    public void OnButtonPressed(int buttonIndex) {
        _ghostIndex = buttonIndex;
        
        _ghostObject.GetComponent<SpriteRenderer>().sprite = _plantPrefabs[buttonIndex].GetComponent<SpriteRenderer>().sprite;

        _ghostObject.gameObject.SetActive(true);
    }
}