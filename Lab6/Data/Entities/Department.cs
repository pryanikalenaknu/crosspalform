namespace Lab6.Data.Entities;

public class Department
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public string DepartmentDetails { get; set; }
    
    public ICollection<StaffDepartmentAssignment> StaffDepartmentAssignments { get; set; }
}