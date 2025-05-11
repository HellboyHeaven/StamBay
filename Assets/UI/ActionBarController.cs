using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ActionBarController : MonoBehaviour
{
   private List<VisualElement> _figureViews;

   public void SetData(List<FigureData> figureDatas)
   {
      gameObject.SetActive(true);
      _figureViews = GetComponent<UIDocument>()
         .rootVisualElement
         .Q<VisualElement>("Container")
         .Q<VisualElement>("Content").Children().ToList();
      
      Clear();
      for (int i = 0; i < figureDatas.Count; i++)
      {
         setData(figureDatas[i], i);
      }
   }

   private void setData(FigureData figureData, int index)
   {
      _figureViews[index].style.backgroundImage = new StyleBackground(figureData.Form);
      _figureViews[index].style.unityBackgroundImageTintColor = new StyleColor(figureData.Color);
      _figureViews[index].Q<VisualElement>("AnimalView").style.backgroundImage = new StyleBackground(figureData.Animal);
   }

   private void Clear()
   {
      foreach (var figureView in _figureViews)
      {
         figureView.style.backgroundImage = null;
         figureView.style.unityBackgroundImageTintColor = new StyleColor(Color.gray);
         figureView.Q<VisualElement>("AnimalView").style.backgroundImage = null; 
      }
   }
   
}