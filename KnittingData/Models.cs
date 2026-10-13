namespace KnittingData;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Country { get; set; } = "";
    public List<Yarn> Yarns { get; set; } = new();
}

public class Yarn
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Fiber { get; set; } = "";
    public string Weight { get; set; } = "";
    public string ColorName { get; set; } = "";
    public int? BrandId { get; set; }
    public Brand? Brand { get; set; }
}

public class Needle
{
    public int Id { get; set; }
    public decimal SizeMm { get; set; }
    public string Type { get; set; } = "";
    public string Material { get; set; } = "";
    public int LengthCm { get; set; }
}

public class Pattern
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Designer { get; set; } = "";
    public string Category { get; set; } = "";
    public string Difficulty { get; set; } = "";
}

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Status { get; set; } = "";
    public DateTime StartDate { get; set; }
    public int? PatternId { get; set; }
    public Pattern? Pattern { get; set; }
    public int? YarnId { get; set; }
    public Yarn? Yarn { get; set; }
    public int? NeedleId { get; set; }
    public Needle? Needle { get; set; }
}
