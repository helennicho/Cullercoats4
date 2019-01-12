using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTV : MonoBehaviour {

    public GameObject TVDisplay;
    public GameObject Shop;

	// Use this for initialization
	void Start () {
        Shop.SetActive(true);
        TVDisplay.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickShop()
    {
        // display Video for ordering canvas
        Shop.SetActive(false);
        TVDisplay.SetActive(true);
    }

    public void OnClickTV()
    {
        // display Video for ordering canvas
        Shop.SetActive(true);
        TVDisplay.SetActive(false);
    }
}
