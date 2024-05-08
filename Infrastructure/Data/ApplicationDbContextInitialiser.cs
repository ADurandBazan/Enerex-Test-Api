using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    /// <summary>
    /// InitialiserExtensions class for initializing the database
    /// </summary>
    public static class InitialiserExtensions
    {
        /// <summary>
        /// Initialises the database asynchronously
        /// </summary>
        /// <param name="app">WebApplication instance</param>
        /// <returns>Task representing the asynchronous operation</returns>
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

            await initialiser.SeedAsync();
        }
    }

    /// <summary>
    /// ApplicationDbContextInitialiser class for initializing the database with seed data
    /// </summary>
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the ApplicationDbContextInitialiser class
        /// </summary>
        /// <param name="logger">ILogger instance</param>
        /// <param name="context">ApplicationDbContext instance</param>
        /// <param name="configuration">IConfiguration instance</param>
        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger,
                                               ApplicationDbContext context,
                                               IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Seeds the database asynchronously
        /// </summary>
        /// <returns>Task representing the asynchronous operation</returns>
        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        /// <summary>
        /// Attempts to seed the database with default data
        /// </summary>
        /// <returns>Task representing the asynchronous operation</returns>
        public async Task TrySeedAsync()
        {
            // Default data
            // Seed, if necessary
            if (!_context.Students.Any())
            {
                var filePath = _configuration["FilePath"];

                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    var studentsString = File.ReadAllText(filePath);

                    string[] studentStrings = studentsString.Trim().Split('\n');

                    List<Student> students = new List<Student>();

                    foreach (string studentString in studentStrings)
                    {
                        string[] values = studentString.Split(',');
                        if (values.Length != 5)
                        {
                            throw new FormatException("Invalid student string format");
                        }

                        Student student = new Student
                        {
                            Name = $"{values[0]}",
                            Gender = Enum.TryParse(values[1], out Gender gender) ? gender : throw new FormatException("Invalid gender"),
                            Age = int.TryParse(values[2], out int age) ? age : throw new FormatException("Invalid age"),
                            Education = values[3],
                            AcademicYear = int.TryParse(values[4], out int academicYear) ? academicYear : throw new FormatException("Invalid academic year")
                        };

                        students.Add(student);
                    }

                    if (students is not null && students.Any())
                    {
                        _context.Students.AddRange(students);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
