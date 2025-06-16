using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SolitaireCard : MonoBehaviour
{
    public string Suit { get; private set; }
    public int Rank { get; private set; }

    private bool isDragging = false;
    private Vector3 offset;

    // The glue offset when snapping to a higher card
    public Vector3 glueOffset = new Vector3(0, -0.3f, 0); // Adjust as needed

    private void Start()
    {
        ParseCardName();
    }

    private void ParseCardName()
    {
        string[] parts = name.Split('_');
        if (parts.Length == 2)
        {
            Suit = parts[0];
            if (!int.TryParse(parts[1], out int rank))
            {
                Debug.LogWarning($"Invalid rank in card name: {name}");
            }
            else
            {
                Rank = rank;
            }
        }
        else
        {
            Debug.LogWarning($"Invalid card name format: {name}");
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreen);
    }

    private void OnTriggerEnter(Collider other)
    {
        SolitaireCard otherCard = other.GetComponent<SolitaireCard>();
        if (otherCard != null && otherCard.Rank > this.Rank)
        {
            Vector3 targetPos = otherCard.transform.position + glueOffset;
            transform.position = targetPos;
            transform.SetParent(otherCard.transform); // Optional: parent for hierarchy
        }
    }
}