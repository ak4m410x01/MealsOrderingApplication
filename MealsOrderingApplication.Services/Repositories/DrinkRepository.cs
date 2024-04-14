﻿using MealsOrderingApplication.Data.DbContext;
using MealsOrderingApplication.Domain.DTOs.DrinkDTO;
using MealsOrderingApplication.Domain.Entities;
using MealsOrderingApplication.Domain.Interfaces;

namespace MealsOrderingApplication.Services.Repositories
{
    public class DrinkRepository : BaseRepository<Drink>, IDrinkRepository
    {
        public DrinkRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Drink> MapAddDtoToEntity<TDto>(TDto dto)
        {
            if (dto is AddDrinkDTO addDto)
            {
                return await Task.FromResult(new Drink
                {
                    Name = addDto.Name,
                    Description = addDto.Description,
                    Price = addDto.Price,
                    CategoryId = addDto.CategoryId,
                });
            }

            throw new ArgumentException("Invalid DTO type. Expected AddDrinkDTO.");
        }

        public override async Task<Drink> MapUpdateDtoToEntity<TDto>(Drink entity, TDto dto)
        {
            if (dto is UpdateDrinkDTO updateDto)
            {
                if (updateDto.Name is not null)
                    entity.Name = updateDto.Name;

                if (updateDto.Description is not null)
                    entity.Description = updateDto.Description;

                if (updateDto.Price is not null)
                    entity.Price = updateDto.Price ?? default;

                if (updateDto.CategoryId is not null)
                    entity.CategoryId = (int)updateDto.CategoryId;

                return await Task.FromResult(entity);
            }
            throw new ArgumentException("Invalid DTO type. Expected UpdateDrinkDTO.");
        }
    }
}
