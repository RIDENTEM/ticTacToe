﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ticTacManager : MonoBehaviour
{

    public static bool gameWon;

    class PlayerMoves
    {
        public Players savedPlayer;
        public int[,] tileHit;
        private int turnNumber = overallTurnNumber;
    }





    public static bool isNormalGame;
    static public int overallTurnNumber = 1;

    private GameObject[,] gridTiles3x3 = new GameObject[3, 3];
    private GameObject[,] gridTiles4x4 = new GameObject[4, 4];




    public enum Players { None, Jerry, Kramer, George, Elaine, Newman };
    public static Player currentPlayer;
    public static Player player1;
    public static Player player2;

    [SerializeField] private GameObject panel3x3;
    [SerializeField] private GameObject panel4x4;
    public Text playerTurnText;
    public Button[] gridButtons3x3;
    public Button[] gridButtons4x4;

    private List<PlayerMoves> playerMovesToSave;
    private winLossScript winLossObject;

    private void Start()
    {
        currentPlayer = player1;
        playerTurnText.text = currentPlayer.playerTurnText;
        gameWon = false;
        winLossObject = FindObjectOfType<winLossScript>();
        playerMovesToSave = new List<PlayerMoves>();
        playerMovesToSave.Capacity = 3;
        populateTilesArray();
        typeOfGrid();
    }



    void typeOfGrid()
    {
        if (isNormalGame)
        {
            panel3x3.SetActive(true);
            panel4x4.SetActive(false);
        }
        else if (!isNormalGame)
        {
            panel3x3.SetActive(false);
            panel4x4.SetActive(true);
        }
    }

    public void displayPlayerTurn(Player currPlayer)
    {
        playerTurnText.text = currPlayer.playerTurnText;
    }

    public void genericWinCheck(int rowSize)
    {
        GameObject[,] tileArray;
        if (isNormalGame)
            tileArray = gridTiles3x3;
        else
            tileArray = gridTiles4x4;
        //This is a row win
        for (int columnCheck = 0; columnCheck < rowSize; columnCheck++)
            for (int rowCheck = 0; rowCheck < rowSize; rowCheck++)
            {

                if (rowCheck == rowSize - 1 && tileArray[rowCheck - 1, columnCheck].GetComponent<ticTacTileScript>().playerSide == tileArray[rowCheck, columnCheck].GetComponent<ticTacTileScript>().playerSide &&
                    tileArray[rowCheck - 1, columnCheck].GetComponent<ticTacTileScript>().playerSide != Players.None && tileArray[rowCheck, columnCheck].GetComponent<ticTacTileScript>().playerSide != Players.None)
                {
                    //gameWon!
                    Debug.Log("Win for one of the rows");
                    winLossObject.gameWon();
                }
                if (rowCheck != rowSize - 1)
                    if (tileArray[rowCheck, columnCheck].GetComponent<ticTacTileScript>().playerSide == tileArray[rowCheck + 1, columnCheck].GetComponent<ticTacTileScript>().playerSide &&
                        tileArray[rowCheck, columnCheck].GetComponent<ticTacTileScript>().playerSide != Players.None && tileArray[rowCheck + 1, columnCheck].GetComponent<ticTacTileScript>().playerSide != Players.None)
                        continue;
                    else
                        break;
            }

        //This should be an entire working check for the columns, and if one of the columns is a winner then it will go through the win process
        for (int rowCheck = 0; rowCheck < rowSize; rowCheck++)
            for (int columnCheck = 0; columnCheck < rowSize; columnCheck++)
            {
                if (columnCheck == rowSize - 1 && tileArray[rowCheck, columnCheck].GetComponent<ticTacTileScript>().playerSide == tileArray[rowCheck, columnCheck - 1].GetComponent<ticTacTileScript>().playerSide &&
          tileArray[rowCheck, columnCheck].GetComponent<ticTacTileScript>().playerSide != Players.None && tileArray[rowCheck, columnCheck - 1].GetComponent<ticTacTileScript>().playerSide != Players.None)
                {
                    //gameWon!
                    Debug.Log("Win for one of the columns");
                    winLossObject.gameWon();
                }
                if (columnCheck != rowSize - 1)
                {
                    if (tileArray[rowCheck, columnCheck].GetComponent<ticTacTileScript>().playerSide == tileArray[rowCheck, columnCheck + 1].GetComponent<ticTacTileScript>().playerSide &&
                        tileArray[rowCheck, columnCheck].GetComponent<ticTacTileScript>().playerSide != Players.None 
                        && tileArray[rowCheck, columnCheck + 1].GetComponent<ticTacTileScript>().playerSide != Players.None)
                        continue;
                    else
                        break;
                }
            }

        int columnCount = 0;
        int rowCount = 0;
        List<int> tileIndexes = new List<int>();
        //check for win from diagonal where each index x and y are equal to each other
        //new and improved for loop, I didn't even know I could increment two variables at once, so never let it be said I learned nothing at BCC
        for (; rowCount < rowSize; rowCount++, columnCount++)
        {

            if (rowCount != rowSize - 1)
            {
                if (tileArray[rowCount, columnCount].GetComponent<ticTacTileScript>().playerSide == tileArray[rowCount + 1, columnCount + 1].GetComponent<ticTacTileScript>().playerSide &&
                tileArray[rowCount, columnCount].GetComponent<ticTacTileScript>().playerSide != Players.None && tileArray[rowCount + 1, columnCount + 1].GetComponent<ticTacTileScript>().playerSide != Players.None)
                {
                    tileIndexes.Add(rowCount);
                    continue;
                }
                else
                {
                    tileIndexes.Clear();
                    break;
                }
            }

            if (rowCount == rowSize - 1 && tileArray[rowCount, columnCount].GetComponent<ticTacTileScript>().playerSide == tileArray[rowCount - 1, columnCount - 1].GetComponent<ticTacTileScript>().playerSide &&
          tileArray[rowCount, columnCount].GetComponent<ticTacTileScript>().playerSide != Players.None && tileArray[rowCount - 1, columnCount - 1].GetComponent<ticTacTileScript>().playerSide != Players.None)
            {
                //gameWon!
                Debug.Log("Win for the diagonal starting from the left");
                winLossObject.gameWon();
            }
        }
        columnCount = 0;
        for (rowCount = rowSize - 1; rowCount < rowSize; rowCount--, columnCount++)
        {
            if (columnCount != rowSize - 1)
            {
                if (tileArray[rowCount, columnCount].GetComponent<ticTacTileScript>().playerSide == tileArray[rowCount - 1, columnCount + 1].GetComponent<ticTacTileScript>().playerSide &&
                   tileArray[rowCount, columnCount].GetComponent<ticTacTileScript>().playerSide != Players.None && tileArray[rowCount - 1, columnCount + 1].GetComponent<ticTacTileScript>().playerSide != Players.None)
                    continue;
                else
                    break;
            }
            if (columnCount == rowSize - 1 && tileArray[rowCount, columnCount].GetComponent<ticTacTileScript>().playerSide == tileArray[rowCount + 1, columnCount - 1].GetComponent<ticTacTileScript>().playerSide &&
          tileArray[rowCount, columnCount].GetComponent<ticTacTileScript>().playerSide != Players.None && tileArray[rowCount + 1, columnCount - 1].GetComponent<ticTacTileScript>().playerSide != Players.None)
            {
                //gameWon!
                Debug.Log("Win for the diagonal starting from the right");
                winLossObject.gameWon();
            }
        }
        if (overallTurnNumber == rowSize * rowSize && gameWon == false)
            winLossObject.gameTied();
    }

    void populateTilesArray()
    {
        if (isNormalGame)
        {
            for (int i = 0; i < 9; i++)
            {
                gridButtons3x3[i].GetComponent<ticTacTileScript>().playerSide = Players.None;
            }
            int gridButtonCounter3x3 = 0;
            //use the 3x3
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    //pretty sure I have to switch these because I check the positions of the array by x,y ([0,1],[0,2],[0,3]) / not y,x
                    gridTiles3x3[j, i] = gridButtons3x3[gridButtonCounter3x3].gameObject;

                    gridButtonCounter3x3++;
                }
            }
        }
        else
        {
            for (int i = 0; i < 16; i++)
            {
                gridButtons4x4[i].GetComponent<ticTacTileScript>().playerSide = Players.None;
            }
            int gridButtonCounter4x4 = 0;
            //use the 4x4
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    gridTiles4x4[j, i] = gridButtons4x4[gridButtonCounter4x4].gameObject;
                    gridButtonCounter4x4++;
                }
            }
        }



    }


    public void storeMoves(int tileIndex)
    {
        PlayerMoves newPlayerMove = new PlayerMoves();
        newPlayerMove.savedPlayer = currentPlayer.playerCharacter;
        if (isNormalGame)
            switch (tileIndex)
            {
                case 0:
                    newPlayerMove.tileHit = new int[0, 0];
                    break;
                case 1:
                    newPlayerMove.tileHit = new int[1, 0];
                    break;
                case 2:
                    newPlayerMove.tileHit = new int[2, 0];
                    break;
                case 3:
                    newPlayerMove.tileHit = new int[0, 1];
                    break;
                case 4:
                    newPlayerMove.tileHit = new int[1, 1];
                    break;
                case 5:
                    newPlayerMove.tileHit = new int[2, 1];
                    break;
                case 6:
                    newPlayerMove.tileHit = new int[0, 2];
                    break;
                case 7:
                    newPlayerMove.tileHit = new int[1, 2];
                    break;
                case 8:
                    newPlayerMove.tileHit = new int[2, 2];
                    break;
            }
        else if (!isNormalGame)
            switch (tileIndex)
            {
                case 0:
                    newPlayerMove.tileHit = new int[0, 0];
                    break;
                case 1:
                    newPlayerMove.tileHit = new int[1, 0];
                    break;
                case 2:
                    newPlayerMove.tileHit = new int[2, 0];
                    break;
                case 3:
                    newPlayerMove.tileHit = new int[3, 0];
                    break;
                case 4:
                    newPlayerMove.tileHit = new int[0, 1];
                    break;
                case 5:
                    newPlayerMove.tileHit = new int[1, 1];
                    break;
                case 6:
                    newPlayerMove.tileHit = new int[2, 1];
                    break;
                case 7:
                    newPlayerMove.tileHit = new int[3, 1];
                    break;
                case 8:
                    newPlayerMove.tileHit = new int[0, 2];
                    break;
                case 9:
                    newPlayerMove.tileHit = new int[1, 2];
                    break;
                case 10:
                    newPlayerMove.tileHit = new int[2, 2];
                    break;
                case 11:
                    newPlayerMove.tileHit = new int[3, 2];
                    break;
                case 12:
                    newPlayerMove.tileHit = new int[3, 0];
                    break;
                case 13:
                    newPlayerMove.tileHit = new int[3, 1];
                    break;
                case 14:
                    newPlayerMove.tileHit = new int[3, 2];
                    break;
                case 15:
                    newPlayerMove.tileHit = new int[3, 3];
                    break;

            }

        playerMovesToSave.Add(newPlayerMove);
    } 
}
