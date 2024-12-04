namespace Day4;

using System.Numerics;
using System.Text.RegularExpressions;
class Program
{
    static void Main(string[] args)
    {
        string filename = "input.txt";
        string filePath = Path.Combine(Directory.GetCurrentDirectory(),filename);
        if(!File.Exists(filePath)){
            Console.WriteLine($"File '{filename}' does not exits in the current directory. " ); 
            return;
        }
        // int total = 0; 
        List<string> lines = File.ReadAllLines(filePath).ToList();
        int n = lines.Count;
        int m = lines[0].Length;

        //create a array of '.' of length m+6
        char[] temp = new char[m+6];
        for(int i = 0;i<m+6;i++){
            temp[i] = '.';
        }

        //add 3 '.' at the start and end of each string
        for(int i = 0;i<n;i++){
            lines[i] = "..." + lines[i] + "...";
        }

        //padding the input file
        for(int i = 0;i<3;i++){
            lines.Insert(0,new string(temp));
            lines.Add(new string(temp));
        }

        //for part 1
        int total1 =0, total2 = 0;
        for(int i = 3; i<n+3;i++){
            for(int j = 3 ; j< m+3;j++){
                if(lines[i][j] == 'X' && lines[i][j+1] == 'M' && lines[i][j+2] == 'A' && lines[i][j+3] == 'S'){
                    total1++;
                }
                if(lines[i][j] == 'X' && lines[i+1][j] == 'M' && lines[i+2][j] == 'A' && lines[i+3][j] == 'S'){
                    total1++;
                }
                if(lines[i][j] == 'X' && lines[i+1][j+1] == 'M' && lines[i+2][j+2] == 'A' && lines[i+3][j+3] == 'S'){
                    total1++;
                }
                if(lines[i][j] == 'X' && lines[i+1][j-1] == 'M' && lines[i+2][j-2] == 'A' && lines[i+3][j-3] == 'S'){
                    total1++;
                }
                if(lines[i][j] == 'X' && lines[i-1][j] == 'M' && lines[i-2][j] == 'A' && lines[i-3][j] == 'S'){
                    total1++;
                }
                if(lines[i][j] == 'X' && lines[i-1][j-1] == 'M' && lines[i-2][j-2] == 'A' && lines[i-3][j-3] == 'S'){
                    total1++;
                }
                if(lines[i][j] == 'X' && lines[i-1][j+1] == 'M' && lines[i-2][j+2] == 'A' && lines[i-3][j+3] == 'S'){
                    total1++;
                }
                if(lines[i][j] == 'X' && lines[i][j-1] == 'M' && lines[i][j-2] == 'A' && lines[i][j-3] == 'S'){
                    total1++;
                }
            }
        }

        // for part 2
        for(int i = 4;i<n+2;i++){
            for(int j = 4;j<m+2;j++){
                if(lines[i][j] == 'A' && lines[i-1][j-1] == 'M' && lines[i+1][j+1] == 'S'){
                    if(lines[i-1][j+1] == 'M' && lines[i+1][j-1] == 'S'){
                        total2++;
                    }
                    else if(lines[i+1][j-1] == 'M' && lines[i-1][j+1] == 'S'){
                        total2++;
                    }
                }
                else if(lines[i][j] == 'A' && lines[i+1][j+1] == 'M' && lines[i-1][j-1] == 'S'){
                    if(lines[i-1][j+1] == 'M' && lines[i+1][j-1] == 'S'){
                        total2++;
                    }
                    else if(lines[i+1][j-1] == 'M' && lines[i-1][j+1] == 'S'){
                        total2++;
                    }
                }
            }
        }
        Console.WriteLine("Part 1:" + total1);
        Console.WriteLine("Part 2:" + total2);
    }
}
