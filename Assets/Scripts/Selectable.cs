using UnityEngine;

public class Selectable : MonoBehaviour {

    [field: SerializeField]
    public bool FaceUp { get; set; } = false;

    public bool Selected
    {
        get => _selected;
        set
        {
            if (value)
            {
                _spriteRenderer.color = CustomColors.halfTransparentWhite;
                _transform.localPosition += _offSet;
            }
            else
            {
                _spriteRenderer.color = FaceUp ? Color.white : CustomColors.cardbackRed;
                _transform.localPosition -= _offSet;
            }

            _selected = value;
        } 
    }
    
    private readonly Vector3 _offSet = new Vector3(0f, 1f, 0f);
    [field: SerializeField]
    private bool _selected = false;
    private SpriteRenderer _spriteRenderer;
    private Transform _transform;

    void Start()
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _transform = this.GetComponent<Transform>();
        
        _spriteRenderer.color = FaceUp ? Color.white : CustomColors.cardbackRed;
    }
}
