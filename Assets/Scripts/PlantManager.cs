using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlantManager : MonoBehaviour
{
    [SerializeField]
    private int _secondsBetweenSeasons;

    private List<Plant> _allActivePlants;

    private bool _paused;

    private void Start() {
        _allActivePlants = new List<Plant>();

        StartCoroutine("GrowPlants");
    }

    private IEnumerator GrowPlants() {
        while (true) {
            yield return new WaitForSeconds(1);

            if (_paused)
                continue;

            // As long as the game isn't paused
            for (int i = 0; i < _allActivePlants.Count; i++) {
                _allActivePlants[i].GrowPlant(1.0f / _secondsBetweenSeasons);
            }
        }
    }

    public void AddPlant(Plant newPlant) {
        _allActivePlants.Add(newPlant); // Assume plant is not already in the list.
    }

    public void RemovePlant(Plant plant) {
        _allActivePlants.Remove(plant);
    }
}
