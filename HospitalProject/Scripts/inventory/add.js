window.onload = handleLoad;

function handleLoad() {
  const form = document.forms.addInventory;
  form.onsubmit = handleSubmit;
}

async function handleSubmit(e) {
  e.preventDefault();

  const form = e.target;
  const item = {
    name: form.name.value,
    baseQuantity: form.baseQuantity.value,
  };

  const success = await addItemToInventory(item);

  if (success) {
    window.location.replace("/inventory");
  } else {
    const msgEl = document.getElementById("errMsg");
    msgEl.innerHTML = "Something went wrong...";
  }
}
