using UnityEngine;

public class ToggleOnEvent : MonoBehaviour
{
    [SerializeField] private string[] eventNames;
    [SerializeField] private GameObject[] objects;

    private void OnEnable()
    {
        foreach (string evt in eventNames)
        {
            EventManager.StartListening(evt, OnEventTriggered);
        }
    }

    private void OnDisable()
    {
        foreach (string evt in eventNames)
        {
            EventManager.StopListening(evt, OnEventTriggered);
        }
    }

    private void OnEventTriggered(object param)
    {
        print("OnEventTriggered in: " + param);
        if (param == null) return;

        bool value = (bool)param;

        for (int i = objects.Length - 1; i >= 0; i--)
        {
            objects[i].SetActive(value);
        }
    }
}