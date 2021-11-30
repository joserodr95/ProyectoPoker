using System.Collections.Generic;
using UnityEngine;

public class UpdateSprite : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Selectable selectable;
    private UserInput userInput;
    //private Solitaire solitaire;

    [field: SerializeField]
    public Sprite CardFace { get; set; }
    [field: SerializeField]
    public Sprite CardBack { get; set; }

    //public void ForceUpdateSprite() {
    //    CardFace = Resources.Load<Sprite>("Sprites/Cards/PlayableCards/" + this.name);
    //}

    // Start is called before the first frame update
    void Start() {
        //solitaire = FindObjectOfType<Solitaire>();
        userInput = FindObjectOfType<UserInput>();

        CardFace = Resources.Load<Sprite>("Sprites/Cards/PlayableCards/" + this.name);

        spriteRenderer = GetComponent<SpriteRenderer>();
        selectable = GetComponent<Selectable>();
    }

    // Update is called once per frame
    void Update() {
        if (selectable.FaceUp) {
            spriteRenderer.sprite = CardFace;
        } else {
            spriteRenderer.sprite = CardBack;
        }
        CardFace = Resources.Load<Sprite>("Sprites/Cards/PlayableCards/" + this.name);
    }
}
