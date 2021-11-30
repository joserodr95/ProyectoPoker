using UnityEngine;

public class Selectable : MonoBehaviour {

    [field: SerializeField]
    public bool FaceUp { get; set; } = false;
    [field: SerializeField]
    public bool Selected { get; set; } = false;

    private Color halfTransparent = new Color(1f, 1f, 1f, 0.5f);
    private Color redBack = new Color(0.7735849f, 0.1788003f, 0.1788003f, 1f);

    // Update is called once per frame
    void Update() {
        if (Selected) {
            this.GetComponent<SpriteRenderer>().color = halfTransparent;
            this.GetComponent<Transform>().localPosition = new Vector3(this.GetComponent<Transform>().position.x, 1f, 0f);
        } else {
            if (FaceUp) {
                this.GetComponent<SpriteRenderer>().color = Color.white;
            } else {
                this.GetComponent<SpriteRenderer>().color = redBack;
            }
            this.GetComponent<Transform>().localPosition = new Vector3(this.GetComponent<Transform>().position.x, 0f, 0f);
        }
    }
}
