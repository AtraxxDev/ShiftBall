using UnityEngine;

    public class PlayerPrefsSaveService:MonoBehaviour, ISaveService
    {
        public void SaveBool(string key, bool value) => PlayerPrefs.SetInt(key, value ? 1 : 0);
        public bool LoadBool(string key, bool defaultValue) => PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
    }
