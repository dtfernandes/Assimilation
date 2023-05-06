using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
