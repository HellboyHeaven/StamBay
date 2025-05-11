using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Figure : MonoBehaviour, IPointerClickHandler
{

    private FigureData _data;
    private SpriteRenderer _formRenderer;
    [SerializeField] private SpriteRenderer animalRenderer;
    
    
    public FigureData Data { get => _data;  set  => SetData(value); }

    public event Action<Figure> OnClicked;

    
    
    private void Awake()
    {
        _formRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked?.Invoke(this);
    }

    private void SetData(FigureData data)
    {
        _formRenderer.sprite = data.Form;
        animalRenderer.sprite = data.Animal;
        _formRenderer.color = data.Color;
        gameObject.AddComponent<PolygonCollider2D>();
        _data = data;
    }

    private void OnDestroy()
    {
        OnClicked = null;
    }

    
}