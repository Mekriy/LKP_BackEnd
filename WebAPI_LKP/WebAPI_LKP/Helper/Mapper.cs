﻿using AutoMapper;
using WebAPI_LKP.DTO;
using WebAPI_LKP.Models;

namespace WebAPI_LKP.Helper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, UserLoginDTO>();
            CreateMap<User, UserSignUpDTO>();
            CreateMap<UserLoginDTO, User>();
            CreateMap<UserSignUpDTO, User>();

            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();

            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();
        }
    }
}
