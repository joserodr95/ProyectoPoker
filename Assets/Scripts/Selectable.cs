using UnityEngine;

public class Selectable : MonoBehaviour {

    [field: SerializeField]
    public bool FaceUp { get; set; } = false;
    [field: SerializeField]
    public bool Selected { get; set; } = false;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        if (Selected) {
            this.GetComponent<SpriteRenderer>().color = Color.yellow;
        } else {
            if (FaceUp) {
                this.GetComponent<SpriteRenderer>().color = Color.white;
            } else {
                this.GetComponent<SpriteRenderer>().color = new Color(0.7735849f, 0.1788003f, 0.1788003f, 1f);
            }
        }
    }
}
