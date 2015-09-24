using UnityEngine;
using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace GameWorthy {

	public class MemoryCard {

		/// <summary>
		/// Set environment variables
		/// </summary>
		public static void Initiate() {
			Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		}

		/// <summary>
		/// Save the specified _objToSave as specified _saveFileName.
		/// NOTE: Before saving the object, make sure that your object class is [Serializable]
		/// </summary>
		/// <param name="_objToSave">_obj to save.</param>
		/// <param name="_saveFileName">_saveFileName</param>
		public static void Save(object _objToSave, string _saveFileName) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (Application.persistentDataPath + "/" + _saveFileName + ".dat");
			bf.Serialize (file, _objToSave);
			file.Close ();
		}

		/// <summary>
		/// Load the specified _saveFileName.
		/// </summary>
		/// <param name="_saveFileName">_save file name.</param>
		public static object Load(string _saveFileName) {
			if (File.Exists (Application.persistentDataPath + "/" + _saveFileName + ".dat")) {
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "/" + _saveFileName + ".dat", FileMode.Open);
				object savedData = bf.Deserialize(file);

				file.Close();

				return savedData;
			}

			return null;
		}

		public static void DeleteSaveFile(string _location) {
			File.Delete (Application.persistentDataPath + "/" + _location + ".dat");
		}
	}
}

