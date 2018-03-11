using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using DSA.Extensions.Base;

namespace DSA.Extensions.Base.Editor
{
	public class JSonEditorWindow<T> : EditorWindow
	{
		protected virtual string path { get; }

		[ExecuteInEditMode]
		public virtual void WriteToJson(T tList)
		{
			string jText = JsonUtility.ToJson(tList);
			Debug.Log(tList);
			using (FileStream fs = new FileStream(path, FileMode.Create))
			{
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.Write(jText);
				}
			}
		}

		public void WriteToJson(T tList, string sentPath)
		{
			string jText = JsonUtility.ToJson(tList);
			Debug.Log(tList);
			using (FileStream fs = new FileStream(sentPath, FileMode.Create))
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
			using (StreamReader sr = new StreamReader(path))
			{
				text = sr.ReadToEnd();
			}
			T newTList = JsonUtility.FromJson<T>(text);
			return newTList;
		}

		public T ReadTFromJson(string sentPath)
		{
			string text;
			using (StreamReader sr = new StreamReader(sentPath))
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
	}
}