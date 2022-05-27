using CGOLCellGenerator;


var generator = new Generator();
var rand = new Random();
var seedLength = rand.Next(10, 30);

Console.Write("(optional) Enter Seed: ");
var seedInput = Console.ReadLine();
var seed = string.IsNullOrEmpty(seedInput)?  generator.RandomSeed(seedLength): seedInput;

generator.GenerateRows(seed.Replace(" ",""),200,200);

