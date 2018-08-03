namespace P.Model.ViewModel
{
    public class ZTreeVM
    {
        public double id { get; set; }

        public int? pId { get; set; }

        public string name { get; set; }

        public bool @checked
        {
            get;
            set;
        }
        public bool isParent { get; set; }

        public bool open { get; set; }
    }
}
