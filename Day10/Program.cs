using Microsoft.VisualBasic;

namespace Day10;

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
        int n = lines.Length;
        int m = lines[0].Length;
        // pad the grid with .'s 
        char[,] grid = new char[n+2,m+2];
        for(int i = 0 ; i<n+2;i++){
            grid[i,0] = '.';
            grid[i,m+1] = '.';
        }
        for(int j = 0 ; j<m+2;j++){
            grid[0,j] = '.';
            grid[n+1,j] = '.';
        }
        HashSet<(int,int)> zer = new HashSet<(int,int)>();
        for(int i = 0 ; i<n;i++){
            for(int j = 0 ; j<m;j++){
                grid[i+1,j+1] = lines[i][j];
                if(grid[i+1,j+1] == '0'){
                    zer.Add((i+1,j+1));
                }
            }
        }
 
        int count = 0;
        int count2 = 0;
        Dictionary<(int,int),int> paths = new Dictionary<(int,int),int>();
        foreach(var pos in zer){
            int nu = 1;
            HashSet<(int,int)> nin = new HashSet<(int,int)>();
            DFS(pos,nu,grid,ref paths, ref nin,n,m);
            count += nin.Count;
        }
        Console.WriteLine("Part1 :"+count);
        foreach(var path in paths){
            count2 += path.Value;
        }
        Console.WriteLine("Part2 :"+count2);
    }

    static void DFS((int,int) pos,int next, char[,] grid, ref Dictionary<(int,int),int> paths, ref HashSet<(int,int)> nin,int n,int m){
        if(grid[pos.Item1,pos.Item2] == '9'){
            nin.Add(pos);
            if(!paths.ContainsKey(pos)){
                paths.Add(pos,0);
            }
            paths[pos]++;
            return ;
        }
        if(grid[pos.Item1+1,pos.Item2] -'0' == next){
            DFS((pos.Item1+1,pos.Item2),next+1,grid,ref paths, ref nin,n,m);
        }
        if(grid[pos.Item1-1,pos.Item2] -'0' == next){
            DFS((pos.Item1-1,pos.Item2),next+1,grid,ref paths, ref nin,n,m);
        }
        if(grid[pos.Item1,pos.Item2+1] -'0' == next){
            DFS((pos.Item1,pos.Item2+1),next+1,grid,ref paths, ref nin,n,m);
        }
        if(grid[pos.Item1,pos.Item2-1] -'0' == next){
            DFS((pos.Item1,pos.Item2-1),next+1,grid,ref paths, ref nin,n,m);
        }
        return ;
    }

}
