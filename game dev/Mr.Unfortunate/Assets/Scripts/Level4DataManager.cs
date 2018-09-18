using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class Level4DataManager : MonoBehaviour {

	public GameObject[] missionList;
	private string[] missionStatus;
	private ArrayList missionTexts;
	public GameObject phone1, phone2;

	// Use this for initialization
	void Start () {
		missionStatus = UserDataManager.instance.GetMissionStatus ();
		missionTexts = UserDataManager.instance.GetMissionTexts();
		updateMisssions ();
	}
		
	public void insertMission3() {
		UserDataManager.instance.insertMission3("- A drunk man broke the elevator and I cannot know the detailed information about that hiking club.");
	}

	public void completeMission1() {
		UserDataManager.instance.completeMission1 ();
	}

	public void updateMisssions(){
			
		if(missionStatus[0] == "T") {
			missionList[0].GetComponent<Text>().text = "<color=grey>" +
				missionList[0].GetComponent<Text>().text + "</color>";
			if(phone1 != null) {
				phone1.SetActive (false);
				phone2.SetActive (true);
			}
		}

		if(missionStatus[2] == "T") {
			missionList[1].GetComponent<Text>().text = "<color=grey>" +
				missionList[1].GetComponent<Text>().text + "</color>";
		}

		int num = missionTexts.Count;
		for(int i = 0; i < num; i++) {
			missionList [i + 2].GetComponent<Text> ().text = missionTexts [i].ToString();
		}
	}

	public void clearMissionData() {
		UserDataManager.instance.clearMissionData ();
	}
}
