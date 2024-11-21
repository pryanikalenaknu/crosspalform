namespace Lab6.Data.Entities;

public class StaffDepartmentAssignment
{
    public int StaffId { get; set; }
    public int DepartmentId { get; set; }
    public DateTime DateAssignedTo { get; set; }

    public Staff Staff { get; set; }
    public Department Department { get; set; }
}
