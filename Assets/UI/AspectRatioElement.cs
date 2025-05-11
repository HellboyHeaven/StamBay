using UnityEngine;
using UnityEngine.UIElements;

// Custom element that lays out its contents following a specific aspect ratio.
[UxmlElement]
public partial class AspectRatioElement : VisualElement
{
    [UxmlAttribute]
    public float aspectRatio { get; set; } = 1.0f; // width / height

    public AspectRatioElement()
    {
        RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
    }

    private void OnGeometryChanged(GeometryChangedEvent evt)
    {
        float width = resolvedStyle.width;
        if (float.IsNaN(width) || width <= 0f) return;

        style.height = width / aspectRatio;
    }
}