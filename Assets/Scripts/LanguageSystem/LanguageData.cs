/* Container class to store JSON file as object */
[System.Serializable]
public class LanguageData {
	public string Language;
	public Entry[] Entries;
}

[System.Serializable]
public class Entry {
	public string Label;
	public string Text;
}