﻿using Homebroker.Application.DTO;
using Homebroker.Application.Interfaces;
using Homebroker.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Homebroker.API
{
    [ApiController]
    [Route("wallets/{walletId}/assets")]
    public class WalletAssetsController : ControllerBase
    {
        private readonly IWalletAssetService _service;

        public WalletAssetsController(IWalletAssetService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAssets(string walletId)
        {
            return Ok(await _service.GetWalletAssetsByWalletId(walletId));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string walletId, WalletAssetInputDTO input)
        {
            var walletAsset = new WalletAsset()
            {
                WalletId = walletId,
                AssetId = input.AssetId,
                Shares = input.Shares,
            };

            await _service.Create(walletAsset);

            return Ok();
        }
    }
}
