﻿using HospitalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HospitalProject.Controllers
{
    [RoutePrefix("api/inventory")]
    public class InventoryDataController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(Inventory inventory)
        {
            _db.Inventories.Add(inventory);

            var ledger = new InventoryLedger()
            {
                InventoryId = inventory.Id,
                Delta = inventory.BaseQuantity,
                CreationDate = DateTime.Now,
            };

            _db.InventoryLedgers.Add(ledger);

            _db.SaveChanges();

            return Created($"/api/inventory/{inventory.Id}", inventory);
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(_db.Inventories);
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var inventory = _db.Inventories.SingleOrDefault(i => i.Id == id);

            if (inventory == null)
            {
                return NotFound();
            }

            return Ok(inventory);
        }

        [Route("{id:int}/ledger")]
        [HttpPost]
        public IHttpActionResult UpdateInventory(
            [FromUri] int id,
            [FromBody] InventoryLedger ledger)
        {
            var inventory = _db.Inventories.SingleOrDefault(i => i.Id == id);

            if (inventory == null)
            {
                return NotFound();
            }

            inventory.BaseQuantity += ledger.Delta;

            ledger.InventoryId = id;
            ledger.CreationDate = DateTime.Now;

            _db.InventoryLedgers.Add(ledger);
            _db.SaveChanges();

            return Ok();
        }

        [Route("{id:int}/ledger")]
        [HttpGet]
        public IHttpActionResult Getledgers(
            [FromUri] int id,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var inventory = _db.Inventories.SingleOrDefault(i => i.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            var ledgers = _db.InventoryLedgers
                .Where(l => l.InventoryId == id)
                .OrderByDescending(l => l.CreationDate)
                .ToList();

            var ledgersToReturn = ledgers.Select(l => new InventoryLedger
            {
                Id = l.Id,
                InventoryId = l.InventoryId,
                CreationDate = l.CreationDate,
                Delta = l.Delta,
            });

            if (startDate != null && endDate != null)
            {
                if (startDate.Value.Date > endDate.Value.Date)
                {
                    return BadRequest("Start date cannot be later than end date.");
                }

                ledgersToReturn = ledgersToReturn
                    .Where(l => l.CreationDate.Date >= startDate
                        && l.CreationDate.Date <= endDate);
            }

            return Ok(ledgersToReturn);
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteInventory([FromUri] int id)
        {
            var inventoryInDb = _db.Inventories.SingleOrDefault(i => i.Id == id);
            if (inventoryInDb == null)
            {
                return NotFound();
            }

            var ledgers = _db.InventoryLedgers.Where(l => l.InventoryId == id);

            _db.Inventories.Remove(inventoryInDb);
            _db.InventoryLedgers.RemoveRange(ledgers);

            _db.SaveChanges();

            return Ok();
        }

        [Route("ledger/{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteLedgerEntry([FromUri] int id)
        {
            var ledgerEntryInDb = _db.InventoryLedgers.SingleOrDefault(i => i.Id == id);
            if (ledgerEntryInDb == null)
            {
                return NotFound();
            }

            var inventoryItem = _db.Inventories.SingleOrDefault(i => i.Id == ledgerEntryInDb.InventoryId);
            inventoryItem.BaseQuantity -= ledgerEntryInDb.Delta;

            _db.InventoryLedgers.Remove(ledgerEntryInDb);
            _db.SaveChanges();

            return Ok();
        }
    }
}