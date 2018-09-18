using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class Level3DataManager : MonoBehaviour {
	
	public GameObject[] missionList;
	private string[] missionStatus;
	private ArrayList missionTexts;
	public GameObject box,box2,room,room2;
	public Fungus.Flowchart fc;

	// Use this for initialization
	void Start () {
		missionStatus = UserDataManager.instance.GetMissionStatus ();
		missionTexts = UserDataManager.instance.GetMissionTexts();
		updateMisssions ();
	}

	public void completeMission2() {
		UserDataManager.instance.completeMission2 ();
	}

	public void completeMission3() {
		UserDataManager.instance.completeMission3 ();
	}

	public void insertMission4() {
		UserDataManager.instance.insertMission4("- I can't recall my account number without my music box.");
	}

	public void changeCheckedCat() {
		UserDataManager.instance.CheckedCat ();
	}

	public void updateMisssions(){
		if(missionStatus[2] == "T" && box != null) {
			box.SetActive (false);
			box2.SetActive (true);
		}

		if(missionStatus[1] == "T" && room != null) {
			room.SetActive (false);
			room2.SetActive (true);
		}

		if(missionStatus[2] == "T") {
			missionList[2].GetComponent<Text>().text = "<color=grey>" +
				missionList[2].GetComponent<Text>().text + "</color>";
		}

		if(missionStatus[1] == "T") {
			missionList[1].GetComponent<Text>().text = "<color=grey>" +
				missionList[1].GetComponent<Text>().text + "</color>";
		}

		if(missionStatus[3] == "T") {
			missionList[0].GetComponent<Text>().text = "<color=grey>" +
				missionList[0].GetComponent<Text>().text + "</color>";
		}

		int num = missionTexts.Count;
		for(int i = 0; i < num; i++) {
			missionList [i + 3].GetComponent<Text> ().text = missionTexts [i].ToString();
		}

		if (missionStatus[1] == "T" && missionStatus[2] == "T" && missionStatus[3] == "T"){
			excuteBlock ();
		}
	}

	void excuteBlock() {
		Block b = fc.FindBlock("final");
		fc.ExecuteBlock (b);
	}

}
