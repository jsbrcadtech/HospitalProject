const baseUrl = "/api/inventory";

async function getAllInventories(searchKey = null, pageNo = 1, pageSize = 10) {
  let url = `${baseUrl}?pageNo=${pageNo}&pageSize=${pageSize}`;

  if (searchKey !== null) {
    url += `&searchKey=${searchKey}`;
  }

  return await getRequest(url);
}

async function addItemToInventory(newItem) {
  return await postRequest(baseUrl, newItem);
}

async function getInventoryItemById(itemId) {
  const url = `${baseUrl}/${itemId}`;
  return await getRequest(url);
}

async function getLedgerForItem(itemId, start, end) {
  let url = `${baseUrl}/${itemId}/ledger`;
  if (start && end) {
    url += `?startDate=${start}&endDate=${end}`;
  }

  const ledgers = await getRequest(url);

  ledgers.forEach((l) => {
    const dateTime = new Date(l.CreationDate);
    const date = dateTime.toDateString();
    const time = dateTime.toTimeString().split(" ")[0];
    l.CreationDate = `${date} ${time}`;
  });

  return ledgers;
}

async function deleteItemFromInventory(itemId) {
  const url = `${baseUrl}/${itemId}`;
  return await deleteRequest(url);
}

async function updateLedger(itemId, data) {
  const url = `${baseUrl}/${itemId}/ledger`;
  return await postRequest(url, data);
}

async function deleteLedgerById(id) {
  const url = `${baseUrl}/ledger/${id}`;
  return await deleteRequest(url);
}
