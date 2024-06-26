using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    [SerializeField] private Sprite unclickedTile;
    [SerializeField] private Sprite flaggedTile;
    [SerializeField] private List<Sprite> clickedTiles;
    [SerializeField] private Sprite mineTile;
    [SerializeField] private Sprite mineWrongTile;
    [SerializeField] private Sprite mineHitTile;

    public GameManager gameManager;

    private SpriteRenderer spriteRenderer;
    public bool flagged = false;
    public bool active = true;
    public bool isMine = false;
    public int mineCount = 0;

    private void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseOver()
    {
        if (Time.timeScale == 1)
        {
            if (Time.timeScale == 1)
            {
                if (active)
                {
                    var mouse = Mouse.current;
                    if (mouse != null)
                    {
                        if (mouse.leftButton.wasPressedThisFrame)
                        {
                            ClickedTile();
                        }
                        else if (mouse.rightButton.wasPressedThisFrame)
                        {
                            flagged = !flagged;
                            if (flagged)
                            {
                                spriteRenderer.sprite = flaggedTile;
                            }
                            else
                            {
                                spriteRenderer.sprite = unclickedTile;
                            }
                        }
                        else if (mouse.leftButton.isPressed && mouse.rightButton.isPressed)
                        {
                            gameManager.ExpandIfFlagged(this);
                        }
                    }
                }
            }
        }
    }

    public void ClickedTile() 
    {
        if (active & !flagged) 
        {
            active = false;
            if (isMine) 
            {
                spriteRenderer.sprite = mineHitTile;
                gameManager.GameOver();
            }
            else 
            {
                spriteRenderer.sprite = clickedTiles[mineCount];
                if (mineCount == 0)
                {
                    gameManager.ClickNeighbours(this);
                }
                gameManager.CheckGameOver();
            }
        }
    }

    public void ShowGameOverState()
    {
        if (active)
        {
            active = false;
            if (isMine & !flagged)
            {
                spriteRenderer.sprite = mineTile;
            }
            else if (flagged & !isMine)
            {
                spriteRenderer.sprite = mineWrongTile;
            }
        }
    }

    public void SetFlaggedIfMine()
    {
        if (isMine)
        {
            flagged = true;
            spriteRenderer.sprite = flaggedTile;
        }
    }
}