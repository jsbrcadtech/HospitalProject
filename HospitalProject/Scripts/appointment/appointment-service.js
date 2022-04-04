async function getSpecializations() {
  const url = "/api/specialization";
  return await getRequest(url);
}

async function getAllStaff() {
  const url = "/api/StaffData/ListStaffs";
  return await getRequest(url);
}
