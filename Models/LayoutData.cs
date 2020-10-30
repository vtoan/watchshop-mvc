using aspcore_watchshop.EF;
using aspcore_watchshop.ViewModels;

namespace aspcore_watchshop.Models
{
    public sealed class LayoutData
    {
        private static readonly LayoutData _instance = new LayoutData();
        public static LayoutData Instance { get { return _instance; } }
        private LayoutData() { }

        public string NamePage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string NameStore { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Messenger { get; set; }
        public string Instargram { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string WorkTime { get; set; }
        public string StoreDescription { get; set; }
        public string URL { get; set; }

        public void SetForInfo(Info data)
        {
            if (data == null) return;
            NameStore = data.NameStore;
            Title = data.SeoTitle;
            Description = data.SeoDescription;
            Image = data.SeoImage;
            Logo = data.Logo;
            Email = data.Email;
            Facebook = data.Facebook;
            Messenger = data.Messenger;
            Instargram = data.Instargram;
            Phone = data.Phone;
            Address = data.Address;
            WorkTime = data.WorkTime;
            StoreDescription = data.SeoDescription;
        }

        public void SetForCategory(CategoryVM data = null)
        {
            if (data == null)
                NamePage = "Khuyễn mãi";
            else
            {
                NamePage = data.Name;
                Title = data.SeoTitle;
                Description = data.SeoDescription;
                Image = data.SeoImage;
            }
        }

        public void SetForProduct(ProdDetailVM data)
        {
            if (data == null) return;
            Title = data.SeoTitle;
            Description = data.SeoDescription;
            Image = data.SeoImage;
        }

        public void SetPageTitle(string page, string title = null)
        {
            NamePage = page;
            Title = title == null ? page : title;
        }
    }
}