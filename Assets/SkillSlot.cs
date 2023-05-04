using UnityEngine;
using UnityEngine.EventSystems;





public class SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{



    [SerializeField]
    private GameObject _descriptionTest;

    public void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        _descriptionTest.SetActive(true);
        transform.localScale = new Vector2(1.1f,1.1f);
    }

    public void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        _descriptionTest.SetActive(false);
        transform.localScale = new Vector2(1,1);
    }

    public void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
