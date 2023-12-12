namespace StudentDetailsInDigitalPlatform.ViewModels
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }

        public List<ManageUserViewModel> Users { get; set; } = new List<ManageUserViewModel>();
    }
}
