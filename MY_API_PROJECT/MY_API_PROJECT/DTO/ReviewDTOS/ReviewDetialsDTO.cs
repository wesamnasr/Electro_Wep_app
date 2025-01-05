namespace MY_API_PROJECT.DTO.ReviewDTOS
{
    public class ReviewDetialsDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public int Rating {get; set; }
        public string Comment { get; set; }

        public string ReviewDescription { get; set; }

        public DateTime ReviewDate { get; set; }= DateTime.Now;




    }
}
