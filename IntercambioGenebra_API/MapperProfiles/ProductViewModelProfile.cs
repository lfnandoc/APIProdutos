using AutoMapper;
using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.ViewModels;

namespace IntercambioGenebraAPI.MapperProfiles
{
    public class ProductViewModelProfile : Profile
    {
        public ProductViewModelProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(viewModel => viewModel.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(viewModel => viewModel.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(viewModel => viewModel.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(viewModel => viewModel.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
