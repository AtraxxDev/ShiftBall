using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    
    public void PlaySoundSFX()
    {
        AudioManager.Instance.PlaySFX("SelectUI");
    }

}
