using AutoMapper;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models;

namespace UI.Utils
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //GVM <- game
            CreateMap<Game, GameViewModel>()
                .ForMember(x => x.Developer, opt => opt.MapFrom(x => x.Developer.Name))
                .ForMember(x => x.Genre, opt => opt.MapFrom(x => x.Genre.Name));

            //game <- GMV
            CreateMap<GameViewModel, Game>()
                .ForMember(x => x.Developer, opt => opt.MapFrom(x => new Developer { Name = x.Developer }))
                 .ForMember(x => x.Genre, opt => opt.MapFrom(x => new Genre { Name = x.Genre }));

            CreateMap<Genre, GenreViewModel>();
            CreateMap<GenreViewModel, Genre>();

            CreateMap<Developer, DeveloperViewModel>();
            CreateMap<DeveloperViewModel, Developer>();

            CreateMap<GamesInCart, GamesInCartViewModel>();
            CreateMap<GamesInCartViewModel, GamesInCart>();

            CreateMap<UserCart, UserCartViewModel>();
            CreateMap<UserCartViewModel, UserCart>();

            CreateMap<OrderDetails, OrderDetailsViewModel>();
            CreateMap<OrderDetailsViewModel, OrderDetails>();
        }
    }
}