using System.IO;
using System.Collections.Generic;
using UnityEngine;



public class LanguageHandler : MonoBehaviour {
	// Singleton object to reach easly from all scripts
	public static LanguageHandler singleton = null;

	// Member data variables
	static List<LanguageData> languageData = null;
	static Entry[] entries = null;

	// Constant variables for language system
	const string JSON_EXTENSION = ".json";
	const int BOM_LENGTH = 3;



	/* Assigning singleton on awake if it is null, else destroy component */
	void Awake() {
		if (singleton == null)
			singleton = this;

		else
			Destroy(this);


		// Initializing language data to be ready at Start() stage
		languageData = new List<LanguageData>();
		ReadFile();
		ChangeLanguage(Global.DEFAULT_LANGUAGE);
	}



	/* Function to read language file and obtain json data as local object */
	private void ReadFile() {
		string path;
		string data;
		string fileName;


		// Setting the path of language files for each language
		path = Path.Combine(Application.streamingAssetsPath, Global.LANGUAGE_FOLDER_NAME);
		foreach (string language in Global.Languages) {
			fileName = Path.Combine(path, language.ToLower() + JSON_EXTENSION);

			// Reading language.json files from Android
			if (Application.platform == RuntimePlatform.Android) {
				WWW reader = new WWW(fileName);
				while(!reader.isDone);
				data = System.Text.Encoding.UTF8.GetString(reader.bytes, BOM_LENGTH, reader.bytes.Length-BOM_LENGTH);
			}

			// Reading language.json files from Windows
			else {
				data = File.ReadAllText(fileName);
			}

			// Converting data text to JSON object
			languageData.Add(JsonUtility.FromJson<LanguageData>(data));
		}
	}



	/* Getter function to return texts according to their labels */
	public static string GetLabel(string label) {
		// Search and return matched label
		if (entries != null) {
			foreach (Entry entry in entries) {
				if (entry.Label == label) {
					return entry.Text;
				}
			}
		}


		// Return null if nothing found
		return "null";
	}



	/* Function to change the language and entries */
	public static void ChangeLanguage(string newLanguage) {
		// Finding language data from list
		foreach (LanguageData data in languageData) {
			if (data.Language == newLanguage) {
				entries = data.Entries;
			}
		}
	}
}
