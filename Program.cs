﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;

namespace boardClassdemo
{

    class Player
    {
        int[] players = new int[4] { 1, 2, 3, 4 };
        int[] positions = new int[4] { 1, 1, 1, 1 };
        string[] name = new string[4];
        Random rnd = new Random();

        public int[] Players
        {
            get
            {
                return players;
            }
            set
            {
                players = value;
            }
        }

        public int[] Positions
        {
            get
            {
                return positions;
            }
            set
            {
                positions = value;
            }
        }
        public string[] Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }


        public void UpdatePosition(int number, int diceValue)
        {
            number--;
            positions[number] = positions[number] + diceValue;
            // Console.WriteLine(positions[number]);
        }

        public bool ValidateString(ref string name)//validating first and last name so that user does not enter any numeric character
        {
            bool flag = false;
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                Console.Write("Enter Valid Name: ");
                name = Console.ReadLine();
                ValidateString(ref name);
                flag = true;
            }
            return flag;
        }

        public int SnakeLadder(int number)
        {
            int newPosition = 0;
            if (positions[number] == 28)//Reaching to 9 
            {
                newPosition = -19;
            }
            if (positions[number] == 36)//Reaching to 3
            {

                newPosition = -33;
            }
            if (positions[number] == 53)//Reaching to 11
            {

                newPosition = -11;
            }
            if (positions[number] == 63)//Reaching to 20
            {

                newPosition = -43;
            }
            if (positions[number] == 76)//Reaching to 6
            {

                newPosition = -70;
            }
            if (positions[number] == 88)//Reaching to 49
            {

                newPosition = -39;
            }
            if (positions[number] == 96)//Reaching to 47
            {

                newPosition = -49;
            }
            if (positions[number] == 99)//Reaching to 39
            {

                newPosition = -60;
            }/////////////////////////////////////////Ladder Start
            if (positions[number] == 5)//Reaching to 39
            {

                newPosition = 22;
            }
            if (positions[number] == 12)//Reaching to 39
            {

                newPosition = 21;
            }
            if (positions[number] == 17)//Reaching to 39
            {

                newPosition = 26;
            }
            if (positions[number] == 31)//Reaching to 39
            {

                newPosition = 38;
            }
            if (positions[number] == 40)//Reaching to 39
            {

                newPosition = 38;
            }
            if (positions[number] == 46)//Reaching to 39
            {

                newPosition = 28;
            }
            if (positions[number] == 56)//Reaching to 39
            {

                newPosition = 42;
            }
            if (positions[number] == 71)//Reaching to 39
            {

                newPosition = 21;
            }
            return newPosition;
        }//for user manual value

    }

    class Cell
    {
        public int number = 0;
        public ConsoleColor[] playerColors = new ConsoleColor[4] { ConsoleColor.Cyan, ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green };
        public Cell()
        {

        }

        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }

        public char[] playerPosition = new char[4] { ' ', ' ', ' ', ' ' };

        public void setPlayerPosition(int player)
        {
            player--;
            playerPosition[player] = (char)1;
        }

        public void resetPlayerPosition(int player)
        {
            player--;
            playerPosition[player] = ' ';
        }

        public void PrintPlayerPositions()
        {
            Console.Write((char)166 + " ");
            for (int i = 0; i < playerColors.Length; i++)
            {
                Console.ForegroundColor = playerColors[i];
                if (playerPosition[i] != ' ')
                {
                    Console.Write(playerPosition[i]);
                }
                else
                {
                    Console.Write("_");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write(" " + (char)166);

        }

    }
    class GameBoard
    {
        Cell[] cells = new Cell[100];
        public Player players = new Player();
        Save saveGame = new Save();
        char[] diceValue = new char[9] { ' ', ' ', ' ', ' ', '0', ' ', ' ', ' ', ' ' };
        int playerWon = 0;
        bool typeOfDice = false;


        public GameBoard(bool newGameOrNot, bool diceType, string[] playerName)
        {
            players.Name = playerName;
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = new Cell();
                cells[i].Number = i + 1;
            }

            if (newGameOrNot)
            {
                for (int i = 1; i <= 4; i++)
                {
                    cells[0].setPlayerPosition(i);
                }

                Console.Clear();
                PrintBoard();
                PrintDice(diceValue);
                PrintPlayerPositionTable();
                // RollDice(0,diceType);
            }

        }

        public bool UpdatePlayer(int player, int diceValue)
        {
            bool flag = false;
            //  Console.WriteLine("Plyaer " + player + " Position " + players.Positions[player] + " is going change and dvalue " + diceValue);
            if (players.Positions[player] - 1 < 100)
            {
                cells[players.Positions[player] - 1].resetPlayerPosition(player + 1);
            }
            players.UpdatePosition(player + 1, diceValue);

            if ((players.Positions[player] - 1) >= 100)
            {
                cells[99].setPlayerPosition(player + 1);
                playerWon = player;
                flag = true;
            }
            else
            {
                cells[players.Positions[player] - 1].setPlayerPosition(player + 1);
            }
            return flag;
        }
        public void PrintCellNumber(int number)
        {

            number--;
            if (number < 10 && number != 100)
            {
                Console.Write((char)166 + "  " + string.Format("{0:00}", cells[number].Number) + "  " + (char)166);
            }
            else if (number == 99)
            {
                Console.Write((char)166 + " " + cells[number].Number + "  " + (char)166);
            }
            else
            {
                Console.Write((char)166 + "  " + cells[number].Number + "  " + (char)166);
            }


        }

        public void PrintNumberRow(int start, int end, bool ascending)
        {

            if (!ascending)
            {
                for (int i = start; i >= end; i--)
                {
                    PrintCellNumber(i);

                }
            }
            else
            {
                for (int i = end; i <= start; i++)
                {
                    PrintCellNumber(i);

                }
            }
            Console.WriteLine();
        }

        public void PrintEmptyRow(bool withDash)
        {
            for (int i = 10; i >= 1; i--)
            {
                if (withDash)
                {
                    Console.Write((char)166 + "------" + (char)166);
                }
                else
                {
                    Console.Write((char)166 + "      " + (char)166);
                }
            }
            Console.WriteLine();
        }

        public void PrintPlayers(int start, int end, bool ascending)
        {
            if (!ascending)
            {
                for (int i = start; i >= end; i--)
                {
                    int temp = i - 1;
                    cells[temp].PrintPlayerPositions();
                }
            }
            else
            {
                for (int i = end; i <= start; i++)
                {
                    int temp = i - 1;
                    cells[temp].PrintPlayerPositions();

                }
            }
            Console.WriteLine();
        }

        public void PrintBoard()
        {
            bool swapNumber = false;
            int start = 0;
            int end = 101;

            PrintEmptyRow(true);
            for (int i = 91; i >= 1; i = i - 10)
            {
                start = end - 1;
                end = i;

                PrintNumberRow(start, end, swapNumber);
                PrintPlayers(start, end, swapNumber);

                PrintEmptyRow(true);

                if (swapNumber)
                {
                    swapNumber = false;
                }
                else
                {
                    swapNumber = true;
                }

            }


            //PrintDice(diceValue);
        }

        public bool RollDice(int playerNumber, bool diceType)
        {
            Random rnd = new Random();
            int randomDiceValue = 0;
            bool flag = false;
            typeOfDice = diceType;
            bool saveOrNot = false;
            if (playerWon == 0)
            {
                if (!diceType)                                               //manual
                {
                    Console.WriteLine("Enter dice Value:");                        //getting manual value of dice
                    ConsoleKeyInfo currentKey = Console.ReadKey(true);
                    switch (currentKey.Key)
                    {
                        case ConsoleKey.Escape:
                            saveOrNot = true;
                            break;
                        case ConsoleKey.D1:
                            randomDiceValue = 1;
                            break;
                        case ConsoleKey.D2:
                            randomDiceValue = 2;
                            break;
                        case ConsoleKey.D3:
                            randomDiceValue = 3;
                            break;
                        case ConsoleKey.D4:
                            randomDiceValue = 4;
                            break;
                        case ConsoleKey.D5:
                            randomDiceValue = 5;
                            break;
                        case ConsoleKey.D6:
                            randomDiceValue = 6;
                            break;
                        default:
                            Console.WriteLine("Press any number from 1 to 6 only");
                            break;
                    }
                    Console.Clear();
                    diceValue = SetDiceShape(randomDiceValue);
                    PrintBoard();
                    PrintDice(diceValue);
                    PrintPlayerPositionTable();
                }
                else                                                       //automatic
                {
                    Console.WriteLine("Press SpaceBar to roll dice.");
                    ConsoleKeyInfo currentKey = Console.ReadKey(true);
                    switch (currentKey.Key)
                    {
                        case ConsoleKey.Spacebar:
                            for (int i = 0; i < 3; i++)
                            {
                                Console.Clear();
                                randomDiceValue = rnd.Next(1, 7);
                                diceValue = SetDiceShape(randomDiceValue);
                                PrintBoard();
                                PrintDice(diceValue);
                                PrintPlayerPositionTable();
                                Thread.Sleep(500);
                                Console.Clear();
                            }
                            break;
                        case ConsoleKey.Escape:
                            saveOrNot = true;
                            break;
                        default:
                            Console.WriteLine("Press spacebar to roll the dice and Esc to save the game");
                            break;

                    }
                }
                flag = UpdatePlayer(playerNumber, randomDiceValue);
                int newPosition = players.SnakeLadder(playerNumber);
                flag = UpdatePlayer(playerNumber, newPosition);
                Console.Clear();
                PrintBoard();
                PrintDice(diceValue);
                PrintPlayerPositionTable();
                if (newPosition < 0)
                {
                    Console.WriteLine(players.Name[playerNumber] + " caught by snake.");
                }
                else if (newPosition > 0)
                {
                    Console.WriteLine(players.Name[playerNumber] + " got ladder.");
                }

            }
            else
            {
                Console.WriteLine("Player " + playerWon + "Won The Game");
                flag = true;
            }
            if(saveOrNot == true)
            {
                saveGameData();
                return saveOrNot;
            }
            return flag;
        }

        public char[] SetDiceShape(int randomDiceValue)
        {

            switch (randomDiceValue)
            {
                case 1:
                    diceValue = new char[9] { ' ', ' ', ' ', ' ', '0', ' ', ' ', ' ', ' ' };
                    break;
                case 2:
                    diceValue = new char[9] { ' ', ' ', '0', ' ', ' ', ' ', '0', ' ', ' ' };
                    break;
                case 3:
                    diceValue = new char[9] { '0', ' ', ' ', ' ', '0', ' ', ' ', ' ', '0' };
                    break;
                case 4:
                    diceValue = new char[9] { '0', ' ', '0', ' ', ' ', ' ', '0', ' ', '0' };
                    break;
                case 5:
                    diceValue = new char[9] { '0', ' ', '0', ' ', '0', ' ', '0', ' ', '0' };
                    break;
                case 6:
                    diceValue = new char[9] { '0', ' ', '0', '0', ' ', '0', '0', ' ', '0' };
                    break;
                default:
                    diceValue = new char[9] { ' ', ' ', ' ', ' ', '0', ' ', ' ', ' ', ' ' };
                    break;

            }
            return diceValue;
        }
        public void PrintDice(char[] diceValue)
        {
            if (diceValue != null)
            {
                Console.WriteLine("\n\n\n\t\t\t\t+-----+");
                Console.WriteLine("\t\t\t\t| " + diceValue[0] + diceValue[1] + diceValue[2] + " |");
                Console.WriteLine("\t\t\t\t| " + diceValue[3] + diceValue[4] + diceValue[5] + " |");
                Console.WriteLine("\t\t\t\t| " + diceValue[6] + diceValue[7] + diceValue[8] + " |");
                Console.WriteLine("\t\t\t\t+-----+\n\n\n");
            }
        }

        public void Instantiate(int playerOnePosition, int playerTwoPosition, int playerThreePosition, int playerFourPosition, int diceType)
        {
            int[] temp = new int[4] { playerOnePosition, playerTwoPosition, playerThreePosition, playerFourPosition };
            for (int i = 0; i < players.Positions.Length; i++)
            {
                players.Positions[i] = temp[i];
            }
            cells[playerOnePosition - 1].setPlayerPosition(1);
            cells[playerTwoPosition - 1].setPlayerPosition(2);
            cells[playerThreePosition - 1].setPlayerPosition(3);
            cells[playerFourPosition - 1].setPlayerPosition(4);

            PrintBoard();
            PrintDice(diceValue);
            PrintPlayerPositionTable();
        }

        public void PrintPlayerPositionTable()
        {
            Console.WriteLine("Player Name \t Position");
            for (int i = 0; i < players.Positions.Length; i++)
            {

                Console.WriteLine(players.Name[i] + "\t" + players.Positions[i]);
            }
        }

        public void saveGameData()
        {
            Console.WriteLine("Saving your Game...");
            saveGame.writeFile(players.Name, players.Positions, typeOfDice);
            Thread.Sleep(3000);
            Console.WriteLine("Game Saved");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            GameBoard board = null;
            string[] playerName = new string[4] { "p1", "p2", "p3", "p4" };
            int playerCount = 0;
            bool playerWon = false;
            bool jarvis = false;
            bool diceType = false;
            Console.WriteLine("You Want to star new game or resume from the last one");
            Console.WriteLine("Y -> New Game");
            Console.WriteLine("R -> Resume Game");
            Console.WriteLine("Esc -> Exit Game");
            ConsoleKeyInfo currentKey = Console.ReadKey(true);
            Console.Clear();
            switch (currentKey.Key)
            {
                case ConsoleKey.Y:
                    {

                        Console.WriteLine(".................................Welcome............................................");
                        Console.WriteLine("Choose Players from 1 to 4: ");
                        do
                        {
                            while (!int.TryParse(Console.ReadLine(), out playerCount))//Checking value String Or Not
                            {
                                Console.Write("Enter Correct Selection: ");
                            }

                        } while (playerCount > 4);

                        for (int countDown = 0; countDown < playerCount; countDown++)//varible i is used to count which key press enter by user
                        {
                            if (playerCount == 1)
                            {
                                Console.WriteLine("You are going to play Game with Computer");
                                playerName[1] = "Jarvis";
                                jarvis = true;
                            }
                            Console.WriteLine("Enter " + (countDown + 1) + " Player Name: ");
                            playerName[countDown] = Console.ReadLine();
                            // board.players.ValidateString(ref players.Name[countDown]);
                            //ValidateString(ref PlayerNameStore[StoeLocation]);//validating string

                        }

                        
                        int getUserSelection = 0;
                        do
                        {
                            
                            Console.WriteLine("Select Your Dice Mode:");//selecting use mode
                            Console.WriteLine("1: Manual-Dice:");
                            Console.WriteLine("2: Automatic_Dice:");
                            while (!int.TryParse(Console.ReadLine(), out getUserSelection))//Checking value String Or Not
                            {
                                Console.Write("Enter Correct Selection: ");
                            }
                            if (getUserSelection > 2 || getUserSelection <= 0)
                            {
                                Console.WriteLine("Try Again");
                            }
                        } while (getUserSelection > 2 || getUserSelection <= 0);

                        switch (getUserSelection)
                        {
                            case 1:
                                diceType = false;
                                board = new GameBoard(true, false, playerName);
                                while (playerWon == false)
                                {
                                    for (int count = 0; count < playerCount; count++)
                                    {

                                        playerWon = board.RollDice(count, false);
                                        
                                        if (playerWon == true)
                                        {
                                            count = playerCount;
                                        }


                                    }
                                    if (jarvis == true)
                                    {
                                        playerWon = board.RollDice(1, true);
                                    }
                                }
                                break;
                            case 2:
                                diceType = true;
                                board = new GameBoard(true, true, playerName);//Automatic
                                while (playerWon == false)
                                {
                                    for (int count = 0; count < playerCount; count++)
                                    {

                                        playerWon = board.RollDice(count, true);

                                        if (playerWon == true)
                                        {
                                            count = playerCount;
                                        }

                                    }
                                    if (jarvis == true)
                                    {
                                        playerWon = board.RollDice(1, true);
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case ConsoleKey.R:
                    string[] people = new string[4];
                    int[] num = new int[5];
                    
                    Save obj = new Save();

                   // obj.writeFile(board.players.Name, board.players.Positions, diceType);
                    people = obj.giveNames();
                    num = obj.giveValues();
                    if (num[4] == 1)
                    {
                        diceType = true;
                    }
                    else
                    {
                        diceType = false;
                    }
                    board = new GameBoard(false, diceType, people);
                    board.Instantiate(num[0], num[1], num[2], num[3], num[4]);
                    for (int i = 0; i < 4;i++ )
                    {
                        if(num[i]!=1 && num[i]!=0)
                        {
                            playerCount++;
                        }
                    }
                    while (playerWon == false)
                    {
                        for (int count = 0; count < playerCount; count++)
                        {

                            playerWon = board.RollDice(count, diceType);

                            if (playerWon == true)
                            {
                                count = playerCount;
                            }

                        }
                        if (jarvis == true)
                        {
                            playerWon = board.RollDice(1, true);
                        }
                    }
                       


                    break;
                default:
                    Console.WriteLine("Please enter Y for new game ane R to resume the previous played game:");
                    break;
            }


            Console.WriteLine("Thank You for playing this game");
            Console.ReadKey();
        }



    }



    class Save
    {
        //string[] people1 = new string[4];
        int[] num1 = new int[5];
        string[] people1 = new string[4];
        // int[] num = new int[4];
        public void writeFile(string[] people, int[] num, bool dice)
        {




            StreamWriter writer = null;
            writer = new StreamWriter("file.txt");
            writer.WriteLine(people[0]);
            writer.WriteLine(people[1]);
            writer.WriteLine(people[2]);
            writer.WriteLine(people[3]);
            writer.WriteLine(num[0]);
            writer.WriteLine(num[1]);
            writer.WriteLine(num[2]);
            writer.WriteLine(num[3]);
            if (dice == true)
            {
                writer.WriteLine(1);
            }
            else
            {
                writer.WriteLine(0);
            }
            writer.Close();


        }
        public string[] giveNames()
        {


            StreamReader reader = null;
            reader = new StreamReader("file.txt");
            people1[0] = reader.ReadLine();
            people1[1] = reader.ReadLine();
            people1[2] = reader.ReadLine();
            people1[3] = reader.ReadLine();


            reader.Close();
            return people1;




        }
        public int[] giveValues()
        {
            StreamReader reader = null;
            reader = new StreamReader("file.txt");
            for (int i = 0; i < 4; i++)
            {
                string temp = reader.ReadLine();
            }
            num1[0] = int.Parse(reader.ReadLine());
            num1[1] = int.Parse(reader.ReadLine());
            num1[2] = int.Parse(reader.ReadLine());
            num1[3] = int.Parse(reader.ReadLine());
            num1[4] = int.Parse(reader.ReadLine());
            reader.Close();

            return num1;

        }




    }



}