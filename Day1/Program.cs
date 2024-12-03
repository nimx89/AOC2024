namespace Day_1;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        int[] array1;
        int[] array2;
        try
        {
            string filename = "input.txt";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),filename);
            
            if (!File.Exists(filePath)){
                Console.WriteLine($"File '{filename} does not exits in the current directory.");
                return;
            }

            List<int> column1 = new List<int>();
            List<int> column2 = new List<int>();

            foreach(string line in File.ReadAllLines(filePath)){
                string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                
                if (int.TryParse(parts[0],out int num1) && int.TryParse(parts[1],out int num2)){
                    column1.Add(num1);
                    column2.Add(num2);
                }
                else{
                    Console.WriteLine($"Invalid number foramt");
                }
            }
            array1 = column1.ToArray();
            array2 = column2.ToArray();
            Array.Sort(array1);
            Array.Sort(array2); 
            int sum =0 ;
            int occur = 0;
            int len = array1.Length;
            int j = 0;

            // part 1 starts
            // uncomment this for part 1
            // for(int i = 0; i<len;i++){
            //     sum += Math.Abs(array1[i]-array2[i]);
            // }
            //part 1 ends

            //part 2 starts
            // comment this for part 2
            for (int i = 0; i < len; i++)
            {
                while(j<len && array1[i]>=array2[j]){
                    if (array1[i] == array2[j]){
                        occur++;
                    }
                    j++;
                }
                sum = sum +array1[i]*occur;
                occur = 0;
                j=0;
            }
            //part 2 ends


            Console.WriteLine("Sum = "+sum);

        }
        catch (System.Exception)
        {
            Console.WriteLine("error");
            throw;
        }
    }
}
