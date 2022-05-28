using System.Text;
using static System.IO.Directory;

namespace CGOLCellGenerator;

public class Generator
{
    private readonly string[] Alphabet =
    {
        "a", "b", "c", "d", "e", "f", "g", "h","i", "j", "k", "l", "m", "n", "o", "p", "q", "r", 
        "s", "t", "u", "v", "w", "x","y", "z"
    };

    private readonly string[] Vowels =
    {
        "a", "e", "i", "o", "u", "y"
    };

    private readonly string[] Consonants =
    {
        "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z"
        
    };

    private readonly string[] Numbers = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0"};

    private readonly string[] Even = {"0", "2", "4", "6", "8"};

    private readonly string[] Odd = {"1", "3", "5", "7", "9"};

    private int lastEnvironmentField;

    private string BuildDna(string seed, int row, int cellCount)
    {
        var environment = ChopSeed(seed);
        var patternRoot = "";
        var nums = new List<int>();
        var letters = new List<string>();
        var dnaStrand = new StringBuilder();
        foreach (var sprout in environment)
        {
            var num = int.TryParse(sprout, out var foundNum);
            if (num) nums.Add(foundNum);
            else letters.Add(sprout);
        }

        var helperNum = 0;
        if (lastEnvironmentField >= environment.Count) lastEnvironmentField = 0;
        
        if (Alphabet.Contains(environment[lastEnvironmentField]))
            helperNum = Alphabet.ToList().IndexOf(environment[lastEnvironmentField]);
        else if (Numbers.Contains(environment[lastEnvironmentField]))
            helperNum = Numbers.ToList().IndexOf(environment[lastEnvironmentField]);

        lastEnvironmentField++;
        
        helperNum *= row;
        
        if (helperNum == 0)
            helperNum = 27;
        
        var bunny = row + Math.Max(nums.Count, letters.Count) - Math.Min(nums.Count, letters.Count);

        foreach (var letter in letters)
        {
            if (Vowels.Contains(letter))
                patternRoot += Vowels.ToList().IndexOf(letter);
            else
                patternRoot += Consonants.ToList().IndexOf(letter);

            patternRoot += Alphabet.ToList().IndexOf(letter) * bunny;
        }

        foreach (var num in Numbers)
        {
            if (Even.Contains(num))
                patternRoot += Even.ToList().IndexOf(num);
            else
                patternRoot += Odd.ToList().IndexOf(num);

            patternRoot += Numbers.ToList().IndexOf(num) * bunny;
        }

        patternRoot = patternRoot.Replace("-", "");
        
        var occhio = 0;

        for (var i = 0; i < cellCount; i++)
        {
            var gene = Convert.ToInt32(patternRoot[occhio].ToString());

            var cell = gene + Convert.ToInt32(helperNum.ToString().Last());

            var dna = Convert.ToInt32(cell.ToString().Last());
            var clip = dna.ToString();
            if (clip.Length > 1)
                clip = clip.Remove(0, dna.ToString().Length - 1);
            switch (Convert.ToInt32(clip))
            {
                case 0 :
                    dnaStrand.Append('O');
                    break;
                case 1 : 
                case 2 or 3:
                case 4 or 5:
                case 6 or 7:
                case 8 or 9:
                    dnaStrand.Append('-');
                    break;
            }

            occhio++;
            if (occhio >= patternRoot.Length)
                occhio = 0;
        }

        return dnaStrand.ToString();
    }

    private List<string> ChopSeed(string seed)
    {
        return seed.Select(t => t.ToString()).ToList();
    }
    
    public void GenerateRows(string seed,int rowCount, int cellCount)
    {
        var root = AppDomain.CurrentDomain.BaseDirectory;
        if (!Exists(@$"{root}Generated Cells"))
            CreateDirectory(@$"{root}Generated Cells");
        
        var name = seed;
        if (name.Length > 15)
            name = name.Remove(15, name.Length - 15);
        
        var foundStream = new FileStream(@$"{root}Generated Cells\{name}.cells", FileMode.Create);
        var stdConsole  = new StreamWriter(Console.OpenStandardOutput());
        var foundWriter = new StreamWriter(foundStream);
        foundWriter.AutoFlush = true;
        stdConsole.AutoFlush = true;

        Console.SetOut(foundWriter);
        
        for (var r = 0; r < rowCount; r++)
            Console.WriteLine(BuildDna(seed,r,cellCount));
        
        Console.SetOut(stdConsole);
        
        Console.WriteLine("File Created -> " + @$"{root}Generated Cells\{name}.cells");
    }

    private bool CoinFlip()
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