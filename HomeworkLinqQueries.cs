using System;
using System.Linq;
using StudentKlasa;
using System.Collections.Generic;

namespace LinqQueries
{
    public class HomeworkLinqQueries
    {
        public static string[] Linq1(int[] intArray)
        {
            return intArray.Distinct().OrderBy(x => x).Select(i => $"Broj {i} ponavlja se {intArray.Count(s => s == i)} puta").ToArray();
        }

        public static University[] Linq2_1(University[] universityArray)
        {
            return universityArray.Where(s => s.Students.All(x => x.Gender == Gender.Male)).ToArray();
        }

        public static University[] Linq2_2(University[] universityArray)
        {
            return universityArray.Where(u => u.Students.Length < universityArray.Average(s => s.Students.Length)).ToArray();
        }

        public static Student[] Linq2_3(University[] universityArray)
        {
            return universityArray
                .Select(s => new University { Name = s.Name, Students = s.Students.Distinct().ToArray() })
                .SelectMany(stu => stu.Students).Distinct().ToArray();
        }

        public static Student[] Linq2_4(University[] universityArray)
        {
            return universityArray
                .Select(s => new University { Name = s.Name, Students = s.Students.Distinct().ToArray() }).Where(s =>
                    s.Students.Count() == s.Students.Count(i => i.Gender == Gender.Male) ||
                    s.Students.Count() == s.Students.Count(i => i.Gender == Gender.Female)).SelectMany(stu => stu.Students).Distinct().ToArray();
        }

        public static Student[] Linq2_5(University[] universityArray)
        {
            return universityArray
                .Select(s => new University { Name = s.Name, Students = s.Students.Distinct().ToArray() })
                .SelectMany(stu => stu.Students).GroupBy(x => x).Where(group => group.Count() > 1)
                .Select(group => group.Key).ToArray();
        }
    }
}


public class University
{
    public string Name { get; set; }
    public Student[] Students { get; set; }
}
