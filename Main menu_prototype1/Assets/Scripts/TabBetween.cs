using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]

public class TabBetween : MonoBehaviour {

    public InputField nextField;
    InputField currField; 

	// Use this for initialization
	void Start () {

        // Checks if nextField has value 
        // Avoids unassigned object errors 
        if (nextField == null)
        {
            Destroy(this);
            return; 
        }

        currField = GetComponent<InputField>();
	}
	
	// Update is called once per frame
	void Update () {

        // Goes from current field to next field with 'Tab'
        if (currField.isFocused && Input.GetKeyDown(KeyCode.Tab))
        {
            nextField.ActivateInputField(); 
        }
		
	}
}
