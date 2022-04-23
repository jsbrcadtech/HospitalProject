let prevBtn;
let nextBtn;
let pageNo = 1;
let pageSize = 5;
let totalPages = 1;
let searchKey = null;
let searchTimer;

window.onload = handleLoad;

async function handleLoad() {
  const searchInp = document.getElementById("searchInput");
  searchInp.onkeyup = handleSearch;

  // add event listeners to prev, next buttons for pagination
  prevBtn = document.getElementById("btnPrev");
  nextBtn = document.getElementById("btnNext");
  prevBtn.onclick = handlePrev;
  nextBtn.onclick = handleNext;

  // get all inventories for list
  await getInventories();
}

async function getInventories() {
  const res = await getAllInventories(searchKey, pageNo, pageSize);
  totalPages = Math.ceil(res.Total / pageSize);

  addDateToPage(res.Inventories);
  updatePrevBtnState();
  updateNextBtnState();
}

function addDateToPage(data) {
  const bodyEl = document.getElementById("tBody");

  // clear data from table
  bodyEl.innerHTML = "";

  // add rows for each item in the table
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

function handleSearch(e) {
  e.preventDefault();

  if (searchTimer) {
    clearTimeout(searchTimer);
    searchTimer = null;
  }

  const value = e.target.value;

  // use search key only if there are minimum 3 characters in search input
  searchKey = value.length >= 3 ? value : null;
  pageNo = 1;

  searchTimer = setTimeout(async () => {
    await getInventories();
  }, 500);
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
