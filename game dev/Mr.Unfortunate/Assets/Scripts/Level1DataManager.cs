using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level1DataManager : MonoBehaviour {

	public GameObject[] missionList;
	private string[] missionStatus = null;
	public GameObject suside,rope,chair;

	void Start() {
		missionStatus = UserDataManager.instance.GetMissionStatus ();

		if(missionStatus[0] == "T") {
			missionList[0].GetComponent<Text>().text = "<color=grey>- Place the chair.</color>";
			if(chair != null)
				chair.SetActive (false);
		}
			
		if(missionStatus[1] == "T") {
			missionList[1].GetComponent<Text>().text = "<color=grey>- Prepare the rope.</color>";
			if (rope != null)
				rope.SetActive (false);
		}
			

		if (missionStatus [0] == "T" && missionStatus [1] == "T" && SceneManager.GetActiveScene ().name == "Chapter-1-Level-3")
			suside.SetActive (true);
			
	}

	public void UpdateMission() {

		if(missionStatus[0] == "T")
			missionList[0].GetComponent<Text>().text = "<color=grey>- Place the chair.</color>";
		else
			missionList[0].GetComponent<Text>().text = "- Place the chair.";
		
		if(missionStatus[1] == "T")
			missionList[1].GetComponent<Text>().text = "<color=grey>- Prepare the rope.</color>";
		else
			missionList[1].GetComponent<Text>().text = "- Place the chair.";
	}

	public void completeChair(){
		UserDataManager.instance.completeMission1 ();
	}

	public void completeRope(){
		UserDataManager.instance.completeMission2 ();
	}

	public void clearMissionData() {
		UserDataManager.instance.clearMissionData ();
	}
}
