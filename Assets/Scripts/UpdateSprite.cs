using System.Collections.Generic;
using UnityEngine;

public class UpdateSprite : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Selectable selectable;
    private UserInput userInput;
    private Solitaire solitaire;

    [field: SerializeField]
    public Sprite CardFace { get; set; }
    [field: SerializeField]
    public Sprite CardBack { get; set; }

    // Start is called before the first frame update
    void Start() {
        List<string> deck = new List<string>(Solitaire.MazoBase.Keys);//Solitaire.RellenaElMazo();
        solitaire = FindObjectOfType<Solitaire>();
        userInput = FindObjectOfType<UserInput>();

        CardFace = Resources.Load<Sprite>("Sprites/Cartas/CartasJugables/" + this.name);//solitaire.CardFaces[i];

        //int i = 0;
        //foreach (string card in deck) {
        //    //if (this.name == card) {
        //        CardFace = Resources.Load<Sprite>("Sprites/Cartas/CartasJugables/" + card);//solitaire.CardFaces[i];
        //        //break;
        //    //}
        //    i++;
        //}
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
