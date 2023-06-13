using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public float delay = 0.1f;
    private string fullText;
    private string currentText = "";
    private bool isAnimating = false;
    private TMP_Text textComponent;
    private WaitForSeconds wait;

    private Coroutine typewriterCoroutine;

    void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
   
    }

    public void Begin(string text)
    {
        fullText = text;
        textComponent.text = "";           
        wait = new WaitForSeconds(delay);
        typewriterCoroutine = StartCoroutine(AnimateText());      
    }

    IEnumerator AnimateText()
    {
        isAnimating = true;
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            yield return wait;
        }
        isAnimating = false;
    }

    public bool IsAnimating()
    {
        return isAnimating;
    }

    public void StopTypewriter()
    {
        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
            textComponent.text = fullText;
            isAnimating = false;
        }
    }
}
