namespace Day3;
using System.Text.RegularExpressions;
class Program
{
    static void Main(string[] args)
    {
        try
        {
            string filename = "input.txt"; ;
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),filename);
            if(!File.Exists(filePath)){
                Console.WriteLine($"File '{filename}' does not exits in the current directory. " ); 
                return;
            }

            bool multiply = true; // flag to check if we need to multiply or not, it is outside so that after a line the flag must remain same

            int sum1 = 0; // sum for part 1
            int sum2 = 0; // sum for part 2
            
            string pattern1 = @"mul\(\d{1,3},\d{1,3}\)"; // pattern for regex for part 1
            string pattern2 =  @"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)"; // pattern for regex for part 2

            foreach(string line in File.ReadAllLines(filePath)){

                //for part 1
                MatchCollection parts1 = Regex.Matches(line,pattern1);
                foreach(Match part in parts1){
                    string[] numbers = part.Value.Replace("mul(","").Replace(")","").Split(","); //extracting numbers from the string
                    int num1 = int.Parse(numbers[0]);
                    int num2 = int.Parse(numbers[1]);
                    int prd = num1 * num2;
                    sum1 += prd;

                }

                //for part 2
                MatchCollection parts2 = Regex.Matches(line,pattern2);
                foreach(Match part in parts2){
                    if(part.Value == "do()"){ // if we get do() then we need to multiply
                        multiply = true;
                    }
                    else if(part.Value == "don't()"){ // if we get don't() then we don't need to multiply
                        multiply = false;
                    }
                    else if(multiply){
                        string[] numbers = part.Value.Replace("mul(","").Replace(")","").Split(",");
                        int num1 = int.Parse(numbers[0]);
                        int num2 = int.Parse(numbers[1]);
                        int prd = num1 * num2;
                        sum2 += prd;
                        
                    }

                }

            }
            Console.WriteLine($"Part 1 :{sum1}");
            Console.WriteLine($"Part 2 :{sum2}");
        }
        catch (System.Exception)
        {
            Console.WriteLine("Some error occured");
            throw;
        }
    }
}