using UnityEngine;

public class Selectable : MonoBehaviour {

    public bool faceUp = false;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (faceUp) {
            this.GetComponent<SpriteRenderer>().color = Color.white;
        } else {
            this.GetComponent<SpriteRenderer>().color = new Color(0.7735849f, 0.1788003f, 0.1788003f, 1f);
        }
    }
}
