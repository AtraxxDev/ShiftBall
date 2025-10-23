using System.Collections;
using UnityEngine;

public class DangerZoneSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] idleDangerSprite; 
    [SerializeField] private float idleChangeInterval = 0.5f;

    [SerializeField] private Sprite[] detectDangerSprite; 
    [SerializeField] private float detectChangeInterval = 0.3f;
    [SerializeField] private ShakeEffect shakeEffect;

    private Coroutine spriteCoroutine;



    public void StartIdleAnimation()
    {
        if (spriteCoroutine != null) StopCoroutine(spriteCoroutine);
        spriteCoroutine = StartCoroutine(ChangeSprites(idleDangerSprite, idleChangeInterval));
    }

    public void StartDetectAnimation()
    {
        if (spriteCoroutine != null) StopCoroutine(spriteCoroutine);
        spriteCoroutine = StartCoroutine(HandleAnim());
    }


    public IEnumerator HandleAnim()
    {
        spriteCoroutine = StartCoroutine(ChangeSprites(detectDangerSprite, detectChangeInterval));
        shakeEffect.Begin();
        yield return new WaitForSeconds(0.2f);
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

    }

    private IEnumerator ChangeSprites(Sprite[] sprites, float interval)
    {
        int index = 0;
        while (true)
        {

            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            spriteRenderer.sprite = sprites[index]; // Cambiar el sprite actual
            index = (index + 1) % sprites.Length;   // Avanzar al siguiente sprite
            yield return new WaitForSeconds(interval);
        }
    }



}
