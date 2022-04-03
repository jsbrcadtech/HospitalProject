let itemId;

window.onload = handleLoad;

async function handleLoad() {
  // get item id from url
  const splits = window.location.toString().split("/");
  itemId = splits[splits.length - 1];

  // add event listener to form
  const form = document.forms.updateInventory;
  form.onsubmit = handleSubmit;

  // get item details and add to the page
  const item = await getInventoryItemById(itemId);
  addInventoryItemToPage(item);
}

function addInventoryItemToPage(item) {
  const nameEl = document.getElementById("name");
  nameEl.innerHTML = item.Name;

  const quantityEl = document.getElementById("quantity");
  quantityEl.innerHTML = item.BaseQuantity;
}

async function handleSubmit(e) {
  e.preventDefault();

  // get form data
  const form = e.target;
  const data = {
    delta: form.delta.value,
  };

  // make api call to update ledger
  const res = await updateLedger(itemId, data);

  if (res) {
    // refresh page
    window.location.replace(`/inventory/show/${itemId}`);
  } else {
    // show errors if any
    const msgEl = document.getElementById("errMsg");
    msgEl.innerHTML = "Something went wrong...";
  }
}
