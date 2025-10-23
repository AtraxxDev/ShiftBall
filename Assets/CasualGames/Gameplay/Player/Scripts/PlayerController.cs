    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using TB_Tools;
using System.Runtime.Serialization;


public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerParticles playerParticles;
        [SerializeField] private ShieldPowerUp shieldPowerUp;

        [SerializeField] private SpriteRenderer playerSprite;
        [SerializeField] private float invulnerabilityDuration = 3f;
        [SerializeField] private float blinkInterval = 0.2f;

    public  bool isInvencible;


    private void OnEnable()
        {
            SuscribeEvents();

        }

    private void Start()
    {
        GameManager.Instance.OnRevivePlayer += HandlePlayerRevival;
    }

    private void OnDisable()
        {
            UnsuscribeEvents();
        }

        
        private void Update()
        {
            if (GameManager.Instance.IsPaused())return;
            playerMovement.HandleInput(); // Se refiere al darle click a la pantalla cambia la direccion
        }

        private void FixedUpdate()
        {

            if (GameManager.Instance.IsPaused()) return;
            playerMovement.MovePlayer();
        }


        

        private void OnCollisionEnter2D(Collision2D collision)
        {
            playerMovement.HandleCollision(collision);
        }


        public void TakeDamage(GameObject visual)
        {
            if (shieldPowerUp != null && shieldPowerUp.AbsorbHit())
            {
                visual.SetActive(false);
                return; // Interrumpir el daño si el golpe fue absorbido
            }

        if (isInvencible) return;


            GameManager.Instance.GameOver();
        }


        private void SuscribeEvents()
        {
            if (ParticleManager.Instance != null)
            {
                ParticleManager.Instance.OnParticleEffectChanged += playerParticles.UpdateParticleEffectID;
            }
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.OnHighScoreChanged += (_) => playerParticles.PlayHighScoreParticles();
            }

            

        }

        public void PausedGame()
        {
            GameManager.Instance.SetState(GameState.Paused);

            
            print("Esta en pausa 2 ");
        }


    private void UnsuscribeEvents()
        {
            if (ParticleManager.Instance != null)
            {
                ParticleManager.Instance.OnParticleEffectChanged -= playerParticles.UpdateParticleEffectID;
            }
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.OnHighScoreChanged -= (_) => playerParticles.PlayHighScoreParticles();
            }
            if (GameManager.Instance != null)
            {
                GameManager.OnPauseGame -= PausedGame;
            }

        GameManager.Instance.OnRevivePlayer -= HandlePlayerRevival;

    }

    private void HandlePlayerRevival()
    {
        //playerMovement.SetZero(); // Detener movimiento
        transform.GetChild(0).gameObject.SetActive(true);
        isInvencible = true;
        StartCoroutine(InvulnerabilityCoroutine());
    }

    private IEnumerator InvulnerabilityCoroutine()
    {


        // Parpadeo del sprite
        float elapsedTime = 0f;
        while (elapsedTime < invulnerabilityDuration)
        {
            playerSprite.enabled = !playerSprite.enabled; // Alterna visibilidad
            elapsedTime += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        // Asegúrate de que el sprite esté visible y reactiva el collider
        playerSprite.enabled = true;
        isInvencible = false;
    }

}
