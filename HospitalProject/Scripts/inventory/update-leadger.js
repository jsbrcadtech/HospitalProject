let itemId;

window.onload = handleLoad;

async function handleLoad() {
  const splits = window.location.toString().split("/");
  itemId = splits[splits.length - 1];

  const form = document.forms.updateInventory;
  form.onsubmit = handleSubmit;

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

  const form = e.target;
  const data = {
    delta: form.delta.value,
  };

  const res = await updateLedger(itemId, data);

  if (res) {
    window.location.replace(`/inventory/show/${itemId}`);
  } else {
    const msgEl = document.getElementById("errMsg");
    msgEl.innerHTML = "Something went wrong...";
  }
}
