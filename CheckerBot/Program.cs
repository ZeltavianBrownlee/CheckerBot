using System;

namespace CheckerBot
{

    class Program
    {
        //DECLARE AN INTIALIZE VARIABLES
        static int xPos = 0;
        static int yPos = 0;
        static int[] AnswerCoords = new int[4];
        

        //static string[,] CheckerSquareColor = new string[,]
        //{
        //        {"blue","red","blue","red","blue","red","blue","red"},
        //        {"red","blue","red","blue","red","blue","red","blue"},
        //        {"blue","red","blue","red","blue","red","blue","red"},
        //        {"red","blue","red","blue","red","blue","red","blue"},
        //        {"blue","red","blue","red","blue","red","blue","red"},
        //        {"red","blue","red","blue","red","blue","red","blue"},
        //        {"blue","red","blue","red","blue","red","blue","red"},
        //        {"red","blue","red","blue","red","blue","red","blue"},
        //};

        //2D Array checks
        //static int[,] CheckerData = new int[8, 8];

        //CHECKER PIECES
        static int empty = 0;
        static int black = 1;
        static int white = 2;
        static int bking = 3;
        static int wking = 4;
        static int unallowed = 5;

        //BOARD COLUMN, ROW, AND COLOR SETUP
        static int xWidth = 8;
        static int yHeight = 8;
        static string[,] BoardColorSlot = new string[8, 8];
        static string boardColor = "";
       
        static int squareHeight = 4;
        static int squareWidth = 8;


        static void Main(string[] args)
        {
            bool gameFinished = false;
            bool numericNumber = false;
            int player = 0;
            bool player1 = true;

            //DECLARE AND INTIALIZE VARIABLES
            string response = "";

            player = PlayerTurn(player1);

            
            //CALL FUNCTION TO DRAW BOARD
            DrawBoard(xWidth, yHeight, xPos, yPos);

            Console.SetCursorPosition(0, 32);

            while (gameFinished == false)
            {
                
                //LOOP UNTIL RESPONSE IS 5 DIGITS AND NUMERIC WITH A DASH
                do
                {
                    Console.Write($"Movement on the checkerboard is handled by inputting two coordinates.\nPlayer {player}, which piece do you want to move and to where? (Ex: 12-34 is (1,2) to (3,4)): ");
                    response = Console.ReadLine();
                    //CALL IS NUMERIC FUNCTION
                    numericNumber = IsNumeric(response);
                } while (numericNumber== false || response.Length != 5 || response == "");

                //CALL FUNCTION TO CONVERT RESPONSE TO 4 COORDINATES WITHOUT DASH
                ConvertResponsePositions(response);

                //CALL FUNCTION TO MOVE CHECKER
                MoveChecker(player);

                //CALL FUNCTION TO CHANGE PLAYER
                ChangePlayer(ref player);
            }//end while

           

            //// populating 2D Array

            // for (int index = 0; index < 8; index++)
            // {
            //     for (int index2 = 0; index2 < 8; index2++)
            //     {

            //         CheckerData[index, index2] = board_color;

            //         Console.WriteLine("{0}", CheckerData[index,index2]);
            //     }
            // }

        }//end main


        static void DrawSquare(int positionX, int positionY, string boardColor)
        {
            int y_start = positionY;
            int x_start = positionX;
            int x_end = x_start + squareWidth;
            int y_end = y_start + squareHeight;

            char block = (char)9608;
            for (int index = y_start; index < y_end; index++)
            {
                for (int index2 = x_start; index2 < x_end; index2++)
                {
                    Console.SetCursorPosition(index2, index);
                    if (boardColor == "red")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (boardColor == "blue")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    Console.Write(block);
                }//end for
            }//end for
            Console.ResetColor();
            Console.SetCursorPosition(xPos, yPos);
        }//end function
        static void DrawChecker(int positionX, int positionY)
        {
            int y_start = positionY + 1;
            int x_start = positionX + 2;
            int x_end = x_start + 4;
            int y_end = y_start + 2;

            for (int index = y_start; index < y_end; index++)
            {
                for (int index2 = x_start; index2 < x_end; index2++)
                {
                    char gamepiece = (char)9679;
                    Console.SetCursorPosition(index2, index);

                    Console.WriteLine(gamepiece);

                }//end for
            }//end for
            Console.ResetColor();
            Console.SetCursorPosition(xPos, yPos);
        }//end function
        static void DrawBoard(int sizeX, int sizeY, int xPosition, int yPosition)
        {
            int rows = 0;
            int squares = 0;

            //LOOP TO FILL ROWS
            while (rows < sizeY)
            {
                //ROW COLOR STARTS WITH BLUE IF ROW NUMBER IS EVEN
                if (rows % 2 == 0)
                {
                    boardColor = "blue";
                }
                //ROW COLOR STARTS WITH RED IF ROW NUMBER IS ODD
                else
                {
                    boardColor = "red";
                }//end if 

                //LOOP TO FILL SQUARES IN ROW
                while (squares < sizeX)
                {
                    //CALL FUNCTION TO DRAW SQUARE
                    DrawSquare(xPos, yPos, boardColor);

                    //STORE SQUARE COLOR INTO COLOR SLOT
                    BoardColorSlot[squares, rows] = boardColor;

                    //PLACE WHITE CHECKERS ON ROWS 1-3
                    if (boardColor == "blue" && rows < 3)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.White;

                        //CALL DRAWCHECKER FUNCTION
                        DrawChecker(xPos, yPos);
                    }
                    //PLACE BLACK CHECKERS ON ROWS 5-7
                    else if (boardColor == "blue" && rows >= 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Black;

                        //CALL DRAWCHECKER FUNCTION
                        DrawChecker(xPos, yPos);
                    }//end if

                    //DISPLAY COLUMN NUMBERS
                    if (rows == 0)
                    {
                        //SET CURSOR POSITION
                        Console.SetCursorPosition(xPos, yPos);
                        Console.Write(squares);
                    }//end if 

                    //DISPLAY ROW NUMBERS
                    if (squares == 0)
                    {
                        //SET CURSOR POSITION
                        Console.SetCursorPosition(xPos, yPos);
                        Console.Write(rows);
                    }//end if 

                    //INCREASE X POSITION BY THE WIDTH 
                    xPos += 8;


                    //IF PREVIOUS BLOCK COLOR BLUE CHANGE TO RED
                    if (boardColor == "blue")
                    {
                        boardColor = "red";
                    }
                    //IF PREVIOUS BLOCK COLOR RED CHANGE TO BLUE
                    else if (boardColor == "red")
                    {
                        boardColor = "blue";
                    }//end if 

                    squares += 1;
                }//end while

                //INCREASE ROW BY ZERO 
                rows += 1;

                //CAN X AXIS POSITION BACK TO TWO FOR NEXT ROW
                xPos = 0;

                //MOVE DOWN ON Y AXIS HEIGHT OF SQUARE
                yPos += 4;

                //REINSTIALIZE SQUARES TO ZERO FOR NEXT ROW
                squares = 0;
            }//end while           
        }//end function
        static void ConvertResponsePositions(string allcoords)
        {
            //TAKE INPUT AS (X1Y1-X2Y2) AND...

            char[] charcoords = allcoords.ToCharArray();

            int count = 0;

            //...CONVERT TO ARRAY OF X1, Y1, X2, Y2 AND REMOVE THE HYPHEN

            for (int index = 0; index < charcoords.Length; index++)
            {

                if (charcoords[index] != '-')
                {
                    AnswerCoords[count] = (Convert.ToInt32(charcoords[index]) - 48);
                    count++;
                }//end if

            }//end for

        }//end function
        static void MoveChecker(int player)
        {
            int currentXposition = (AnswerCoords[0]) * squareWidth;
            int currentYposition = (AnswerCoords[1]) * squareHeight;
            int xMoveto = AnswerCoords[2] * squareWidth;
            int yMoveto = AnswerCoords[3] * squareHeight;
            bool correctColorSquare = false;

            //CALL CHECKVALID SQUARE COLOR FUNCTION
            correctColorSquare = CheckValidSquareColor(BoardColorSlot);

            //ONLY MOVE PIECE IF CORRECT SQUARE COLOR
            if (correctColorSquare == true)
            {
                //CHANGE BACKGROUND COLOR TO FILL IN CHECKER SPOT
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.BackgroundColor = ConsoleColor.Blue;

                //CALL DRAWCHECKER FUNCTION TO CHANGE COLOR BACK BLUE
                DrawChecker(currentXposition, currentYposition);

                
                //CHANGE COLOR TO PLACE CHECKER
                if (player == 1)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.White;

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                //CALL DRAWCHECKER TO DRAW NEW CHECKER POSITION
                DrawChecker(xMoveto, yMoveto);
            }
            else
            {
                Console.WriteLine("----------You entered a bad coordinate!!!!!\n");
            }
        }//end function
        static bool CheckValidSquareColor(string[,] squareColor)
        {
            bool correctSquareColor = false;
            //IF MOVETO ANSWER COORDINATES CORRESPONDS TO BLUE SQUARE ON BOARD
            if (squareColor[AnswerCoords[2], AnswerCoords[3]] == "blue")
            {
                return true;
            }
            return correctSquareColor;

        }//end function
        static bool IsNumeric(string response)
        {
            int decimalValue = 0;

            for (int i = 0; i < response.Length; i++)
            {
                //CHECKING EACH CHAR ELEMENT OF STRING FOR NUMBER DECIMAL VALUES
                char letter = response[i];

                decimalValue = (int)letter;
                if (decimalValue< 45 || decimalValue > 57 || decimalValue ==46 || decimalValue == 47)
                {
                    Console.WriteLine("-----Please check your coordinates and make sure they are in the correct format-----\n");
                    return false;
                }//end if
                
            }//end for
            return true;
        }//end function
        static int PlayerTurn(bool p1Turn)
        { 
            //CHECK PLAYER TURN
            if (p1Turn == true)
            {
                return 1;
            }
            else
            {
               return  2;
            }//end if
        }//end function
        static int ChangePlayer(ref int playerNow)
        {
            //FLIP FLOP FROM PLAYER 1 TO 2, AND 2 TO 1
            if (playerNow == 1)
            {
                playerNow++; //CHANGE TO PLAYER 2
            }
            else if (playerNow == 2)
            {
                playerNow--; //CHANGE TO PLAYER 1
            }//end if

            return playerNow;
        }//end function
    }//end class
    }//end namespace
