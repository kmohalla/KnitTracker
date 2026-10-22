using System;
using System.Collections.Generic;
using System.Text;

namespace KnittingData
{
	//I included this because it was part of the Microsoft learn tutorial that I used to guide my development. It is supposed to create and fill a test database if the target database can be found. However, when I tested it, it did not work. I will leave it here in case I can get it to work later.
	public static class DbInitializer
	{
		public static void Initialize(KnittingContext context)
		{
			context.Database.EnsureCreated();
			// Look for any patterns.
			if (context.Patterns.Any())
			{
				return;   // DB has been seeded
			}

			var yarns = new Yarn[]
			{
				new Yarn{Id=1, Name="Yarn 1", Fiber="Wool", Weight="DK", ColorName="Red", BrandId=1},
				new Yarn {Id=2, Name="Yarn 2", Fiber="Cotton", Weight="Sport", ColorName="Blue", BrandId=2},
				new Yarn {Id=3, Name="Yarn 3", Fiber="Acrylic", Weight="Worsted", ColorName="Green", BrandId=3},
			};
			context.Yarns.AddRange(yarns);
			context.SaveChanges();

			var brands = new Brand[]
			{
				new Brand{Id=1, Name="Brand 1", Country="USA"},
				new Brand {Id=2, Name="Brand 2", Country="UK"},
				new Brand {Id=3, Name="Brand 3", Country="Canada"},
			};

			var needles = new Needle[]
			{
				new Needle{Id=1, SizeMm=3.5M, Type="Circular", Material="Bamboo", LengthCm=40},
				new Needle {Id=2, SizeMm=4.0M, Type="Straight", Material="Metal", LengthCm=30},
				new Needle {Id=3, SizeMm=5.0M, Type="Double Pointed", Material="Plastic", LengthCm=20},
			};
			context.Needles.AddRange(needles);
			context.SaveChanges();

			var patterns = new Pattern[]
			{
				new Pattern{Id=1, Name="Pattern 1", Designer="Designer 1", Category="Category 1", Difficulty="Easy"},
				new Pattern {Id=2, Name="Pattern 2", Designer="Designer 2", Category="Category 2", Difficulty="Medium"},
				new Pattern {Id=3, Name="Pattern 3", Designer="Designer 3", Category="Category 3", Difficulty="Hard"},
			};
			foreach (Pattern p in patterns)
			{
				context.Patterns.Add(p);
			}
			context.SaveChanges();

			var projects = new Project[]
			{
				new Project{Id=1, Name="Project 1", Status="In Progress", StartDate=DateTime.Parse("2023-01-01"), PatternId=1, YarnId=1, NeedleId=1},
				new Project {Id=2, Name="Project 2", Status="Completed", StartDate=DateTime.Parse("2023-02-01"), PatternId=2, YarnId=2, NeedleId=2},
				new Project {Id=3, Name="Project 3", Status="Not Started", StartDate=DateTime.Parse("2023-03-01"), PatternId=3, YarnId=3, NeedleId=3},
			};
			context.Projects.AddRange(projects);
			context.SaveChanges();
			

		}
	}

}
