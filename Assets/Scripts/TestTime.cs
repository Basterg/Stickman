using UnityEngine;
using UnityEngine.UI;
using System;

public class TestTime : MonoBehaviour {
	[SerializeField] Text datetimeText;

	void Update ( ) {
		if ( Input.GetMouseButtonUp ( 0 ) && WorldTimeAPI.Instance.IsTimeLodaed ) {
			DateTime currentDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();

			datetimeText.text = currentDateTime.ToString ( );
		}
	}
}
