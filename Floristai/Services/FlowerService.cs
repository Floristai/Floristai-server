﻿using Floristai.Models;
using Floristai.Repositories;

namespace Floristai.Services
{
    public class FlowerService : IFlowerService
    {
        private readonly IFlowerRepository _flowerRepository;

        public FlowerService(IFlowerRepository flowerRepository, IJwtKeyHoldingService jwtKeyHoldingService)
        {
            _flowerRepository = flowerRepository;
        }

        public async Task<List<Flower>> getAll()
        {
            return await _flowerRepository.getAll();
        }
    }
}