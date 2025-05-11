using System;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class StateUIController : MonoBehaviour
{
   private UIDocument _uiDocument;

   public event Action OnRestartClicked;
   
   private void Awake()
   {
      _uiDocument = GetComponent<UIDocument>();
   }

   private void OnEnable()
   {
      _uiDocument.rootVisualElement.Q<VisualElement>("Content").Q<Button>("Restart").clicked += () => OnRestartClicked?.Invoke();
   }
}
