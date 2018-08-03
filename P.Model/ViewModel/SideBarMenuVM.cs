using System.Collections.Generic;

namespace P.Model.ViewModel
{
    public class SideBarMenuVM
    {
        public SideBarMenuVM()
        {
            this.ChildMenuList = new List<SideBarMenuVM>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string LinkUrl { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public List<SideBarMenuVM> ChildMenuList { get; set; }
    }
}
