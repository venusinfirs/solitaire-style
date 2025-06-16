using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class SolitaireCard2D : MonoBehaviour
{
    public string Suit { get; private set; }
    public int Rank { get; private set; }

    public Vector3 glueOffset = new Vector3(0, -0.3f, 0); // Local offset for snapping

    private bool isDragging = false;
    private Vector3 offset;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;

        ParseCardName();
    }

    private void ParseCardName()
    {
        string[] parts = name.Split('_');
        if (parts.Length == 2)
        {
            Suit = parts[0];
            if (!int.TryParse(parts[1], out int parsedRank))
            {
                Debug.LogWarning($"Invalid rank in card name: {name}");
            }
            else
            {
                Rank = parsedRank;
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
            rb.MovePosition(GetMouseWorldPosition() + offset);
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = 10f; // You can adjust this if needed
        return Camera.main.ScreenToWorldPoint(mouseScreen);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDragging) return;

        SolitaireCard2D otherCard = other.GetComponent<SolitaireCard2D>();
        if (otherCard != null && otherCard != this && otherCard.Rank > this.Rank)
        {
            transform.position = otherCard.transform.position + glueOffset;
            transform.SetParent(otherCard.transform); // Optional parenting
        }
    }
}
