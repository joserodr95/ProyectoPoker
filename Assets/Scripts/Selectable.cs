using UnityEngine;

public class Selectable : MonoBehaviour
{
    
    private readonly Vector3 _offSet = new Vector3(0f, 1f, 0f);
    [field: SerializeField]
    private bool _faceUp;
    [field: SerializeField]
    private bool _selected;
    private SpriteRenderer _spriteRenderer = null;
    private Transform _transform;
    
    public bool FaceUp
    {
        get => _faceUp;
        set
        {
            if (_spriteRenderer is object)
            {
                if (value)
                {
                    _spriteRenderer.color = Color.white;
                }
                else
                {
                    _spriteRenderer.color = CustomColor.cardbackRed;
                }
            }

            _faceUp = value;
        }
    }

    public bool Selected
    {
        get => _selected;
        set
        {
            if (value)
            {
                _spriteRenderer.color = CustomColor.halfTransparentWhite;
                _transform.localPosition += _offSet;
            }
            else
            {
                _spriteRenderer.color = FaceUp ? Color.white : CustomColor.cardbackRed;
                _transform.localPosition -= _offSet;
            }

            _selected = value;
        }
    }
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();

        _spriteRenderer.color = FaceUp ? Color.white : CustomColor.cardbackRed;
    }
}