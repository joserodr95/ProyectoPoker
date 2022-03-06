using UnityEngine;
using static CustomColors;

public class Selectable : MonoBehaviour {

    [field: SerializeField]
    public bool FaceUp { get; set; } = false;
    [field: SerializeField]
    public bool Selected { get; set; } = false;

    // Update is called once per frame
    void Update() {
        if (Selected) {
            this.GetComponent<SpriteRenderer>().color = CustomColors.halfTransparentWhite;
            this.GetComponent<Transform>().localPosition = new Vector3(this.GetComponent<Transform>().position.x, 1f, 0f);
        } else {
            if (FaceUp) {
                this.GetComponent<SpriteRenderer>().color = Color.white;
            } else
            {
                this.GetComponent<SpriteRenderer>().color = CustomColors.cardbackRed;
            }
            this.GetComponent<Transform>().localPosition = new Vector3(this.GetComponent<Transform>().position.x, 0f, 0f);
        }
    }
}
