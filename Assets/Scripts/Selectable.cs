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

    // Update is called once per frame
    // void Update() 
    // {
    //     if (Selected) 
    //     {
    //         this.GetComponent<SpriteRenderer>().color = CustomColors.halfTransparentWhite;
    //         // this.GetComponent<Transform>().localPosition = new Vector3(this.GetComponent<Transform>().position.x, 1f, 0f);
    //     } 
    //     else
    //     {
    //         this.GetComponent<SpriteRenderer>().color = FaceUp ? Color.white : CustomColors.cardbackRed;
    //         // this.GetComponent<Transform>().localPosition = new Vector3(this.GetComponent<Transform>().position.x, 0f, 0f);
    //     }
    // }
}
