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
        var raycast = Physics2D.Raycast(transform.position + new Vector3(0.5f, 0.5f), Vector2.zero, 1, 1 << LayerMask.NameToLayer("Fertile"));
        
        if (raycast.collider == null || raycast.transform.GetComponent<Fertile>() == null)
            return false;

        return true;
    }
}