using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileInteraction : MonoBehaviour
{
    public Sprite tileHover;
    public Sprite tileUnhover;

    private BoxCollider2D collider;
    private SpriteRenderer spriteRenderer;

    //// Double click
    //private float clicked = 0;
    //private float clickTime = 0;
    //private float clickDelay = 0.5f;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = tileUnhover;
        spriteRenderer.sortingLayerName = "Background";
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Over UI");
        }
        else
        {
            Debug.Log("Not over UI");
            spriteRenderer.sprite = tileHover;
        }
    }
    private void OnMouseExit()
    {
        spriteRenderer.sprite = tileUnhover;
    }
    private void OnMouseOver()
    {
        if (/*DoubleClick()*/ Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject() && PlayerScript.isPlayerTurn)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D raycastHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (raycastHit.collider != null)
            {
                Debug.Log(string.Format("hey! You poked {0}", raycastHit.transform.gameObject.name));
                Move(raycastHit.transform.gameObject);
            }
        }
    }

    private void Move(GameObject clickedTile)
    {
        GameObject player = GameObject.Find("Player");

        int x = (int)player.transform.localPosition.x;
        int y = (int)player.transform.localPosition.y;


        //int groundSpeed = PlayerScript.playerGroundSpeed;
        //int seaSpeed = PlayerScript.playerSeaSpeed;
        //int mountainSpeed = PlayerScript.playerMountainSpeed;
        
        if (pathIsAvailable(clickedTile, x, y, /*groundSpeed, seaSpeed, mountainSpeed,*/ player))
        {
            Debug.Log(string.Format("LandSpeed: {0}, SeaSpeed: {1}, MountainSpeed: {2}", PlayerScript.playerGroundSpeed, PlayerScript.playerSeaSpeed, PlayerScript.playerMountainSpeed));
            player.transform.localPosition = clickedTile.transform.localPosition;
        }
    }    

    private bool pathIsAvailable(GameObject clickedTile, int currentX, int currentY, /*int currentGroundSpeed, int currentSeaSpeed, int currentMountainSpeed,*/ GameObject player)
    {

        if (currentX == (int)clickedTile.transform.localPosition.x && currentY == (int)clickedTile.transform.localPosition.y)
        {
            return true;
        }

        int endingX = (int)clickedTile.transform.localPosition.x;
        int endingY = (int)clickedTile.transform.localPosition.y;
        List<GameObject> nearbyPathTiles;

        nearbyPathTiles = new List<GameObject>();
        GameObject sideTile;
        for (int i = 0; i < 4; i++)
        {
            int direction = (int)Math.Pow(-1, i);

            if (i <= 1)
            {
                sideTile = GameObject.Find(string.Format("tile_x{0}_y{1}", currentX + 1 * direction, currentY));
            }
            else
            {
                sideTile = GameObject.Find(string.Format("tile_x{0}_y{1}", currentX, currentY + 1 * direction));
            }

            if (sideTile != null && 
                    ((sideTile.transform.parent.name == "GroundTile" && PlayerScript.playerGroundSpeed != 0)  || 
                    (sideTile.transform.parent.name == "SeaTile" && PlayerScript.playerSeaSpeed != 0) || 
                    (sideTile.transform.parent.name == "MountainTile" && PlayerScript.playerMountainSpeed != 0)))
            {
                nearbyPathTiles.Add(sideTile);
            }

        }

        if (PlayerScript.playerGroundSpeed == 0 && PlayerScript.playerSeaSpeed == 0 && PlayerScript.playerMountainSpeed == 0)
        {
            return false;
        }

        if (nearbyPathTiles.Count == 0 && transform.localPosition != clickedTile.transform.localPosition)
        {
            return false;
        }

        int shortestPath = 0;
        float lowestDistance = Vector3.Distance(nearbyPathTiles[0].transform.localPosition, clickedTile.transform.localPosition); 

        if (nearbyPathTiles.Count > 1)
        {
            for (int i = 1; i < nearbyPathTiles.Count; i++)
            {
                //if (pathIsAvailable(clickedTile, (int)nearbyPathTiles[i].transform.localPosition.x, (int)nearbyPathTiles[i].transform.localPosition.y) && i == nearbyPathTiles.Count)
                //{
                //    return false;
                //}

                if (Vector3.Distance(nearbyPathTiles[i].transform.localPosition, clickedTile.transform.localPosition) < lowestDistance)
                {
                    lowestDistance = Vector3.Distance(nearbyPathTiles[i].transform.localPosition, clickedTile.transform.localPosition);
                    shortestPath = i;
                }
            }
        }
        
        switch (nearbyPathTiles[shortestPath].transform.parent.name)
        {
            case "GroundTile": PlayerScript.playerGroundSpeed--; break;
            case "SeaTile": PlayerScript.playerSeaSpeed--; break;
            case "MountainTile": PlayerScript.playerMountainSpeed--; break;
        }

        if (pathIsAvailable(clickedTile, (int)nearbyPathTiles[shortestPath].transform.localPosition.x, (int)nearbyPathTiles[shortestPath].transform.localPosition.y, /*currentGroundSpeed, currentSeaSpeed, currentMountainSpeed,*/ player))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // FIX DOUBLE CLICK -- THIS SCRIPT DOESN'T ALWAYS WORK
    //private bool DoubleClick()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        clicked++;
    //        if (clicked == 1)
    //        {
    //            clickTime = Time.time;
    //        }
    //    }
    //    if (clicked > 1 && Time.time - clickTime < clickDelay)
    //    {
    //        clicked = 0;
    //        clickTime = 0;
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}


}
