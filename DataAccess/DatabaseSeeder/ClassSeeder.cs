using Domain.Models;

namespace DataAccess.DatabaseSeeder
{

    public class ClassSeeder
    {
        private readonly SocialDbContext _context;

        public ClassSeeder(SocialDbContext context)
        {
            _context = context;
        }

        public void SeedClasses()
        {
            // Check for existing data to avoid duplicates
            if (_context.ClassInSchools.Any())
            {
                return; // Exit if classes already exist
            }
            var classesToSeed = new List<ClassInSchool>();
            // Create a list of class names to seed
            var classNames = new List<string> { "JSS1", "JSS2", "JSS3", "SS1", "SS2", "SS3" };

            // Define classes to seed
            classNames.ForEach(x => { classesToSeed.Add(new ClassInSchool { ClassName = x }); });


            // Add classes to the context and save
            _context.AddRange(classesToSeed);
            _context.SaveChanges();
        }
    }

}
