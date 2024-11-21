namespace Lab6.Data.Entities;

public class Staff
{
    public int StaffId { get; set; }
    public string StaffName { get; set; }
    public int JobTitleCode { get; set; }
    public int DeptStoreId { get; set; }
    
    public RefJobTitle JobTitle { get; set; }
    public DepartmentStore DepartmentStore { get; set; }
    public ICollection<StaffDepartmentAssignment> StaffDepartmentAssignments { get; set; }
}