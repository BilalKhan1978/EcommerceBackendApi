namespace EcommerceBackendApi.ViewModels
{
    public class UpdateProductRequestDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int UniqueStoreId { get; set; }  //fk
        public string Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
    }
}
