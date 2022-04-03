using HospitalProject.Models;
using HospitalProject.Models.Dto;
using System;
using System.Linq;
using System.Web.Http;

namespace HospitalProject.Controllers
{
    [RoutePrefix("api/inventory")]
    public class InventoryDataController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        /// <summary>
        /// POST: api/inventory
        /// api to add new item to the inventory
        /// </summary>
        /// <requestBody>
        /// {
        ///     "Name": "Gloves",
        ///     "BaseQuantity": 500
        /// }
        /// </requestBody>
        /// <returns>
        /// Ok
        /// {
        ///     "Id": 1,
        ///     "Name": "Gloves",
        ///     "BaseQuantity": 500
        /// }
        /// BadRequest - if request body is invalid
        /// </returns>
        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(InventoryDto dto)
        {
            // validate request object
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // add item to inventory
            var inventory = new Inventory
            {
                Name = dto.Name,
                BaseQuantity = dto.BaseQuantity,
            };
            _db.Inventories.Add(inventory);

            // add ledger entry for initial quantity
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

        /// <summary>
        /// GET: api/inventory
        /// api to get all items from inventory
        /// supports search by item name and pagination
        /// </summary>
        /// <param name="searchKey">search key for item name</param>
        /// <param name="pageNo">page no for pagination</param>
        /// <param name="pageSize">page size for pagination</param>
        /// <returns>
        /// OK
        /// {
        ///     "Inventories": [
        ///         {
        ///             "Id": 12,
        ///             "Name": "Ball",
        ///             "BaseQuantity": 519
        ///         },
        ///         {
        ///             "Id": 1002,
        ///             "Name": "Masks",
        ///             "BaseQuantity": 500
        ///         }
        ///     ],
        ///     "PageSize": 10,
        ///     "Total": 2
        /// }
        /// </returns>
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll(
            string searchKey = null,
            int pageNo = 1,
            int pageSize = 10)
        {
            // get all items
            var item = _db.Inventories.AsQueryable();

            // get no of items in the db
            var count = _db.Inventories.Count();

            // add where clause based on search key
            if (searchKey != null)
            {
                item = item.Where(i => i.Name.Contains(searchKey));
                count = _db.Inventories.Where(i => i.Name.Contains(searchKey)).Count();
            }

            // apply pagination
            item = item.OrderBy(i => i.Id).Skip((pageNo - 1) * pageSize).Take(pageSize);

            // create response object
            var result = new PagedDto()
            {
                Inventories = item.ToList(),
                PageSize = pageSize,
                Total = count
            };

            return Ok(result);
        }

        /// <summary>
        /// GET: /api/inventory/{id}
        /// api to fetch item by id
        /// </summary>
        /// <param name="id">int id of the item to fetch</param>
        /// <returns>
        /// OK
        /// {
        ///     "Id": 1002,
        ///     "Name": "Masks",
        ///     "BaseQuantity": 500
        /// }
        /// NotFound - if item with requested id is not in the DB
        /// </returns>
        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            // check if item exists in the db
            var inventory = _db.Inventories.SingleOrDefault(i => i.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return Ok(inventory);
        }

        /// <summary>
        /// POST: api/inventory/{id}/ledger
        /// api to add ledger entries for an item
        /// </summary>
        /// <param name="id">int id of the item</param>
        /// <param name="ledger">leadger details to add</param>
        /// <requestBody>
        /// {
        ///     "delta": -16
        /// }
        /// </requestBody>
        /// <returns>
        /// OK
        /// NotFound - if item is not in the DB
        /// BadRequest - if base quantity goes below 0
        /// </returns>
        [Route("{id:int}/ledger")]
        [HttpPost]
        public IHttpActionResult UpdateInventory(
                [FromUri] int id,
                [FromBody] InventoryLedger ledger)
        {
            // check if item exists in the db
            var inventory = _db.Inventories.SingleOrDefault(i => i.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            // update item
            inventory.BaseQuantity += ledger.Delta;

            // validate base quantity
            if (inventory.BaseQuantity < 0)
            {
                return BadRequest("Base quantity cannot be less than 0.");
            }

            // add ledger entry
            ledger.InventoryId = id;
            ledger.CreationDate = DateTime.Now;
            _db.InventoryLedgers.Add(ledger);

            _db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// GET: api/inventory/{id}/ledger
        /// api to fetch ledger entries for an item
        /// supports filtering by date range
        /// </summary>
        /// <param name="id">int id of the item</param>
        /// <param name="startDate">start date for filter</param>
        /// <param name="endDate">end date for filter</param>
        /// <returns>
        /// Ok
        /// [
        ///     {
        ///         "Id": 28,
        ///         "Delta": 11,
        ///         "CreationDate": "2022-03-28T05:04:01.227",
        ///         "InventoryId": 12,
        ///         "Inventories": null
        ///     },
        /// ]
        /// NotFound - if item with requested id is not in th DB
        /// BadRequest - if start date is later than end date in filtered request
        /// </returns>
        [Route("{id:int}/ledger")]
        [HttpGet]
        public IHttpActionResult Getledgers(
            [FromUri] int id,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // check if item exists in the db
            var inventory = _db.Inventories.SingleOrDefault(i => i.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            // fetch all ledger entries for the item ordered by date
            var ledgers = _db.InventoryLedgers
                .Where(l => l.InventoryId == id)
                .OrderByDescending(l => l.CreationDate)
                .ToList();

            // remove unwanted properties
            var ledgersToReturn = ledgers.Select(l => new InventoryLedger
            {
                Id = l.Id,
                InventoryId = l.InventoryId,
                CreationDate = l.CreationDate,
                Delta = l.Delta,
            });

            // apply date filter of parametes are not null
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

        /// <summary>
        /// DELETE: api/inventory/{id}
        /// api to delete an item and all the ledger entries from inventory
        /// </summary>
        /// <param name="id">int id of the item to delete</param>
        /// <returns>
        /// Ok
        /// NotFound - if item with requested id is not in the DB
        /// </returns>
        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteInventory([FromUri] int id)
        {
            // check if item exists in the Db
            var inventoryInDb = _db.Inventories.SingleOrDefault(i => i.Id == id);
            if (inventoryInDb == null)
            {
                return NotFound();
            }

            // retrive all the ledger entries related to the item
            var ledgers = _db.InventoryLedgers.Where(l => l.InventoryId == id);

            // delete data from db
            _db.Inventories.Remove(inventoryInDb);
            _db.InventoryLedgers.RemoveRange(ledgers);

            _db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// DELETE: api/inventory/ledger/{id}
        /// api to delete a ledger entry
        /// </summary>
        /// <param name="id">int id of the ledger entry</param>
        /// <returns>
        /// Ok
        /// NotFound - if there no entry with requested id in Inventory Ledger
        /// </returns>
        [Route("ledger/{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteLedgerEntry([FromUri] int id)
        {
            // check if ledger entry exists in the Db
            var ledgerEntryInDb = _db.InventoryLedgers.SingleOrDefault(i => i.Id == id);
            if (ledgerEntryInDb == null)
            {
                return NotFound();
            }

            // update the base quantity for the item in inventory table
            var inventoryItem = _db.Inventories.SingleOrDefault(i => i.Id == ledgerEntryInDb.InventoryId);
            inventoryItem.BaseQuantity -= ledgerEntryInDb.Delta;

            // remove entry from the Db
            _db.InventoryLedgers.Remove(ledgerEntryInDb);
            _db.SaveChanges();

            return Ok();
        }
    }
}
