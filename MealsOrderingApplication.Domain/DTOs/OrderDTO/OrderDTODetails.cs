﻿using MealsOrderingApplication.Domain.Interfaces.DTOs;

namespace MealsOrderingApplication.Domain.DTOs.OrderDTO
{
    public class OrderDTODetails : BaseOrderDTO, IDetailsDTO
    {
        public int Id { get; set; }
    }
}
