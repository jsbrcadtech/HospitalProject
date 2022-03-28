window.onload = handleLoad;

async function handleLoad() {
  const items = await getAllInventories();
  addDateToPage(items);
}

function addDateToPage(data) {
  const bodyEl = document.getElementById("tBody");
  data.forEach((e) => {
    const trEl = `
        <tr>
            <td><a href='/inventory/show/${e.Id}'>${e.Name}</a></td>
            <td>${e.BaseQuantity}</td>
        </tr>
      `;
    bodyEl.innerHTML += trEl;
  });
}
