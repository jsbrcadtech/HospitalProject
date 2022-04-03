let itemId;
window.onload = handleLoad;

async function handleLoad() {
  // get item id from url
  const splits = window.location.toString().split("/");
  itemId = splits[splits.length - 1];

  // add event listener for filter from
  const form = document.forms.filter;
  form.onsubmit = handleSubmit;

  // add event listener to delete button
  const deleteBtn = document.getElementById("btnDelete");
  deleteBtn.onclick = handleDelete;

  // get item from api and add to the page
  const item = await getInventoryItemById(itemId);
  addInventoryItemToPage(item);

  // get ledger entries for the item and update page
  const ledgers = await getLedgerForItem(itemId);
  addLedgerDataToPage(ledgers);
}

function addInventoryItemToPage(item) {
  const nameEl = document.getElementById("name");
  nameEl.innerHTML = item.Name;

  const quantityEl = document.getElementById("quantity");
  quantityEl.innerHTML = item.BaseQuantity;
}

function addLedgerDataToPage(ledgers) {
  const bodyEl = document.getElementById("tBody");

  // clear data from table
  bodyEl.innerHTML = "";

  // add rows for each ledger entry
  ledgers.forEach((l) => {
    const trEl = `
        <tr>
            <td>${l.CreationDate}</td>
            <td>${l.Delta}</td>
            <td><button class="btn" onclick="deleteLedger(${l.Id})">Delete</button></td>
        </tr>
      `;
    bodyEl.innerHTML += trEl;
  });
}

async function handleDelete() {
  // confirm before delete
  const ans = confirm(
    "Delete inventory item? This will remove item and all ledger history."
  );

  if (ans) {
    await deleteItemFromInventory(itemId);
    window.location.replace("/inventory");
  }
}

async function handleSubmit(e) {
  e.preventDefault();
  const msgEl = document.getElementById("errMsg");

  // get filter data from form
  const form = e.target;
  const start = form.startDate.value;
  const end = form.endDate.value;

  // validate date values
  const startDate = new Date(start);
  const endDate = new Date(end);
  if (startDate > endDate) {
    msgEl.innerHTML = "Start date cannot be later than end date.";
  }

  // get ledger entries based on the date values
  const ledgers = await getLedgerForItem(itemId, start, end);
  addLedgerDataToPage(ledgers);

  if (!ledgers.length) {
    msgEl.innerHTML = "No data found for the selected date range.";
  }
}

async function deleteLedger(ledgerId) {
  // confim before delete
  const ans = confirm("Delete ledger entry?");

  if (ans) {
    const res = await deleteLedgerById(ledgerId);
    if (res) {
      // refresh page after deleting ledger entry
      window.location.reload();
    } else {
      // show error message
      const msgEl = document.getElementById("errMsg");
      msgEl.innerHTML = "Something went wrong...";
    }
  }
}
