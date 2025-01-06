namespace MY_API_PROJECT.DTO.ReviewDTOS
{
    public class ReviewCreateDTO
    {

        public int UserID { get; set; }
        public int productId {  get; set; }
        public int Rating { get; set; }
        public string comment { get; set; }
        
        public DateTime CreatedAt { get; set; } 

    }
}
