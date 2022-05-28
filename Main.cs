namespace CGOLCellGenerator;

public class Main
{
    public void PopMemo()
    {
        Console.WriteLine("-----Hello Welcome to my GOL Seed Generator!");
        Console.WriteLine("-----This Generator is brought to you by Adrion :D");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine("---------------Instructions---------------------");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine("----This Generator lets you create seeds using numbers & letters");
        Console.WriteLine("-----> any inputted spaces will be removed. Feel free to make the seed as long as you want!");
        Console.WriteLine("----The outputted 'cells' file will be named after your seed (trimmed to the first 15 characters)");
        Console.WriteLine("----the cells file will be created immediately after you submit the seed below, to make another,");
        Console.WriteLine("---- just continue to input seeds and press enter to submit.");
        Console.WriteLine("------------------------------------------------------------------------------------------------");
        Console.WriteLine("----Dont feel like thinking up a seed? Just Press 'Enter' and I'll randomly generate one for you!");
        Console.WriteLine("------------------------------------------------------------------------------------------------");
        Console.WriteLine("----After generating, open the 'cells' file with the included GOL Program... ");
        Console.WriteLine("----Now you're set to sit back watch and enjoy!");
        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine("----------------------------------------------------");
    }
    public void GenerateMenu()
    {
        var generator = new Generator();
        var rand = new Random();
        var seedLength = rand.Next(10, 50);

        
        Console.WriteLine(" ");
        Console.Write("Enter Seed (optional): ");
        var seedInput = Console.ReadLine();
        var seed = string.IsNullOrEmpty(seedInput) ? generator.RandomSeed(seedLength) : seedInput;
        generator.GenerateRows(seed.Replace(" ", ""), 321, 640);
    }
}