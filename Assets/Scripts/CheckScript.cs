using UnityEngine;
using System.Collections;

public class CheckScript : MonoBehaviour {
    [SerializeField]
    private Sprite pepsi;

    private int test1;
    public int Test
    {
        get { return test1; }
        set
        {
            test1 = value;
        }
    }

	// Use this for initialization
	void Start () {
        Debug.Log("Hello World");
        test = 10;
	}
	 
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SpriteRenderer coke = GetComponent<SpriteRenderer>();
            var tempSprite = coke.sprite;

            coke.sprite = pepsi;

            pepsi = tempSprite;
        }
	}
}
