let prevBtn;
let nextBtn;
let pageNo = 1;
let pageSize = 5;
let totalPages = 1;
let searchKey = null;

window.onload = handleLoad;

async function handleLoad() {
  const searchInp = document.getElementById("searchInput");
  searchInp.onkeyup = handleSearch;

  prevBtn = document.getElementById("btnPrev");
  nextBtn = document.getElementById("btnNext");

  prevBtn.onclick = handlePrev;
  nextBtn.onclick = handleNext;

  await getInventories();
}

async function getInventories() {
  const res = await getAllInventories(searchKey, pageNo, pageSize);
  totalPages = Math.ceil(res.Total / pageSize);
  console.log(totalPages);
  addDateToPage(res.Inventories);
  updatePrevBtnState();
  updateNextBtnState();
}

function addDateToPage(data) {
  const bodyEl = document.getElementById("tBody");
  bodyEl.innerHTML = "";
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

async function handleSearch(e) {
  e.preventDefault();
  const value = e.target.value;
  searchKey = value.length >= 3 ? value : null;
  pageNo = 1;
  await getInventories();
}

async function handlePrev() {
  pageNo--;
  await getInventories();
}

async function handleNext() {
  pageNo++;
  await getInventories();
}

function updatePrevBtnState() {
  if (pageNo === 1) {
    disableBtn(prevBtn);
  } else {
    enableBtn(prevBtn);
  }
}

function updateNextBtnState() {
  if (pageNo === totalPages) {
    disableBtn(nextBtn);
  } else {
    enableBtn(nextBtn);
  }
}

function disableBtn(btn) {
  btn.disabled = true;
}

function enableBtn(btn) {
  btn.disabled = false;
}
