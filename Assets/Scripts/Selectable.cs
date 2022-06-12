using Helpers;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    private readonly Vector3 _offSetPlus = new Vector3(0f, 1f, 0f);
    private readonly Vector3 _offSetMinus = new Vector3(0f, -1f, 0f);
    [field: SerializeField] private bool _faceUp;
    [field: SerializeField] private bool _selected;
    [field: SerializeField] private bool _folded;
    private SpriteRenderer _spriteRenderer = null;
    private Transform _transform;

    public bool FaceUp
    {
        get => _faceUp;
        set
        {
            if (_spriteRenderer is object)
            {
                if (value)
                {
                    _spriteRenderer.color = Color.white;
                }
                else
                {
                    _spriteRenderer.color = CustomColor.cardbackRed;
                }
            }

            _faceUp = value;
        }
    }

    public bool Selected
    {
        get => _selected;
        set
        {
            if (value == _selected) return;

            int playerNum = GetComponent<InGameCardInfo>().playerOwner;

            if (value)
            {
                _spriteRenderer.color = playerNum == 1 || PokerManager.Instance.revealCpuCards
                    ? CustomColor.halfTransWhite
                    : CustomColor.cardbackRed;
                _transform.localPosition += (playerNum == 1 || playerNum == 4) ? _offSetPlus : _offSetMinus;
            }
            else
            {
                _spriteRenderer.color = playerNum == 1 || PokerManager.Instance.revealCpuCards
                    ? Color.white
                    : CustomColor.cardbackRed;
                _transform.localPosition -= (playerNum == 1 || playerNum == 4) ? _offSetPlus : _offSetMinus;
            }

            _selected = value;
        }
    }

    public bool Folded
    {
        get => _folded;
        set
        {
            if (value == _folded) return;

            int playerNum = GetComponent<InGameCardInfo>().playerOwner;

            if (value)
            {
                _spriteRenderer.color = CustomColor.halfTransDarkGrey;
                _transform.localPosition += (playerNum == 1 || playerNum == 4) ? _offSetMinus : _offSetPlus;
            }
            else
            {
                _spriteRenderer.color = playerNum == 1 || PokerManager.Instance.revealCpuCards
                    ? Color.white
                    : CustomColor.cardbackRed;
                _transform.localPosition -= (playerNum == 1 || playerNum == 4) ? _offSetMinus : _offSetPlus;
            }

            _folded = value;
        }
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();

        _spriteRenderer.color = FaceUp ? Color.white : CustomColor.cardbackRed;
    }
}