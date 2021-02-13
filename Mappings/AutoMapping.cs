using AutoMapper;

public class AutoMapping : Profile {
    public AutoMapping() {
        CreateMap<Customer, CustomerDTO>();
        CreateMap<CustomerDTO, Customer>();
        
        CreateMap<Product, ProductDTO>();
        CreateMap<ProductDTO, Product>();

        CreateMap<Order, OrderDTO>();
        CreateMap<OrderDTO, Order>();

        CreateMap<OrderDetails, OrderDetailsDTO>();
        CreateMap<OrderDetailsDTO, OrderDetails>();

        CreateMap<Size, SizeDTO>();
        CreateMap<SizeDTO, Size>();
    }

}