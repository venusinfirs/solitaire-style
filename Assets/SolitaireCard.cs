using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class SolitaireCard2D : MonoBehaviour
{
    public Vector3 InitialPosition { get; set; }
    public SolitaireCard2D CurrentHigherCard { get; set; }
    public SolitaireCard2D PreviousHigherCard { get; set; }

    public Stack<SolitaireCard2D> LoverCards = new Stack<SolitaireCard2D>();
    public string Suit { get; private set; }
    public int Rank { get; private set; }

    public float glueOffset =  -0.5f; // Local offset for snapping
    
    private bool isDragging = false;
    private Vector3 offset;

    private Rigidbody2D rb;

    public event Action<LastMoveData> OnCardPlaced;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;
        
        ParseCardName();
        
        InitialPosition = transform.position;
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
        if (CurrentHigherCard != null && CurrentHigherCard.LoverCards.Count > 0)
        {
            return;
        }

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
        
        if (CurrentHigherCard == null) return;
        
        transform.position =
            new Vector3(CurrentHigherCard.transform.position.x, CurrentHigherCard.transform.position.y + glueOffset);
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
        if (otherCard != null && otherCard != this && otherCard.Rank > Rank)
        {
            OnCardPlaced?.Invoke(new LastMoveData()
            {
                LastDraggedCard = this,
                PreviousHigherCard = CurrentHigherCard,
                InitialPosition = InitialPosition,
                CurrentHigherCard = otherCard
            });

            PreviousHigherCard = CurrentHigherCard;
            CurrentHigherCard = otherCard;
        }
    }
}
