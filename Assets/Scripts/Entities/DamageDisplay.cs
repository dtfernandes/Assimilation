using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamageDisplay : MonoBehaviour
{
    private static Canvas _canvas;

    [SerializeField]
    private TextMeshProUGUI _textComponent;

    [SerializeField]
    private float _riseDuration = 1f;

    [SerializeField]
    private float _fadeDuration = 0.5f;

    private float _elapsedTime = 0f;


    [SerializeField]
    private Vector3 _translationDistance;

    private Vector3 _initialPosition;
    private Vector3 _targetPosition;

    private Vector3 _initialScale;
    private Vector3 _targetScale;

    private Color _initialColor;
    private Color _targetColor;

    private bool _isRising = true;

    public void SetText(int damage)
    {
        _textComponent.text = damage + "";

        _initialPosition = transform.position;
        _targetPosition = _initialPosition + _translationDistance;
    }
       

    private void Awake()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();



        _initialScale = transform.localScale;
        _targetScale = _initialScale * 1.5f;

        _initialColor = _textComponent.color;
        _targetColor = new Color(_initialColor.r, _initialColor.g, _initialColor.b, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_canvas == null)
            _canvas = GameObject.FindObjectOfType<Canvas>();

        transform.SetParent(_canvas.transform);
      
      
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_isRising)
        {
            float t = Mathf.Clamp01(_elapsedTime / _riseDuration);

            transform.position = Vector3.Lerp(_initialPosition, _targetPosition, t);
            transform.localScale = Vector3.Lerp(_initialScale, _targetScale, t);
            _textComponent.color = Color.Lerp(_initialColor, _targetColor, t);

            if (_elapsedTime >= _riseDuration)
            {
                _isRising = false;
                _elapsedTime = 0f;
            }
        }
        else
        {           
            Destroy(gameObject);           
        }
    }
}
