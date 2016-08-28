using UnityEngine;
using System.Collections.Generic;

public class FollowMouse : MonoBehaviour
{
    public bool OverValidPosition { get; private set; }

    private void Update() {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector2(Mathf.Floor(mousePos.x), Mathf.Floor(mousePos.y));

        if (Input.GetKeyDown(KeyCode.R)) {
            transform.Rotate(0, 0, -90);
        }

        if (IsValidPosition()) {
            GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f);
            OverValidPosition = true;
        } else {
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
            OverValidPosition = false;
        }
    }

    private bool IsValidPosition() {
        var raycasts = Physics2D.RaycastAll(transform.position + new Vector3(0.5f, 0.5f), Vector2.zero, 1, 1 << LayerMask.NameToLayer("Fertile") | 1 << LayerMask.NameToLayer("Plant"));

        bool isFertileAtPos = false;

        for (int i = 0; i < raycasts.Length; i++) {
            if (raycasts[i].collider != null && raycasts[i].transform.GetComponent<Fertile>() != null)
                isFertileAtPos = true;
            if (raycasts[i].collider != null && raycasts[i].transform.GetComponent<Plant>() != null)
                return false;
        }

        return isFertileAtPos;
    }
}