using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonController : MonoBehaviour
{
    private VisualElement _buttons;

    public Button RerollButton { get; private set; }

    private void Awake()
    {
        _buttons = GetComponent<UIDocument>()
            .rootVisualElement
            .Q<VisualElement>("Buttons");

        RerollButton = _buttons.Q<Button>("Reroll");
    }
    

    
   
}