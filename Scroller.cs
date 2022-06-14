using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage img;
    [SerializeField] private float scrollSpeed;
    void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(scrollSpeed, 0) * Time.deltaTime, img.uvRect.size);
    }
}
