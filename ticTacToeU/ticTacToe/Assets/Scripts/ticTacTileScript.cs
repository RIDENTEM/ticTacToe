﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ticTacTileScript : MonoBehaviour
{

    Button thisButton;
    private RawImage thisButtonImage;
    bool tileClicked;
    ticTacManager gameManager;
    public ticTacManager.Players playerSide;
    private void Start()
    {
        gameManager = FindObjectOfType<ticTacManager>();
        thisButton = GetComponent<Button>();
        thisButtonImage = GetComponent<RawImage>();
        tileClicked = false;
        playerSide = ticTacManager.Players.None;
    }

    public ticTacManager.Players getPlayerType()
    {
        return playerSide;
    }

    public void onTileClicked()
    {
        Debug.LogFormat(ticTacManager.currentPlayer.ToString());

        if (!tileClicked)
        {
            //if its player hearts turn and they press a tile
            if (ticTacManager.currentPlayer == ticTacManager.Players.Heart)
            {
                //set the image of that tile to a heart 
                if (gameManager)
                    thisButtonImage.texture = gameManager.getHeartImage();
                //sets the playerSide for this tile correctly on click
                playerSide = ticTacManager.Players.Heart;
                //function to store moves whenever a tile is hit, also finds the index of the current tile in the tiles array
                gameManager.storeMoves(System.Array.IndexOf(gameManager.gridButtons3x3, gameObject.GetComponent<Button>()));
                //set the current player to chip
                ticTacManager.currentPlayer = ticTacManager.Players.Chip;
            }
            //otherwise if its chip players turn
            else if (ticTacManager.currentPlayer == ticTacManager.Players.Chip)
            {
                //set the tile image to a chip
                if (gameManager)
                    thisButtonImage.texture = gameManager.getChipImage();
                //started using playerSide to check against what tiles are set instead of textures because it was a bit finicky
                playerSide = ticTacManager.Players.Chip;
                //function to store moves whenever a tile is hit, also finds the index of the current tile in the tiles array
                gameManager.storeMoves(System.Array.IndexOf(gameManager.gridButtons3x3, gameObject.GetComponent<Button>()));
                //set the current player to heart
                ticTacManager.currentPlayer = ticTacManager.Players.Heart;
            }
            tileClicked = true;
            ticTacManager.overallTurnNumber++;
        }
        else
        {
            //play some in/out animation make it look nice
        }


        thisButton.interactable = false;



    }


}
