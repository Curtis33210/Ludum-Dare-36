using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private int _waitTime;

    [SerializeField]
    private GameObject _targetGameObject;

    private void OnEnable() {
        StartCoroutine("StartTimer");
    }

    private IEnumerator StartTimer() {
        yield return new WaitForSeconds(_waitTime);

        _targetGameObject.SetActive(true);
    }
}