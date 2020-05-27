using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_anchor : MonoBehaviour {

	public RectTransform FatherImg;
	public RectTransform SubImg;
	public Button LeftButton;
	public Button RightButton;
	// Use this for initialization
	public Vector3 AnchorPos;
	public Vector3 TransPos;
	void Start () {
		FatherImg = GameObject.Find("UIRoot/Father").GetComponent<RectTransform>();
		SubImg = GameObject.Find("UIRoot/Father/Child").GetComponent<RectTransform>();
		LeftButton = GameObject.Find("UIRoot/LeftButton").GetComponent<Button>();
		RightButton = GameObject.Find("UIRoot/RightButton").GetComponent<Button>();
		// Debug.LogError(SubImg.anchoredPosition3D);

		LeftButton.onClick.AddListener(OnLeftButtonClick);
		RightButton.onClick.AddListener(OnRightButtonClick);
	}


	void OnLeftButtonClick()
	{
		// SubImg.anchoredPosition = AnchorPos;
		SubImg.transform.localPosition = TransPos;
		Debug.Log(string.Format("localPosition:{0} anchoredPosition:{1}  ", SubImg.transform.localPosition, SubImg.anchoredPosition));
	}

	void OnRightButtonClick()
	{
		// SubImg.anchoredPosition = AnchorPos;
		// SubImg.transform.localPosition = TransPos;
		Debug.Log(string.Format("localPosition:{0} anchoredPosition:{1}  ", SubImg.transform.localPosition, SubImg.anchoredPosition));
	}
	
	// Update is called once per frame
	void Update () {
		// Debug.LogError(SubImg.anchoredPosition3D);
	}
}
