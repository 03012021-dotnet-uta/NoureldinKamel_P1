const urlParams = new URLSearchParams(window.location.search);
let parsedId = urlParams.get("test");
// parsedId = parsedId.slice(1, -1);
console.log(parsedId);

const loginTabBtn = document.querySelector("#login-selector");
const regTabBtn = document.querySelector("#reg-selector");

const regForm = document.querySelector("#registration-form");
const loginForm = document.querySelector("#login-form");

hide(regForm, regTabBtn);

regTabBtn.addEventListener("click", () => {
  console.log("alo?");
  show(regForm, regTabBtn);
  hide(loginForm, loginTabBtn);
});

loginTabBtn.addEventListener("click", () => {
  show(loginForm, loginTabBtn);
  hide(regForm, regTabBtn);
});

regForm.addEventListener("submit", (event) => {
  event.preventDefault();
  const obj = {
    CustomerUName: regForm.username.value,
    CustomerPass: regForm.password.value,
  };
  console.log(obj);
  fetch("../user/auth/register", {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(obj),
  })
    .then((response) => response.json())
    .then((text) => {
      console.log("look here");
      console.log(text);
    });
});

loginForm.addEventListener("submit", (event) => {
  event.preventDefault();
  const obj = {
    CustomerUName: loginForm.username.value,
    CustomerPass: loginForm.password.value,
  };
  console.log(obj);
  fetch("../user/auth/login", {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(obj),
  })
    .then((response) => response.json())
    .then((textjson) => JSON.parse(textjson))
    .then((text) => {
      console.log("look here");
      console.log(text);
    });
});

function show(element, btn) {
  element.style.display = "block";
  btn.classList.add("btn-active");
  btn.classList.remove("btn-inactive");
}

function hide(element, btn) {
  element.style.display = "none";
  btn.classList.remove("btn-active");
  btn.classList.add("btn-inactive");
}
