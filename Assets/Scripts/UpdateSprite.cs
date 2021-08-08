using System.Collections.Generic;
using UnityEngine;

public class UpdateSprite : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Selectable selectable;

    [field: SerializeField]
    public Sprite CardFace { get; set; }
    [field: SerializeField]
    public Sprite CardBack { get; set; }

    public Solitaire Solitaire { get; set; }

    // Start is called before the first frame update
    void Start() {
        List<string> deck = Solitaire.GenerateDeck();
        Solitaire = FindObjectOfType<Solitaire>();

        int i = 0;
        foreach (string card in deck) {
            if (this.name == card) {
                CardFace = Solitaire.CardFaces[i];
                break;
            }
            i++;
        }
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
    }
}
