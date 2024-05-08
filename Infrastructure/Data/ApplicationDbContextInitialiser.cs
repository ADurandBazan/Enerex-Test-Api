using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

          
            await initialiser.SeedAsync();
        }
    }
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, 
                                               ApplicationDbContext context,
                                               IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

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
