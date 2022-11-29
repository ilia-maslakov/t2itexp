using t2itexp.Data.EF;

namespace t2itexp.Models
{
    public class PhoneList
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int LastPage { get; set; }
        public int PagesOnScreen { get; set; }

        public IEnumerable<Phone>? Phones { get; set; }
    }
}