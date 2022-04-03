window.onload = handleLoad;

function handleLoad() {
  const form = document.forms.addInventory;
  form.onsubmit = handleSubmit;
}

async function handleSubmit(e) {
  e.preventDefault();

  // get form data
  const form = e.target;
  const item = {
    name: form.name.value,
    baseQuantity: form.baseQuantity.value,
  };

  // make api call
  const success = await addItemToInventory(item);

  if (success) {
    // navigate to list page
    window.location.replace("/inventory");
  } else {
    // display error message
    const msgEl = document.getElementById("errMsg");
    msgEl.innerHTML = "Something went wrong...";
  }
}
