using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamUI : MonoBehaviour {

   // public GameObject FlavoursUI;
    public GameObject OrderConfirmedUI;

	// Use this for initialization
	void Start () {
  //      FlavoursUI.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnLook()
    {
        OrderConfirmedUI.SetActive(true);
    }

    public void LookAway()
    {
        OrderConfirmedUI.SetActive(false);
    }

    public void OnClick()
    {
        Debug.Log("in IceCream/OnClick");
        // display Thankyou for ordering canvas
        OrderConfirmedUI.SetActive(true);
    }
       

}
