using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

namespace DSA.Extensions.Base
{
	public abstract class JsonWriter<T> : ScriptableObject, IProcessor, ISettable, IProvider<string, string, string>
	{
		[SerializeField] protected string path;
		[SerializeField] protected List<string> uniqueIDs;
		protected string Path { get { return path; } }

		public abstract void Process();
		public abstract void Set();

		[ExecuteInEditMode]
		protected virtual void WriteToJson(T tList)
		{
			string jText = JsonUtility.ToJson(tList);
			Debug.Log(tList);
			using (FileStream fs = new FileStream(Path, FileMode.Create))
			{
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.Write(jText);
				}
			}
		}

		[ExecuteInEditMode]
		public T ReadTFromJson()
		{
			string text;
			using (StreamReader sr = new StreamReader(Path))
			{
				text = sr.ReadToEnd();
			}
			T newTList;
			try { newTList = JsonUtility.FromJson<T>(text); }
			catch (System.Exception e)
			{
				e.ToString();
				newTList = default(T);
			}
			return newTList;
		}

		public string GetItem(string sentUniqueID, string sentPrefix)
		{
			if (uniqueIDs == null)
			{
				uniqueIDs = new List<string>();
			}
			if (!string.IsNullOrEmpty(sentUniqueID))
			{
				if (uniqueIDs.Contains(sentUniqueID))
				{
					return sentUniqueID;
				}
			}
			string tempID = "#" + sentPrefix + "#" + GetRandomizedString(11);
			if (uniqueIDs.Contains(tempID))
			{
				tempID = GetItem(sentUniqueID, sentPrefix);
				return tempID;
			}
			uniqueIDs.Add(tempID);
			return tempID;
		}

		protected string GetRandomizedString(int step)
		{
			string availableChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			char tempChar = availableChars[UnityEngine.Random.Range(0, availableChars.Length)];
			string tempString = tempChar.ToString();
			if (step > 0)
			{
				tempString = tempString + GetRandomizedString(step - 1);
			}
			return tempString;
		}
	}
}