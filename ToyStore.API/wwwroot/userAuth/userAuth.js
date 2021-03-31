// const urlParams = new URLSearchParams(window.location.search);
// let receivedUrl = urlParams.get("from");
const receivedUrl = window.localStorage.getItem("from");
// receivedUrl = receivedUrl.slice(1, -1);
console.log('from: ')
console.log(receivedUrl);

const loginTabBtn = document.querySelector("#login-selector");
const regTabBtn = document.querySelector("#reg-selector");

const regForm = document.querySelector("#registration-form");
const loginForm = document.querySelector("#login-form");

const pass = document.querySelector("#password");
const confpass = document.querySelector("#password2");

function validatePassword() {
  if (pass.value.length < 8) {
    pass.setCustomValidity("Passwords size must be greater than 8");
  } else {
    pass.setCustomValidity("");
  }

  if (pass.value != confpass.value) {
    confpass.setCustomValidity("Passwords Don't Match");
  } else {
    confpass.setCustomValidity('');
  }

}

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

  console.log("conf: " + regForm.confpass.value);
  console.log("pass: " + regForm.password.value);

  if (regForm.confpass.value != regForm.password.value) {
    regForm.confpass.setCustomValidity("Passwords Don't Match");
    // pass.addEventListener("change", validatePassword);
    // confpass.addEventListener("keyup", validatePassword);
    pass.onchange = validatePassword;
    confpass.onkeyup = validatePassword;
    return;
  } else {
    regForm.confpass.setCustomValidity("");
  }

  if (regForm.password.value.length < 8) {
    regForm.password.setCustomValidity("Passwords size must be greater than 8");
    pass.addEventListener("change", validatePassword);
    confpass.addEventListener("keyup", validatePassword);
    return;
  } else {
    regForm.password.setCustomValidity("");
  }

  const obj = {
    Username: regForm.username.value,
    Password: regForm.password.value,
    FirstName: regForm.fistname.value,
    LastName: regForm.lastname.value,
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
    .then((response) => {
      if (response.status == 200) {
        return response.json();
      } else
        throw Error();
    })
    .then((text) => {
      return JSON.parse(text);
    })
    .then((obj) => {
      console.log(obj);
      redirect(receivedUrl, obj.CustomerToken.TokenValue);
    })
    .catch(() => {
      showErrorDiv("Username already exists", "#reg-error-div");
    });
});

loginForm.addEventListener("submit", (event) => {
  event.preventDefault();
  const obj = {
    Username: loginForm.username.value,
    Password: loginForm.password.value,
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
    .then((response) => {
      if (response.status == 200) {
        return response.json();
      } else
        throw Error();
    })
    .then((text) => {
      console.log(text);
      return JSON.parse(text);
    })
    .then((obj) => {
      console.log(obj);
      redirect(receivedUrl, obj.CustomerToken.TokenValue);
    })
    .catch(() => {
      showErrorDiv("Incorrect username or password", "#login-error-div");
    });
});

function showErrorDiv(message, id) {
  const errDiv = document.querySelector(id);
  errDiv.innerHTML = message;
}

function redirect(url, token) {
  // window.location.pathname.href = `https://localhost:5001/${url}?token=${token}`
  // document.cookie = `token = ${token}; SameSite = 'Lax';`;
  window.localStorage.setItem("token", token);
  let args = window.localStorage.getItem("args");
  if (args != undefined) {
    let toyIdPair = JSON.parse(args);
    // console.log("whole args");
    // console.log(toyIdPair);
    toyIdPair = toyIdPair['args'];
    let toyKey = Object.keys(toyIdPair)[0];
    // console.log("keys:");
    // console.log(Object.keys(toyIdPair));
    if (toyKey != undefined) {
      let toyId = toyIdPair[toyKey];
      window.location.href = `https://localhost:5001${url}?${toyKey}=${toyId}`;
    } else {
      window.location.href = `https://localhost:5001${url}`;
    }
  } else {
    window.location.href = `https://localhost:5001${url}`;
  }
}

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