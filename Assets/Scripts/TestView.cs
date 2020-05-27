using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestView : MonoBehaviour
{

	public Button button;
	public Text text;
	// Use this for initialization
	void Start()
	{
		var panel = this.transform.Find("Panel");
		button = panel.Find("Button").GetComponent<Button>();
		text = panel.Find("Text").GetComponent<Text>();

		button.onClick.AddListener(OnBtnClick);
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnBtnClick()
	{
		Debug.LogError("Button click");
	}
}