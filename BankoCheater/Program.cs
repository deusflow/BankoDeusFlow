namespace BancoGame;

class BancoGame
{
    static List<List<int>> CreateBoardList(int[] numbers)
    {
        var row1 = numbers.Take(5).ToList();
        var row2 = numbers.Skip(5).Take(5).ToList();
        var row3 = numbers.Skip(10).Take(5).ToList();
        
        var boardList = new List<List<int>> { row1, row2, row3 };
        return boardList;
    }

    
    static void Main()
    {
        var board = new Dictionary<string, List<List<int>>>
        {
            { "Oleh",  CreateBoardList (new[] { 2, 11, 23, 33, 60, 14, 34, 44, 54, 63, 7, 16, 66, 74, 88 }) },
            { "Oleh1", CreateBoardList (new[] { 1, 27, 32, 70, 81, 4, 13, 43, 53, 64, 38, 47, 59, 69, 77 }) },
            { "Oleh2", CreateBoardList (new[] { 1, 23, 31, 65, 84, 2, 24, 45, 77, 87, 14, 26, 38, 59, 78 }) },
            { "Oleh3", CreateBoardList (new[] { 21, 31, 40, 62, 71, 25, 32, 63, 77, 85, 8, 18, 35, 59, 78}) },
            { "Oleh4", CreateBoardList (new[] { 6, 21, 46, 51, 72, 17, 31, 47, 65, 74, 32, 48, 54, 79, 88}) }
            
        };
        
        var drawnNumbers = new HashSet<int>(); //stores unique values of the drawn numbers
        bool gameOver = false; 
        
        
        while (!gameOver)
        {
            Console.WriteLine("Enter a draw number and press ENTER: ");
            var input = Console.ReadLine();
            
            //трайПарсим
            if (int.TryParse(input, out var number))
            {
                if (drawnNumbers.Contains(number))
                {
                    Console.WriteLine("Draw number {0} is already in the list", number);
                }
                else
                {
                    drawnNumbers.Add(number);
                }
            }
            
            else
            {
                Console.WriteLine("Are you crazy? You have to enter only numbers!");
            }
            
            
            
            Console.Clear();
            
            //текущее состояние таблицы --Current board status
            Console.WriteLine("\nCurrent board status"); 
                
                foreach (var pair in board)
            { Console.WriteLine($"{pair.Key}'s board:");
                    
                    foreach (var row  in pair.Value)
                        
                    {
                            foreach (var num in row)
                            {
                                //играю в цвета -just for fun added color to the numbers
                                if (drawnNumbers.Contains(num))
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write($"[{num}] ");
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write($"[{num}] ");
                                }
                            }
                            Console.ResetColor();
                            Console.WriteLine();
                    } 
                    Console.WriteLine();
            }

                
            
                                                        //---------ChekWin-----------

            foreach (var pair in board)
            {
                int winCount = 0; //hvor mange rækker er vundet
                
                foreach (var row in pair.Value)
                {
                    if (row.All(n => drawnNumbers.Contains(n)))
                    {
                        winCount++;
                    }
                }
                
                if (winCount > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    
                    if (winCount == 1)
                        Console.WriteLine($"{pair.Key} won!");
                    else if(winCount == 2)
                        Console.WriteLine($"{pair.Key} won a two rows");
                    else if(winCount == 3)
                        Console.WriteLine($"{pair.Key} won a all rows!");
                    
                    foreach (var row in pair.Value)
                    {
                        if (row.All(n => drawnNumbers.Contains(n)))
                        {
                            foreach (var num in row)
                            {
                                Console.Write($"[{num}] ");
                            }
                            Console.WriteLine();
                        }
                    }
                    Console.ResetColor();
                    
                    
                    if (winCount == 3)
                    {
                        Console.WriteLine($"Game over! {pair.Key} You win!");
                       gameOver = true;
                       break;
                    }
                    
                }
               
            }
            
        }//end while
    }
}
