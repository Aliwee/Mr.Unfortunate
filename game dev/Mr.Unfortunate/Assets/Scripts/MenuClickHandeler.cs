using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//用于处理开始界面跳转的类
public class MenuClickHandeler : MonoBehaviour {

	public Button startBtn, creditBtn, exitBtn;

	void Start() {
		startBtn.onClick.AddListener (StartClick);
		creditBtn.onClick.AddListener (CreditClick);
		exitBtn.onClick.AddListener (ExitClick);
	}

	//开始按钮
	public void StartClick() {
		//找到存档中的章节和关卡号
		string chapterNum = UserDataManager.instance.GetChapterNum ();
		string levelNum = UserDataManager.instance.GetLevelNum ();

		//建立场景索引
		string sceneName = "Chapter-" + chapterNum + 
		                   "-Level-" + levelNum;

		//加载场景
		SceneManager.LoadScene (sceneName);
	}

	//设置按钮
	public void CreditClick() {
		SceneManager.LoadScene ("Credits");
	}

	//退出按钮
	public void ExitClick() {
		Application.Quit ();
	}
}
