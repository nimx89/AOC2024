using System.Diagnostics;

namespace Day7;

class Program
{
    static void Main(string[] args)
    {
        string filename = "input.txt" ;
        string filePath = Path.Combine(Directory.GetCurrentDirectory(),filename);
        if(!File.Exists(filePath)){
            Console.WriteLine($"File '{filename}' does not exits in the current directory. " ); 
            return;
        }
            
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        string[] lines = File.ReadAllLines(filePath);
        var operands = new List<long>();
        long sums , res1 = 0,res2 =0 ;
        foreach(var line in lines){
            string[] parts = line.Split(":");
            
            sums = long.Parse(parts[0]);
            operands = parts[1].Trim().Split(" ").Select(long.Parse).ToList();
            int n = operands.Count;

            // Part 1
            int maxComb = 1 <<n -1;
            for( int mask = 0 ; mask < maxComb ; mask++){
                long sum = operands[0];
                for(int i = 0 ; i < n-1 ; i++){
                    if((mask & (1<<i)) > 0){
                        sum += operands[i+1];
                    }else{
                        sum *= operands[i+1];
                    }
                }
                if(sum == sums){
                    res1+=sums;
                    break;
                }
            }

            //Part 2
            long maxComb2 = (long)Math.Pow(3, n - 1);
            string[] operators = { "+", "*", "||" };

            for (long mask = 0; mask < maxComb2; mask++)
            {
                long temp = mask;
                string eq = operands[0].ToString();
                long sum = long.Parse(eq);

                for (int i = 0; i < n - 1; i++)
                {
                    string op = operators[temp % operators.Length];
                    long operand = operands[i + 1];
                    eq += $" {op} {operand}";
                    temp /= operators.Length;

                    sum = op switch
                    {
                        "+" => sum + operand,
                        "*" => sum * operand,
                        "||" => long.Parse($"{sum}{operand}"),
                        _ => sum
                    };
                }

                if (sum == sums)
                {
                    res2 += sums;
                    break;
                }
            }

        }
        Console.WriteLine($"Part 1 : {res1}");
        Console.WriteLine($"Part 2 : {res2}");
        stopwatch.Stop();
        Console.WriteLine($"Executed in {stopwatch.ElapsedMilliseconds} ms");
    }
}
