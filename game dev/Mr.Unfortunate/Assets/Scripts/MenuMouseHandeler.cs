using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//用于处理开始界面鼠标移入移出效果
public class MenuMouseHandeler : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {

	private string path1, path2;
	public GameObject button;

	// Use this for initialization
	void Start () {
		path1 = "items/menuMouseIn";
		path2 = "items/transparent";
	}
	
	// 鼠标移入
	public void OnPointerEnter (PointerEventData eventData) {
		Sprite imageSource = new Sprite ();
		imageSource = Resources.Load (path1, imageSource.GetType ()) as Sprite;
		button.GetComponent<Image> ().sprite = imageSource;
	}

	//鼠标移出
	public void OnPointerExit (PointerEventData eventData) {
		Sprite imageSource = new Sprite ();
		imageSource = Resources.Load (path2, imageSource.GetType ()) as Sprite;
		button.GetComponent<Image> ().sprite = imageSource;
	}
}
