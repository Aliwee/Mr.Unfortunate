using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Fungus;
using AssemblyCSharp;

public class MouseHandler : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler {

	//是否锁定鼠标
	private bool wasLocked = false;

	//cursorTexture：使用的2D图片
	public Texture2D[] cursorTextures;

	//鼠标移入 设置鼠标样式
	public void OnPointerEnter (PointerEventData eventData)
	{
		switch (this.tag) {
		case "PickableItem":
			Cursor.SetCursor (cursorTextures [(int)CursorState.Pick], Vector2.zero, CursorMode.Auto);
			break;
		case "CheckableItem":
			Cursor.SetCursor (cursorTextures [(int)CursorState.Check], Vector2.zero, CursorMode.Auto);
			break;
		case "LWalkableItem":
			Cursor.SetCursor (cursorTextures [(int)CursorState.LWalk], Vector2.zero, CursorMode.Auto);
			break;
		case"RWalkableItem":
			Cursor.SetCursor (cursorTextures [(int)CursorState.RWalk], Vector2.zero, CursorMode.Auto);
			break;
		case "TalkableItem":
			Cursor.SetCursor (cursorTextures [(int)CursorState.Talk], Vector2.zero, CursorMode.Auto);
			break;
		default:
			break;
		}
	}

	/// <summary>
	/// 鼠标移出 设置鼠标样式为默认
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerExit (PointerEventData eventData)
	{
		Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);
	}

	public void OnPointerClick (PointerEventData eventData){
		lockCursor ();
	}

	/// <summary>
	/// 锁定鼠标
	/// </summary>
	public void lockCursor ()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		wasLocked = true;
	}

	/// <summary>
	/// 解锁鼠标
	/// </summary>
	public void unlockCursor ()
	{
		Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		wasLocked = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape) && wasLocked) {
			unlockCursor ();
		}
	}

	public void clearData(){
		UserDataManager.instance.clearLevelData ();
	}

}
