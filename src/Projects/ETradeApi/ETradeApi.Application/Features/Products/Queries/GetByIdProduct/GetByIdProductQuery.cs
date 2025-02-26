﻿using AutoMapper;
using Core.Application.Pipelines.Authorization;
using ETradeApi.Application.Features.Products.Dtos;
using ETradeApi.Application.Services.Repositories;
using ETradeApi.Domain.Entities;
using MediatR;
using static ETradeApi.Domain.Constants.OperationClaims;

namespace ETradeApi.Application.Features.Products.Queries.GetByIdProduct
{
    public class GetByIdProductQuery : IRequest<GetByIdProductDto>, ISecuredRequest
    {
        public int Id { get; set; }
        public string[] Roles => new[] { Admin };

        public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, GetByIdProductDto>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public GetByIdProductQueryHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }


            public async Task<GetByIdProductDto> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
            {
                Product? product = await _productRepository.GetAsync(b => b.Id == request.Id);
                GetByIdProductDto getByIdProductdDto = _mapper.Map<GetByIdProductDto>(product);
                return getByIdProductdDto;
            }
        }
    }
}
