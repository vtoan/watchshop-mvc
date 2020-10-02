using aspcore_watchshop.Entities;
namespace aspcore_watchshop.Models
{
    public static class LayoutData
    {
        public static string NamePage { get; set; }
        public static string Title { get; set; }
        public static string Description { get; set; }
        public static string Image { get; set; }
        public static string NameStore { get; set; }
        public static string Logo { get; set; }
        public static string Email { get; set; }
        public static string Facebook { get; set; }
        public static string Messenger { get; set; }
        public static string Instargram { get; set; }
        public static string Phone { get; set; }
        public static string Address { get; set; }
        public static string WorkTime { get; set; }
        public static string StoreDescription { get; set; }
        public static string URL { get; set; }

        public static void SetForInfo(Info data)
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

        public static void SetForCategory(CategoryVM data = null)
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

        public static void SetForProduct(PropDetailVM data)
        {
            if (data == null) return;
            Title = data.SeoTitle;
            Description = data.SeoDescription;
            Image = data.SeoImage;
        }
    }
}