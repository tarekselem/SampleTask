using AutoMapper;
using System;

namespace Futura.BusinessOperations.Configurations
{
    public class MappingConfigurations
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                #region From XML to Entity
                config.CreateMap<XmlModels.Customer, Entities.Customer>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.CustomerID))
                .ForMember(dest => dest.ContactTile, opts => opts.MapFrom(src => src.ContactTitle))
                .ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.FullAddress.Address))
                .ForMember(dest => dest.City, opts => opts.MapFrom(src => src.FullAddress.City))
                .ForMember(dest => dest.Region, opts => opts.MapFrom(src => src.FullAddress.Region))
                .ForMember(dest => dest.PostalCode, opts => opts.MapFrom(src => src.FullAddress.PostalCode))
                .ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.FullAddress.Country));

                config.CreateMap<XmlModels.Order, Entities.Order>()
                .ForMember(dest => dest.CustomerId, opts => opts.MapFrom(src => src.CustomerID))
                .ForMember(dest => dest.ShippedDate, opts => opts.MapFrom(src => src.ShipInfo.ShippedDate == DateTime.MinValue ? new DateTime(1900, 1, 1) : src.ShipInfo.ShippedDate))
                .ForMember(dest => dest.ShipVia, opts => opts.MapFrom(src => src.ShipInfo.ShipVia))
                .ForMember(dest => dest.Freight, opts => opts.MapFrom(src => src.ShipInfo.Freight))
                .ForMember(dest => dest.ShipName, opts => opts.MapFrom(src => src.ShipInfo.ShipName))
                .ForMember(dest => dest.ShipAddress, opts => opts.MapFrom(src => src.ShipInfo.ShipAddress))
                .ForMember(dest => dest.ShipCity, opts => opts.MapFrom(src => src.ShipInfo.ShipCity))
                .ForMember(dest => dest.ShipRegion, opts => opts.MapFrom(src => src.ShipInfo.ShipRegion))
                .ForMember(dest => dest.ShipPostalCode, opts => opts.MapFrom(src => src.ShipInfo.ShipPostalCode))
                .ForMember(dest => dest.ShipCountry, opts => opts.MapFrom(src => src.ShipInfo.ShipCountry));
                #endregion

                #region From Binding Models to Entity
                config.CreateMap<BindingModels.Customer, Entities.Customer>();
                #endregion

                #region From Entity to View Model
                config.CreateMap<Entities.Customer,ViewModels.CustomerDetails>();
                #endregion

            });
        }
    }
}
