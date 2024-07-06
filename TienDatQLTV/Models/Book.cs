namespace TienDatQLTV.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public int AuthorID { get; set; }
        public string Publisher { get; set; }
        public int YearPublished { get; set; }
        public string ISBN { get; set; }
        public int CategoryID { get; set; }
        public int Quantity { get; set; }
        public int Available { get; set; }
        public string ImageUrl { get; set; }
        public Author Author { get; set; }
        public Category Category { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}
