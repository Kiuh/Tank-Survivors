using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsContainer : MonoBehaviour
{
    [SerializeField]
    private List<Image> stars;

    [SerializeField]
    private Color firedColor;

    public void SetupProgress(int starsCount)
    {
        for (int i = 0; i < starsCount; i++)
        {
            stars[i].color = firedColor;
        }
    }
}
