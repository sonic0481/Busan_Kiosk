using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleColor : MonoBehaviour
{
    [SerializeField] Color offColor;
    [SerializeField] Color onColor;

    Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnValueChanged);
    }

    public void OnValueChanged(bool isOn)
    {
        ColorBlock cb = toggle.colors;
        if (isOn)
        {
            cb.normalColor = onColor;
            cb.selectedColor = onColor;
        }
        else
        {
            cb.normalColor = offColor;
            cb.selectedColor = offColor;
        }
        toggle.colors = cb;
    }
}
