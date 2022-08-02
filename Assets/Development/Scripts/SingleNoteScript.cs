
using System;
using UnityEngine;

public class SingleNoteScript : MonoBehaviour
{

    [SerializeField] private GameObject showNote;
    [SerializeField] private GameObject hideNote;
    [SerializeField] private float changeSpeed;
    [SerializeField] private bool isBlank;

    private float _alpha;
    private int _changeAlpha = 0;
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _alpha = _spriteRenderer.color.a;
    }

    private void Update()
    {

        if (isBlank)
        {
            var pos = transform.position;
            pos.x -= changeSpeed * Time.deltaTime;
            transform.position = pos;
            Destroy(gameObject,3.5f);
        }
        if (transform.position.x <= showNote.transform.position.x &&
            transform.position.x > hideNote.transform.position.x)
            _changeAlpha = 1;
        else
            _changeAlpha = -1;

        var color = _spriteRenderer.color;
        color.a += _changeAlpha * Time.deltaTime * changeSpeed;
        if (color.a <= 0)
            color.a = 0;
        if (color.a >= _alpha)
            color.a = _alpha;
        
        _spriteRenderer.color = color;
    }
}
