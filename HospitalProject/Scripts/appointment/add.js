window.onload = handleLoad;

async function handleLoad() {
  const specializationEl = document.getElementById("specializationId");
  specializationEl.onchange = handleSpecSelection;

  const form = document.forms.addForm;
  form.onsubmit = handleSubmit;

  const specs = await getSpecializations();
  addSpecializationsToPage(specs);
}

function addSpecializationsToPage(specs) {
  const el = document.getElementById("specializationId");
  el.innerHTML =
    "<option selected disabled>-- select specialization --</option>";

  specs.forEach((sp) => {
    const option = `<option value=${sp.Id}>${sp.Name}</option>`;
    el.innerHTML += option;
  });
}

function addStaffToPage(staff) {
  const el = document.getElementById("staffId");
  el.innerHTML = "<option selected disabled>-- select staff --</option>";

  staff.forEach((st) => {
    const option = `<option value=${st.Id}>${st.Name}</option>`;
    el.innerHTML += option;
  });
}

async function handleSpecSelection(e) {
  e.preventDefault();
  const errEl = document.getElementById("staffErr");
  errEl.innerHTML = "";
  errEl.classList.add("hidden");

  let staff = await getAllStaff();
  staff = staff.filter((st) => st.SpecializationId == e.target.value);

  const staffEl = document.getElementById("staffId");
  addStaffToPage(staff);

  if (staff.length == 0) {
    errEl.classList.remove("hidden");
    errEl.innerHTML = "No staff found for selected specialization.";
    staffEl.disabled = true;
  } else {
    staffEl.disabled = false;
  }
}

async function handleSubmit(e) {
  e.preventDefault();
  const form = e.target;
  const data = {
    staffId: form.staffId.value,
    date: form.date.value,
    time: form.time.value,
  };

  await addAppointment(data);
}
