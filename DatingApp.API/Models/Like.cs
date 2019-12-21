namespace DatingApp.API.Models
{
    public class Like
    {
        //User liker id
        public int LikerId { get; set; }
        //User Likee id
        public int LikeeId { get; set; }
        public User Liker { get; set; }
        public User Likee { get; set; }
    }
}