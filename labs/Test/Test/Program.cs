using System;

// https://books.google.com/books?id=K0ICo6r29W4C&pg=PA44&lpg=PA44&dq=Console.WriteLine("1+%3D+pen+up;+2+%3D+pen+down");&source=bl&ots=nvDsZSHRSa&sig=ACfU3U22nfPZBcB07rHNFk-ZT4vekiMUDw&hl=en&sa=X&ved=2ahUKEwiL_bKAmqWBAxWIkokEHdSEDwMQ6AF6BAgKEAM#v=onepage&q&f=false
// https://www.youtube.com/playlist?list=PLvQSG8B7sh6mnpeOV2xzZp5emfIEAWDFU

// I couldnt get it to work with the xaml so I deleted it and am tried running the program just as a .cs. I still cant get it to work :(


namespace CS_423_A02_Turtle
{
     public class RobotTurtle
     {
          private bool KeepGoing = true;
          private const char PenUp = '1';
          private const char PenDown = '2';
          private const char TurnRight = '3';
          private const char TurnLeft = '4';
          private const char ClearGrid = '6';
          private const char Exit = '9';

          private enum PenPos
          {
               Up,
               Down
          }

          private enum Direct
          {
               North,
               South,
               East,
               West
          }

          private PenPos position = PenPos.Up;
          private Direct direction = Direct.East;
          private bool [ , ] grid;
          private int current_row = 0;
          private int current_col = 0;

          public RobotTurtle ( int rows , int cols )
          {
               Console.WriteLine ( "Roland the Headless Turtle" );
               grid = new bool [ rows , cols ];
          }

          public static void PrintMenu ( )
          {
               Console.WriteLine ( "Robot Turtle Commands" );
               Console.WriteLine ( "1   Pen Up" );
               Console.WriteLine ( "2   Pen Down" );
               Console.WriteLine ( "3   Turn Right" );
               Console.WriteLine ( "4   Turn Left" );
               Console.WriteLine ( "5,x   Move forward by x number of spaces" );
               Console.WriteLine ( "6   Clear the grid" );
               Console.WriteLine ( "9   Terminate program" );
          }

          public void ProcessMenuChoice ( )
          {
               string input = Console.ReadLine ( );
               if ( string.IsNullOrEmpty ( input ) )
               {
                    input = "0";
               }

               switch ( input [ 0 ] )
               {
                    case PenUp:
                         LiftPen ( );
                         break;
                    case PenDown:
                         LowerPen ( );
                         break;
                    case TurnRight:
                         SetTurnRight ( );
                         break;
                    case TurnLeft:
                         SetTurnLeft ( );
                         break;
                    case ClearGrid:
                         SetClearGrid ( );
                         break;
                    case Exit:
                         KeepGoing = false;
                         break;
                    default:
                         if ( input.StartsWith ( "5," ) )
                         {
                              MoveForwardBySpaces ( int.Parse ( input.Substring ( 2 ) ) );
                         }
                         else
                         {
                              PrintErrorMessage ( );
                         }

                         break;
               }
          }

          public void LiftPen ( )
          {
               position = PenPos.Up;
               Console.WriteLine ( "The pen is " + position );
          }

          public void LowerPen ( )
          {
               position = PenPos.Down;
               Console.WriteLine ( "The pen is " + position );
          }

          public void SetTurnRight ( )
          {
               switch ( direction )
               {
                    case Direct.North:
                         direction = Direct.East;
                         break;
                    case Direct.East:
                         direction = Direct.South;
                         break;
                    case Direct.South:
                         direction = Direct.West;
                         break;
                    case Direct.West:
                         direction = Direct.North;
                         break;
               }

               Console.WriteLine ( "Direction is " + direction );
          }

          public void SetTurnLeft ( )
          {
               switch ( direction )
               {
                    case Direct.North:
                         direction = Direct.West;
                         break;
                    case Direct.West:
                         direction = Direct.South;
                         break;
                    case Direct.South:
                         direction = Direct.East;
                         break;
                    case Direct.East:
                         direction = Direct.North;
                         break;
               }

               Console.WriteLine ( "Direction is " + direction );
          }

          public void SetClearGrid ( )
          {
               Array.Clear ( grid , 0 , grid.Length );
               Console.WriteLine ( "Grid cleared." );
          }


          public void PrintErrorMessage ( )
          {
               Console.WriteLine ( "Input a valid command." );
          }

          public void Run ( )
          {
               while ( KeepGoing )
               {
                    PrintMenu ( );
                    ProcessMenuChoice ( );
               }
          }

          public void MoveForwardBySpaces ( int moveSpaces )
          {
               int moveCount = moveSpaces;
               switch ( position )
               {
                    case PenPos.Up:
                         switch ( direction )
                         {
                              case Direct.North:
                                   if ( ( current_row - moveSpaces ) < 0 )
                                   {
                                        current_row = 0;
                                   }
                                   else
                                   {
                                        current_row = current_row - moveSpaces;
                                   }

                                   break;
                              case Direct.South:
                                   if ( ( current_row - moveSpaces ) > ( grid.GetLength ( 1 ) - 1 ) )
                                   {
                                        current_row = ( grid.GetLength ( 1 ) - ( 1 ) );
                                   }
                                   else
                                   {
                                        current_row = current_row - moveSpaces;
                                   }

                                   break;
                              case Direct.East:
                                   if ( ( current_col - moveSpaces ) > ( grid.GetLength ( 0 ) - 1 ) )
                                   {
                                        current_col = ( grid.GetLength ( 0 ) - ( 1 ) );
                                   }
                                   else
                                   {
                                        current_col = current_col - moveSpaces;
                                   }

                                   break;
                              case Direct.West:
                                   if ( ( current_col - moveSpaces ) < 0 )
                                   {
                                        current_col = 0;
                                   }
                                   else
                                   {
                                        current_col = current_col - moveSpaces;
                                   }

                                   break;
                         }

                         break;
                    case PenPos.Down:
                         switch ( direction )
                         {
                              case Direct.North:
                                   while ( ( current_row > 0 ) && ( moveSpaces-- > 0 ) )
                                   {
                                        grid [ current_row-- , current_col ] = true;
                                   }

                                   break;
                              case Direct.South:
                                   while ( current_row < grid.GetLength ( 0 ) - 1 && moveSpaces-- > 0 )
                                   {
                                        grid [ current_row++ , current_col ] = true;
                                   }
                                   break;
                              case Direct.East:
                                   while ( current_col < grid.GetLength ( 1 ) - 1 && moveSpaces-- > 0 )
                                   {
                                        grid [ current_row , current_col++ ] = true;
                                   }
                                   break;

                              case Direct.West:
                                   while ( ( current_col > 0 ) && ( moveSpaces-- > 0 ) )
                                   {
                                        grid [ current_row , current_col-- ] = true;
                                   }

                                   break;
                         }

                         break;
               }
          }

          public int GetMoves ( )
          {
               int spaces = 0;
               string input;
               Console.WriteLine ( "Enter the number of spaces: " );
               input = Console.ReadLine ( );

               if ( input == String.Empty )
               {
                    spaces = 0;
               }
               else
               {
                    try
                    {
                         spaces = Convert.ToInt32 ( input );
                    }
                    catch ( Exception )
                    {
                         spaces = 0;
                    }
               }

               return spaces;
          }

          public static void Main ( String [ ] args )
          {
               RobotTurtle rr = new RobotTurtle ( 20 , 20 );
               rr.Run ( );
          }
     }
}