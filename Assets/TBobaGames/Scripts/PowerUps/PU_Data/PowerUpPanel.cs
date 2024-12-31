using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TB_Tools;

public class PowerUpPanel : MonoBehaviour
{
    [SerializeField] private Transform container;

    private Dictionary<PowerUpType, Coroutine> activePowerUps = new Dictionary<PowerUpType, Coroutine>();
    private List<Transform> slots = new List<Transform>();

    private void Start()
    {
        foreach (Transform child in container)
        {
            slots.Add(child);
        }
    }

    private void OnEnable()
    {
        PowerUpBase.OnPowerUpActivate += HandlePowerUpActivated;
        PowerUpBase.OnPowerUpDeactivate += HandlePowerUpDeactivated; // Escuchar desactivaciones
    }

    private void OnDisable()
    {
        PowerUpBase.OnPowerUpActivate -= HandlePowerUpActivated;
        PowerUpBase.OnPowerUpDeactivate -= HandlePowerUpDeactivated;
    }

    private void HandlePowerUpActivated(PowerUpType type, Sprite icon, float duration)
    {
        foreach (Transform slot in slots)
        {
            if (slot.gameObject.activeSelf && slot.name == $"PowerUp_{type}")
            {
                if (activePowerUps.TryGetValue(type, out Coroutine activeCoroutine))
                {
                    StopCoroutine(activeCoroutine);
                }

                activePowerUps[type] = StartCoroutine(DeactivatePowerUpSlot(slot, type, duration));
                return;
            }
        }

        foreach (Transform slot in slots)
        {
            if (!slot.gameObject.activeSelf)
            {
                ActivatePowerUpSlot(slot, type, icon, duration);
                return;
            }
        }

        Debug.LogWarning("No hay slots disponibles para el power-up: " + type);
    }

    private void HandlePowerUpDeactivated(PowerUpType type)
    {
        foreach (Transform slot in slots)
        {
            if (slot.gameObject.activeSelf && slot.name == $"PowerUp_{type}")
            {
                slot.gameObject.SetActive(false);

                if (activePowerUps.ContainsKey(type))
                {
                    StopCoroutine(activePowerUps[type]);
                    activePowerUps.Remove(type);
                }

                Debug.Log($"Slot del power-up ({type}) desactivado.");
                return;
            }
        }
    }

    private void ActivatePowerUpSlot(Transform slot, PowerUpType type, Sprite icon, float duration)
    {
        slot.gameObject.SetActive(true);
        slot.name = $"PowerUp_{type}";

        Image image = slot.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = icon;
            image.color = Color.white;
        }

        activePowerUps[type] = StartCoroutine(DeactivatePowerUpSlot(slot, type, duration));
    }

    private IEnumerator DeactivatePowerUpSlot(Transform slot, PowerUpType type, float duration)
    {
        float flashStartTime = duration * 0.7f;
        yield return new WaitForSeconds(flashStartTime);

        Image image = slot.GetComponent<Image>();
        float remainingTime = duration - flashStartTime;
        float flashSpeed = 5f;

        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (image != null)
            {
                image.color = new Color(1f, 1f, 1f, Mathf.PingPong(Time.time * flashSpeed, 1f));
            }
            yield return null;
        }

        if (image != null)
        {
            image.color = Color.white;
        }
        slot.gameObject.SetActive(false);

        if (activePowerUps.ContainsKey(type))
        {
            activePowerUps.Remove(type);
        }
    }
}
