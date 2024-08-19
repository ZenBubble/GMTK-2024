using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class ScrambleFruitTexture : MonoBehaviour
{
    [SerializeField] private Sprite[] fruits;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite fruit = fruits[Random.Range(0, fruits.Length - 1)];
        spriteRenderer.sprite = fruit; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
