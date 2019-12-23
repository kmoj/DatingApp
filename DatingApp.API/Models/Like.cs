namespace DatingApp.API.Models
{
    public class Like
    {
        //User liker id
        public int LikerId { get; set; }
        //User Likee id
        public int LikeeId { get; set; }
        public virtual User Liker { get; set; }
        public virtual User Likee { get; set; }
    }
}