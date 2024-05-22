using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonSaveLoad
{
	public static void JsonSave<T>(T data, string fileName)
	{
		string jsonString = JsonConvert.SerializeObject(data);
		string filePath = Path.Combine(Application.persistentDataPath, fileName);
		File.WriteAllText(filePath, jsonString);
	}

	public static T JsonLoad<T>(string fileName)
	{
		string filePath = Path.Combine(Application.persistentDataPath, fileName);
		T data = default(T);
		if (File.Exists(filePath))
		{
			string jsonString = File.ReadAllText(filePath);
			data = JsonConvert.DeserializeObject<T>(jsonString);
		}
		return data;
	}
}
