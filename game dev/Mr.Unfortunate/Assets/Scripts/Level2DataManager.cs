using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Fungus;

public class Level2DataManager : MonoBehaviour {

	public GameObject[] missionList;
	private string[] missionStatus;
	private ArrayList missionTexts;
	public GameObject door,door2,pc,pc2,r904,door3,door4,cat,cat2;
	public Fungus.Flowchart fc;

	// Use this for initialization
	void Start () {
		missionStatus = UserDataManager.instance.GetMissionStatus ();
		missionTexts = UserDataManager.instance.GetMissionTexts();
		updateMisssions ();
		if (UserDataManager.instance.GetCheckedCat() == true && r904 != null) {
			if (missionStatus [2] == "T") {
				cat.SetActive (false);
				cat2.SetActive (true);
			} else
				r904.SetActive (true);
		}
	}
		

	public void insertMission3() {
		UserDataManager.instance.insertMission3("- Mr. Lucky's favorite food has been sold out.");
	}

	public void insertMission4() {
		UserDataManager.instance.insertMission4("- Mr. Simpson won't listen to me.");
	}

	public void insertMission5() {
		UserDataManager.instance.insertMission5("- Mr. Snow isn't at home today.");
	}

	public void changeCheckedCat() {
		UserDataManager.instance.CheckedCat ();
	}

	public void updateMisssions(){
		if(missionStatus[3] == "T" && door != null) {
			door.SetActive (false);
			door2.SetActive (true);
		}

		if(missionStatus[2] == "T" && pc != null) {
			pc.SetActive (false);
			pc2.SetActive (true);
		}

		if(missionStatus[4] == "T" && door3 != null) {
			door3.SetActive (false);
			door4.SetActive (true);
		}

		if(missionStatus[2] == "T" && missionStatus[3] == "T") {
			missionList[1].GetComponent<Text>().text = "<color=grey>" +
				missionList[1].GetComponent<Text>().text + "</color>";
		}

		if(missionStatus[4] == "T") {
			missionList[0].GetComponent<Text>().text = "<color=grey>" +
				missionList[0].GetComponent<Text>().text + "</color>";
		}

		int num = missionTexts.Count;
		for(int i = 0; i < num; i++) {
			missionList [i + 2].GetComponent<Text> ().text = missionTexts [i].ToString();
		}

		if (missionStatus[2] == "T" && missionStatus[3] == "T" && missionStatus[4] == "T"){
			excuteBlock ();
		}
	}

	void excuteBlock() {
		Block b = fc.FindBlock("final");
		fc.ExecuteBlock (b);
	}
}
