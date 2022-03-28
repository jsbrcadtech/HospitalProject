let itemId;
window.onload = handleLoad;

async function handleLoad() {
  const splits = window.location.toString().split("/");
  itemId = splits[splits.length - 1];

  const form = document.forms.filter;
  form.onsubmit = handleSubmit;

  const deleteBtn = document.getElementById("btnDelete");
  deleteBtn.onclick = handleDelete;

  const item = await getInventoryItemById(itemId);
  addInventoryItemToPage(item);

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
  bodyEl.innerHTML = "";

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

  const form = e.target;
  const start = form.startDate.value;
  const end = form.endDate.value;

  const startDate = new Date(start);
  const endDate = new Date(end);

  if (startDate > endDate) {
    msgEl.innerHTML = "Start date cannot be later than end date.";
  }

  const ledgers = await getLedgerForItem(itemId, start, end);
  addLedgerDataToPage(ledgers);

  if (!ledgers.length) {
    msgEl.innerHTML = "No data found for the selected date range.";
  }
}

async function deleteLedger(ledgerId) {
  const ans = confirm("Delete ledger entry?");

  if (ans) {
    const res = await deleteLedgerById(ledgerId);
    if (res) {
      window.location.reload();
    } else {
      const msgEl = document.getElementById("errMsg");
      msgEl.innerHTML = "Something went wrong...";
    }
  }
}
