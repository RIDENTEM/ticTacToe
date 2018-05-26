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

    private void Start()
    {
        gameManager = FindObjectOfType<ticTacManager>();
        thisButton = GetComponent<Button>();
        thisButtonImage = GetComponent<RawImage>();
        tileClicked = false;
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


                //set the current player to chip
                ticTacManager.currentPlayer = ticTacManager.Players.Chip;
            }
            //otherwise if its chip players turn
            else
            {
                //set the tile image to a chip
                if (gameManager)
                    thisButtonImage.texture = gameManager.getChipImage();
                //set the current player to heart
                ticTacManager.currentPlayer = ticTacManager.Players.Heart;

            }
            tileClicked = true;
        }
        else
        {
            //play some in/out animation make it look nice
        }


        thisButton.interactable = false;



    }
    

}
