using aspcore_watchshop.Daos;
using aspcore_watchshop.EF;
using aspcore_watchshop.Hepler;
using aspcore_watchshop.Entities;

namespace aspcore_watchshop.Models
{
    public interface IPostModel
    {
        PostVM GetPostVM(watchContext context, int idPost);
    }
    public class PostModel : IPostModel
    {
        public PostVM GetPostVM(watchContext ctext, int id)
        {
            Post asset;
            using (PostDao db = new PostDao())
                asset = db.Get(ctext, id);
            return Helper.ObjectToVM<PostVM, Post>(asset);
        }
    }

    public class PostVM
    {
        public int ProductID { get; set; }
        public string PostContent { get; set; }
    }
}