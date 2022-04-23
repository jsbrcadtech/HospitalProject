async function getSpecializations() {
  const url = "/api/specialization";
  return await getRequest(url);
}

async function getAllStaff() {
  const url = "/api/StaffData/ListStaffs";
  return await getRequest(url);
}

async function addAppointment(data) {
  const url = "/api/appointments";
  await postRequest(url, data);
}
