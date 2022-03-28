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
