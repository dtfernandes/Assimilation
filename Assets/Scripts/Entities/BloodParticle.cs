using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BloodParticle : MonoBehaviour
{
   private bool isParented = false;
    private Rigidbody2D _rigi;
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float fadeDuration;

    private void Awake(){
        _rigi = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void AddForce(Vector2 force, ForceMode2D mode)
    {
        _rigi.AddForce(force, mode);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isParented)
        {
           
            string[] targetLayers = { "GRound", "Entities", "Enemies" };

            if (Array.Exists(targetLayers, layer => other.gameObject.layer == LayerMask.NameToLayer(layer)))
            {

                transform.parent = other.transform;
                isParented = true;
                

                _rigi.gravityScale = 0f;
                _rigi.velocity = Vector2.zero;
                _rigi.isKinematic = true;

                StartCoroutine(DestroyAfterDelay());
            }
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float normalizedTime = timer / fadeDuration;
            float currentAlpha = Mathf.Lerp(1, 0f, normalizedTime);
            Color newColor = _spriteRenderer.color;
            newColor.a = currentAlpha;
            _spriteRenderer.color = newColor;
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }


    }
