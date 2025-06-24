namespace Student_Management.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }

        public ICollection<Course> Courses { get; set; }
        public ICollection<Lecturer> Lecturers { get; set; }
    }
}
