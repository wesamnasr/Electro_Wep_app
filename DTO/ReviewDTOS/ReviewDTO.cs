namespace MY_API_PROJECT.DTO.ReviewDTOS
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }


        public int UserID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

    }

}
