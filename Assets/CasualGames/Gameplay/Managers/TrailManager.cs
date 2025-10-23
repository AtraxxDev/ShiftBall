using System;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    
    public static TrailManager Instance { get; private set; }
    
    [SerializeField] private int _currentTrailID;
    
    //public event Action<int> OnTrailDataChanged;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void ChangeColorTrailID(int newTrailColorID)
    {
        _currentTrailID = newTrailColorID;
        PlayerPrefs.SetInt("TrailKey", _currentTrailID);
        PlayerPrefs.Save();
    }
   
}
