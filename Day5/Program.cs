using System.Text;
using System.Collections.Generic;
using System.Linq;
namespace Day5;
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
        string[] lines = File.ReadAllLines(filePath);
        bool hit = false;
        List<(int,int)> pairs = new List<(int,int)>();
        int result =0 ;
        int result2 = 0;
        Dictionary<int,List<int>> graph = new Dictionary<int, List<int>>();
        List<int> sortedOrder = new List<int>();
        foreach(string line in lines){
            if(line.Equals("")){
                hit = true;
                foreach(var (x,y) in pairs){
                    if(!graph.ContainsKey(x)){
                        graph[x] = new List<int>();
                    }
                    if(!graph.ContainsKey(y)){
                        graph[y] = new List<int>();
                    }
                    graph[x].Add(y);
                }
                continue;
            }

            if(!hit){
                int[] p = line.Split('|').Select(int.Parse).ToArray();
                pairs.Add((p[0],p[1]));
            }
            else{
                int[] seq = line.Split(',').Select(int.Parse).ToArray();
                int  m = seq.Length;
                bool isContain = true;
                for(int i=0;i<m;i++){
                    for(int j=i+1;j<m;j++){
                        if(!graph.ContainsKey(seq[i]) || !graph[seq[i]].Contains(seq[j])){
                            isContain = false;
                            break;
                        }
                    }
                    if(!isContain){
                        break;
                    }
                }
                if(isContain){
                    result += seq[m/2];
                }
                else{
                    var correctedGraph = new Dictionary<int,List<int>>();

                    foreach(var item in seq){
                        correctedGraph[item] = new List<int>(graph[item].Intersect(seq));
                    }

                    var sorted = correctedGraph.OrderByDescending(pair => pair.Value.Count).Select(pair => pair.Key).ToList();
                    result2 += sorted[m/2];
                }
            }
        }
        Console.WriteLine("Part 1 : "+result);
        Console.WriteLine("Part 2 : "+result2);
    }
}
