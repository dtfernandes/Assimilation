using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private UpgradeDefinition _definition;

    [SerializeField]
    private TextMeshProUGUI _description;
    [SerializeField]
    private TextMeshProUGUI _title;

    public Action OnSelection { get; set; }

    public void Setup(UpgradeDefinition selectedDefinition)
    {
        if(selectedDefinition == null) 
        {
            gameObject.SetActive(false);
            return;
        }
        else
             gameObject.SetActive(true);


        _definition = selectedDefinition;

        _title.text = _definition.Title;
        _description.text = _definition.Description;
    }

    void Start()
    {
        _title.text = _definition.Title;
        _description.text = _definition.Description;
    }

    public void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        _description.gameObject.SetActive(true);
        transform.localScale = new Vector2(1.1f,1.1f);
    }

    public void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        _description.gameObject.SetActive(false);
        transform.localScale = new Vector2(1,1);
    }

    public void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        //Change Stat
        _definition.GameValue.Value += _definition.Value;
        OnSelection?.Invoke();
    }
  
}

