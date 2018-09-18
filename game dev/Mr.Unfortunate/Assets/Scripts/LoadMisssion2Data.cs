using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMisssion2Data : MonoBehaviour {

	public GameObject[] missionList;
	private ArrayList missionTexts;

	// Use this for initialization
	void Start () {
		UserDataManager.instance.insertMission5("- Mr. Snow will still go hiking.");
		missionTexts = UserDataManager.instance.GetMissionTexts();

		missionList[0].GetComponent<Text>().text = "<color=grey>" +
			missionList[0].GetComponent<Text>().text + "</color>";
		missionList[1].GetComponent<Text>().text = "<color=grey>" +
			missionList[1].GetComponent<Text>().text + "</color>";
		missionList[2].GetComponent<Text>().text = "<color=grey>" +
			missionList[2].GetComponent<Text>().text + "</color>";

		int num = missionTexts.Count;
		for(int i = 0; i < num; i++) {
			missionList[i+3].GetComponent<Text>().text = missionTexts [i].ToString();
		}
	}

	public void clearMissionData() {
		UserDataManager.instance.clearMissionData ();
	}
}
