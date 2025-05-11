using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create FigureDatabase", fileName = "FigureDatabase", order = 0)]
public class FigureDatabase : ScriptableObject
{
    public List<Sprite> Forms;
    public List<Sprite> Animals;
    public List<Color> Colors;
}