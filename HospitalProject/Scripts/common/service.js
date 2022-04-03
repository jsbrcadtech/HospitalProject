// method to make get request to provided url
async function getRequest(url) {
  try {
    const res = await fetch(url);
    if (res.ok) {
      return await res.json();
    }
  } catch (e) {
    console.error(e);
  }
}

// method to make post request to provided URL and data
async function postRequest(url, data) {
  try {
    const res = await fetch(url, {
      method: "POST",
      body: JSON.stringify(data),
      headers: {
        "Content-Type": "application/json",
      },
    });

    return res.ok;
  } catch (e) {
    console.error(e);
  }
}

// methoda to make delete request to provided url
async function deleteRequest(url) {
  try {
    const res = await fetch(url, {
      method: "DELETE",
    });

    return res.ok;
  } catch (e) {
    console.error(e);
  }
}
