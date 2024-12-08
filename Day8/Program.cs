namespace Day8;

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
        string[] lines = File.ReadAllLines(filePath);    
        Dictionary<char,List<(int,int)>> signals = new Dictionary<char, List<(int, int)>>();
        int n = lines.Count();
        int m = lines[0].Length;
        var grid = new char[n,m];
        for(int i = 0 ;i <n;i++){
            for(int j = 0;j<m;j++){
                char c = lines[i][j];
                grid[i,j] = c;
                if(c == '.'){
                    continue;
                }
                if(!signals.ContainsKey(c)){
                    signals[c] = new List<(int, int)>();
                }
                signals[c].Add((i,j));
            }
        }

        //Part 1
        int count=0;
        foreach(var kvp in signals){
            int l = kvp.Value.Count();
            for(int i = 0;i<l-1;i++){
                for(int j = i+1 ;j<l;j++){
                    var p1 = kvp.Value[i];
                    var p2 = kvp.Value[j];
                    var d = (p2.Item1-p1.Item1,p2.Item2-p1.Item2);
                    var a1 = new ValueTuple<int, int>(0,0);
                    var a2 = new ValueTuple<int, int>(0,0);
                    a1 = (p1.Item1-d.Item1,p1.Item2-d.Item2);
                    a2 = (p2.Item1+d.Item1,p2.Item2+d.Item2);
                    if(a1.Item1>=0 && a1.Item1<n && a1.Item2>=0 && a1.Item2<m && grid[a1.Item1,a1.Item2] != '#'){
                        grid[a1.Item1,a1.Item2] = '#';
                        count++;
                    }
                    if(a2.Item1>=0 && a2.Item1<n && a2.Item2>=0 && a2.Item2<m && grid[a2.Item1,a2.Item2] != '#'){
                        grid[a2.Item1,a2.Item2] = '#';
                        count++;
                    }
                }
            }
        }
        
        Console.WriteLine($"Part 1 : {count}");

        //Part 2
        // reset the grid
        for(int i = 0 ;i <n;i++){
            for(int j = 0;j<m;j++){
                char c = lines[i][j];
                grid[i,j] = c;
            }
        }

        int count2=0;
        foreach(var kvp in signals){
            count2+=kvp.Value.Count();
        }

        foreach(var kvp in signals){
            int l = kvp.Value.Count();
            for(int i = 0;i<l-1;i++){
                for(int j = i+1 ;j<l;j++){
                    var p1 = kvp.Value[i];
                    var p2 = kvp.Value[j];
                    var d = (p2.Item1-p1.Item1,p2.Item2-p1.Item2);
                    var a1 = new ValueTuple<int, int>(0,0);
                    var a2 = new ValueTuple<int, int>(0,0);
                    for(int k = 1;k<=100;k++){
                        a1 = (p1.Item1-d.Item1*k,p1.Item2-d.Item2*k);
                        a2 = (p2.Item1+d.Item1*k,p2.Item2+d.Item2*k);
                        if(a1.Item1>=0 && a1.Item1<n && a1.Item2>=0 && a1.Item2<m && grid[a1.Item1,a1.Item2] == '.'){
                            if(grid[a1.Item1,a1.Item2] == '.')    
                                grid[a1.Item1,a1.Item2] = '#';
                            count2++;
                        }
                        if(a2.Item1>=0 && a2.Item1<n && a2.Item2>=0 && a2.Item2<m && grid[a2.Item1,a2.Item2] == '.'){
                            if(grid[a2.Item1,a2.Item2] == '.')    
                                grid[a2.Item1,a2.Item2] = '#';
                            count2++;
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Part 2 : {count2}");
    }
}
