using System.IO;
using System.Collections.Generic;
using UnityEngine;



public class LanguageHandler : MonoBehaviour {
	/* Singleton object to reach easly from all scripts */
	public static LanguageHandler singleton = null;

	/* Member data variables */
	static List<LanguageData> languageData = null;
	static Entry[] entries = null;



	/* Assigning singleton on awake if it is null, else destroy component */
	void Awake() {
		if (singleton == null)
			singleton = this;

		else
			Destroy(this);


		/* Initializing language data to be ready at Start() stage */
		languageData = new List<LanguageData>();
		ReadFile();
		ChangeLanguage(Global.DEFAULT_LANGUAGE);
	}



	/* Function to read language file and obtain json data as local object */
	private void ReadFile() {
		string path = null;

		// TODO: Fix the Android location problem, streaming assets can't found currently
		if (Application.platform == RuntimePlatform.Android) {
			path = Path.Combine("jar:file://" + Application.dataPath + "!assets/", Global.LANGUAGE_FOLDER_NAME);
		}

		else {
			path = Path.Combine(Application.streamingAssetsPath, Global.LANGUAGE_FOLDER_NAME);
		}


		string[] files = Directory.GetFiles(path);
		string data;


		/* Reading each JSON file in indicated directory to get each language data */
		foreach (string file in files) {
			if (Path.GetExtension(file) == Global.JSON_EXTENSION) {
				data = File.ReadAllText(file);
				languageData.Add(JsonUtility.FromJson<LanguageData>(data));
			}
		}
	}



	/* Getter function to return texts according to their labels */
	public static string GetLabel(string label) {
		/* Search and return matched label */
		if (entries != null) {
			foreach (Entry entry in entries) {
				if (entry.Label == label) {
					return entry.Text;
				}
			}
		}


		/* Return null if nothing found */
		return "null";
	}



	/* Function to change the language and entries */
	public static void ChangeLanguage(string newLanguage) {
		/* Finding language data from list */
		foreach (LanguageData data in languageData) {
			if (data.Language == newLanguage) {
				entries = data.Entries;
			}
		}
	}
}
