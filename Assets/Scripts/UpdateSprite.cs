using UnityEngine;

public class UpdateSprite : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Selectable selectable;
    private UserInput userInput;
    private CardComponent cardComponent;

    [field: SerializeField]
    public Sprite CardFace { get; set; }
    [field: SerializeField]
    public Sprite CardBack { get; set; }
    
    void Start() {
        userInput = FindObjectOfType<UserInput>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        selectable = GetComponent<Selectable>();
        cardComponent = GetComponent<CardComponent>();

        CardFace = Resources.Load<Sprite>("Sprites/Cards/PlayableCards/" + cardComponent.card.name);
    }

    // Update is called once per frame
    void Update() {
        spriteRenderer.sprite = selectable.FaceUp ? CardFace : CardBack;

        CardFace = Resources.Load<Sprite>($"Sprites/Cards/PlayableCards/{cardComponent.card.name}");
        this.gameObject.name = cardComponent.card.name;
    }
}
