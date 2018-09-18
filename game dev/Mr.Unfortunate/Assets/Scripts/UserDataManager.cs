using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserDataManager : MonoBehaviour {

	private string userDataXmlPath;                   //xml文件路径
	private XmlDocument userDataXml;                  //userData.xml文件
	private XmlReaderSettings readerSetting;          //xml reader设置
	private XmlNodeList userDataNodes;                //<component>子节点列表
	private string chapterNum;                        //用户存档章节
	private string levelNum;                          //用户存档关卡
	private string[] missionStatus;
	private ArrayList missionTexts;
	private bool isChecked;

	public static UserDataManager instance = null;    //一个UserDataManager实例

	//Awake总是在任何Start方法之前调用
	void Awake() {

		//判断是否已有一个实例
		if (instance == null)
			instance = this;   //如果没有，那么设置实例为该实例
		else if (instance != this)
			Destroy (gameObject);    //保证只能有一个实例

		//设置为重新加载场景时不被销毁
		DontDestroyOnLoad(gameObject);

		//初始化
		userDataXmlPath = Application.dataPath + "/UserData/UserData.xml";
		userDataXml = new XmlDocument ();
		readerSetting = new XmlReaderSettings ();
		missionStatus = new string[] { "F", "F", "F", "F", "F" };
		missionTexts = new ArrayList();
		isChecked = false;

		//找到xml文件目录
		StartCoroutine (findXML ());
	}

	//使用这个创建和查找xml文件目录
	IEnumerator findXML() {
		//如果不存在那么就创建并且拷贝初始存档
		string path = Application.dataPath + "/UserData";
		if (!Directory.Exists (path)) {
			//创建存档目录
			Directory.CreateDirectory (path);
			//拷贝初始存档至该目录
			yield return StartCoroutine (createOriginXML ());
		}

		//加载xml文件
		LoadXML ();

		//获得用户配置数据
		GetSetting ();

		//等待3秒再执行
		yield return new WaitForSeconds (3.0f);

		//如果是Loading页面需要在获取到存档文件之后跳转至下一个页面
		if (SceneManager.GetActiveScene ().name == "Loading")
			LoadStartScene ();
	}

	//第一次使用时写入一个默认的游戏存档
	IEnumerator createOriginXML() {
		//默认初始存档的路径是在StreamingAssets
		string XMLScrPath = GetStreamingAssetsPath ();
		string XMLDesPath = userDataXmlPath;

		//使用WWW方法
		using (WWW www = new WWW (XMLScrPath)) {
			yield return www;

			//出错
			if (!string.IsNullOrEmpty (www.error))
				Debug .Log (www.error);
			//没出错就下载文件
			else {
				FileStream originXML = File.Create (XMLDesPath);
				originXML.Write (www.bytes, 0, www.bytes.Length);
				originXML.Flush ();
				originXML.Close ();
			}			
		}
	}

	//获得StreamingAssets路径
	string GetStreamingAssetsPath() {
		string prefix;    //前缀

		#if UNITY_EDITOR              //如果是Editor模式
		prefix = "file://";

		#elif UNITY_STANDALONE_WIN    //如果是Windows平台模式
		prefix = "file:///";

		#elif UNITY_STANDALONE_OSX    //如果是MACOS平台
		prefix = "file://";
		#endif 

		string path = prefix + Application.streamingAssetsPath + "/UserData.xml";

		return path;
	}

	//使用这个读取xml文件
	void LoadXML() {

		//创建xml文档
		readerSetting.IgnoreComments = true;    //忽略xml文件中注释的影响
		userDataXml.Load (XmlReader.Create (userDataXmlPath, readerSetting));

		//得到xml文件中<data>标签下的所有<component>子节点
		userDataNodes = userDataXml.SelectSingleNode ("data").ChildNodes;
	}

	//获得用户配置数据
	void GetSetting() {
		//遍历所有<component>子节点
		foreach (XmlElement component in userDataNodes) {
			//选择标签为level的<component>
			if (component .GetAttribute ("type") == "level") {
				XmlNodeList userLevelNodes = component.ChildNodes;    //获得标签为level的<component>下的子节点
				foreach (XmlElement level in userLevelNodes ) {
					if (level.GetAttribute ("type") == "chapterNum")
						this.chapterNum = level.InnerText;            //获得章节信息
					else
						this.levelNum = level.InnerText;              //获得关卡信息
				}
			}
		}
	}

	//获得用户存档路径
	public string GetUserDataPath() {
		return this.userDataXmlPath;
	}

	//获得用户存档章节数
	public string GetChapterNum() {
		return this.chapterNum;
	}

	//获得用户存档关卡数
	public string GetLevelNum() {
		return this.levelNum;
	}

	//读取用户配置完之后跳转至开始页面
	void LoadStartScene() {
		SceneManager.LoadScene ("Start");
	}

	public string[] GetMissionStatus() {
		return this.missionStatus;
	}

	public ArrayList GetMissionTexts() {
		return this.missionTexts;
	}

	public void completeMission1(){
		missionStatus [0] = "T";
	}

	public void completeMission2(){
		missionStatus [1] = "T";
	}

	public void completeMission3(){
		missionStatus [2] = "T";
	}

	public void completeMission4(){
		missionStatus [3] = "T";
	}

	public void completeMission5(){
		missionStatus [4] = "T";
	}

	public void insertMission3(string s) {
		missionTexts.Add (s);
		completeMission3 ();
	}

	public void insertMission4(string s) {
		missionTexts.Add (s);
		completeMission4 ();
	}

	public void insertMission5(string s) {
		missionTexts.Add (s);
		completeMission5 ();
	}

	public void CheckedCat() {
		this.isChecked = true;
	}

	public bool GetCheckedCat() {
		return this.isChecked;
	}

	//更新用户物品栏和关卡信息
	public void UpdateLevelData() {
		//获取退出前的场景信息
		SplitLevelName ();
		//遍历所有<component>子节点
		foreach (XmlElement component in userDataNodes) {
			//选择标签为setting的<component>
			if (component.GetAttribute ("type") == "level") {
				XmlNodeList userSettingNodes = component.ChildNodes;   //获得标签为level的<component>下的子节点
				//遍历所有<level>子节点
				foreach (XmlElement level in userSettingNodes ) {
					if (level.GetAttribute ("type") == "chapterNum")
						level.InnerText = this.chapterNum;    //更新章节信息
					else if (level.GetAttribute ("type") == "levelNum")
						level.InnerText = this.levelNum;      //更新关卡信息
				}
			}
		}

		//保存更新信息至本地用户存档
		StartCoroutine (saveUserData ());
	}

	//保存用户存档
	IEnumerator saveUserData() {
		//保存xml存档文件
		userDataXml.Save (userDataXmlPath);

		//等待1秒再执行
		yield return new WaitForSeconds (3.0f);

		//如果是Loading页面需要在获取到存档文件之后跳转至下一个页面
		if (SceneManager.GetActiveScene ().name == "Loading") 
			LoadStartScene ();
	}

	//分割场景名称字符串
	void SplitLevelName() {
		//获取当前场景名称
		string levelName = SceneManager.GetActiveScene ().name;
		//按照-分割
		string[] nameArray = levelName.Split (new char[] { '-' });
		//得到chapter和level的编号
		this.chapterNum = nameArray [1];
		this.levelNum = "1";
	}

	public void clearMissionData() {
		int num = missionStatus.Length;
		for(int i = 0; i < num; i++ ) {
			missionStatus[i] = "F";
		}

		missionTexts.Clear ();
		isChecked = false;
	}

	public void clearLevelData(){
		this.chapterNum = "1";
		this.levelNum = "1";
	}
}
