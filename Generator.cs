using System.Text;
using static System.IO.Directory;

namespace CGOLCellGenerator;

public class Generator
{
    string[] Alphabet =
    {
        "a", "b", "c", "d", "e", "f", "g", "h","i", "j", "k", "l", "m", "n", "o", "p", "q", "r", 
        "s", "t", "u", "v", "w", "x","y", "z"
    };

    private string[] Vowels =
    {
        "a", "e", "i", "o", "u", "y"
    };

    private string[] Consonants =
    {
        "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z"
        
    };
        
    string[] Numbers = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0"};

    private string[] Even = {"0", "2", "4", "6", "8"};

    private string[] Odd = {"1", "3", "5", "7", "9"};
    
    string CellMortality(string seed, int cell,int row)
    {
        var environment = ChopSeed(seed);
        var rand = new Random();
        var patternRoot = "";
        var nums = new List<int>();
        var letters = new List<string>();
        
        foreach (var sprout in environment)
        {
            var num = int.TryParse(sprout,out var foundNum);
            if(num) nums.Add(foundNum);
            else letters.Add(sprout);
        }

        var bunny = cell * row + Math.Max(nums.Count,letters.Count) - Math.Min(nums.Count,letters.Count);

        foreach (var letter in letters)
        {
            if (Vowels.Contains(letter))
                patternRoot += Vowels.ToList().IndexOf(letter) * bunny;
            else
                patternRoot += Vowels.ToList().IndexOf(letter) * bunny;
            
            patternRoot += Alphabet.ToList().IndexOf(letter) * bunny;
        }

        foreach (var num in Numbers)
        {
            if (Even.Contains(num))
                patternRoot += Even.ToList().IndexOf(num) * bunny;
            else
                patternRoot += Even.ToList().IndexOf(num) * bunny;

            patternRoot += Numbers.ToList().IndexOf(num) * bunny;
        }

        patternRoot = patternRoot.Replace("-", "");
       
        var dna = 0;
        var nothing = 0;
        for (int i = 0; i < patternRoot.Length; i++)
        {
            var gene = Convert.ToInt32(patternRoot[i].ToString().Last().ToString());
            if (gene == 0)
                gene++;
            if (Odd.Contains(gene.ToString()))
            {
                if (gene % (Odd.Length - Odd.ToList().IndexOf(gene.ToString())) == 1)
                    dna++;
            }
            else
                nothing++;
        }

        var life = Math.Max(nothing, dna) - Math.Min(nothing, dna);

        return life > 15 ? "-" : "O";
    }

    private List<string> ChopSeed(string seed)
    {
        var choppedSeed = new List<string>();
        
        for (var i = 0; i < seed.Length; i++)
            choppedSeed.Add(seed[i].ToString());
        
        return choppedSeed;
    }

    public void GenerateRows(string seed,int rowCount, int cellCount)
    {
        var root = AppDomain.CurrentDomain.BaseDirectory;
        if (!Exists(@$"{root}\cells"))
            CreateDirectory(@$"{root}\cells");

        var foundStream = new FileStream(@$"{root}\cells\{seed}.cells", FileMode.Create);
        var foundWriter = new StreamWriter(foundStream);
        foundWriter.AutoFlush = true;
        Console.SetOut(foundWriter);

        for (var r = 0; r < rowCount; r++)
        {
            var rowString = "";
            
            for (var c = 0; c < cellCount; c++)
                rowString += CellMortality(seed,c,r);
            
            Console.WriteLine(rowString);
        }
    }



    bool CoinFlip()
    {
        var rand = new Random();
        return (rand.Next() % 2) == 1;
    }
    
    public string RandomSeed(int len)
    {
        var r = new Random();
        var name = new StringBuilder();
        
        string[] letters =
        {
            "a", "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x","y","z"
        };
        
        string[] numbers =
        {
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"
        };
        
        var b = 0;
        
        while (b < len)
        {
            name.Append(CoinFlip()? letters[r.Next(letters.Length)]: numbers[r.Next(numbers.Length)]);
            b++;
        }
        
        return name.ToString().Replace(" ", "");
    }
}