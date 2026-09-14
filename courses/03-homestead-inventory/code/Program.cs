// Course 3 - Homestead Inventory: a List<Crop> instead of one named
// variable per crop, and a WaterLevel property that validates itself no
// matter how it's set.

var crops = new List<Crop>
{
    new Crop("Carrot"),
    new Crop("Potato"),
    new Crop("Pumpkin"),
};

Console.WriteLine($"You're growing {crops.Count} crops.");

foreach (var crop in crops)
{
    crop.Water(40);
}

foreach (var crop in crops)
{
    Console.WriteLine($"{crop.Name}: water level {crop.WaterLevel}, harvestable: {crop.IsHarvestable}");
}

// Way too much water in one go -- WaterLevel clamps at 100 instead of overflowing.
crops[0].Water(1000);
Console.WriteLine($"{crops[0].Name} after a flood: water level {crops[0].WaterLevel}");

// Setting WaterLevel directly still goes through the same validation.
crops[1].WaterLevel = -20;
Console.WriteLine($"{crops[1].Name} after an invalid direct assignment: water level {crops[1].WaterLevel}");

foreach (var crop in crops)
{
    crop.Harvest();
}

// --- Challenge: the same pattern, applied to animals ---
var animals = new List<Animal>
{
    new Animal("Bessie"),
    new Animal("Clucky"),
};

foreach (var animal in animals)
{
    animal.Pet();
}

animals[0].Happiness = 1000; // clamps to 100, same as WaterLevel does
Console.WriteLine($"{animals[0].Name} happiness after a very enthusiastic pet: {animals[0].Happiness}");
