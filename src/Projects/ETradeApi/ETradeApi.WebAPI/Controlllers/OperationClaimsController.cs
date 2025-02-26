﻿using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using ETradeApi.WebAPI.Controllers;
using ETradeApi.Application.Features.OperationClaims.Queries.GetByIdOperationClaim;
using ETradeApi.Application.Features.OperationClaims.Dtos;
using ETradeApi.Application.Features.OperationClaims.Queries.GetListOperationClaim;
using ETradeApi.Application.Features.OperationClaims.Models;
using ETradeApi.Application.Features.OperationClaims.Commands.CreateOperationClaim;
using ETradeApi.Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using ETradeApi.Application.Features.OperationClaims.Commands.DeleteOperationClaim;

namespace ETradeApi.WebAPI.Controlllers
{
    public class OperationClaimsController : BaseController
    {
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdOperationClaimQuery getByIdOperationClaimQuery)
        {
            OperationClaimDto result = await Mediator.Send(getByIdOperationClaimQuery);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOperationClaimQuery getListOperationClaimQuery = new() { PageRequest = pageRequest };
            OperationClaimListModel result = await Mediator.Send(getListOperationClaimQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
        {
            CreatedOperationClaimDto result = await Mediator.Send(createOperationClaimCommand);
            return Created("", result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOperationClaimCommand updateOperationClaimCommand)
        {
            UpdatedOperationClaimDto result = await Mediator.Send(updateOperationClaimCommand);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteOperationClaimCommand deleteOperationClaimCommand)
        {
            DeletedOperationClaimDto result = await Mediator.Send(deleteOperationClaimCommand);
            return Ok(result);
        }
    }
}
