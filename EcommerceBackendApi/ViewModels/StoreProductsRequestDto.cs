namespace EcommerceBackendApi.ViewModels
{
    public class StoreProductsRequestDto
    {
        public int StoreId { get; set; }
        public int UniqueStoreId { get; set; }
        public string Name { get; set; }
        public List<GetProductsRequestDto> ProductsList { get; set; }    

    }
}
