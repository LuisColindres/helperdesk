namespace HelperDesk.API.Dtos
{
    public class DepartmentForUpdateDto
    {
        public string Description { get; set; }
        public int Status { get; set; }
        public bool add { get; set; }
        public bool edit { get; set; }
        public bool delete { get; set; }
    }
}