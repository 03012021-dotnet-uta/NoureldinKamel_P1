const token = window.localStorage.getItem("token");

if (token != null) {
  fetch("https://localhost:5001/user/auth/validateToken", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        TokenValue: token
      }),
    }).then((response) => {
      if (response.status == 200) {
        return response.json();
      } else
        throw Error();
    })
    .then(textjson => JSON.parse(textjson))
    .then((obj) => {
      console.log("from base: ");
      console.log(obj);
      // build user data stuff
      buildUserData(obj);
      buildUserOrders(obj);
    })
    .catch((e) => {
      console.log(e);
      // buildNavBar();
      console.log("incorrect token");
      // window.location.href = '../userAuth/userAuth.html';
      // window.localStorage.removeItem("token");
    });
} else {
  window.location.href = '../userAuth/userAuth.html';
}

function buildUserOrders(customer) {
  const userOrdersDiv = document.querySelector(".user-form");
  console.log("building orders...");
}

function buildUserData(customer) {
  const userInfoForm = document.querySelector(".user-form");
  const fnameSpan = createLabelwClass("info-title", "First Name");
  const fnameInput = createInput("info-title", "fname", customer.FirstName);
  const lnameSpan = createLabelwClass("info-title", "Last Name");
  const lnameInput = createInput("info-title", "lname", customer.LastName);
  const usernameSpan = createLabelwClass("info-title", "Username");
  const usernameInput = createInput("info-title", "uesrname", customer.CustomerUName);
  userInfoForm.appendChild(usernameSpan);
  userInfoForm.appendChild(usernameInput);
  // userInfoForm.appendChild(createBreak());
  userInfoForm.appendChild(fnameSpan);
  userInfoForm.appendChild(fnameInput);
  // userInfoForm.appendChild(createBreak());
  userInfoForm.appendChild(lnameSpan);
  userInfoForm.appendChild(lnameInput);
  // userInfoForm.appendChild(createBreak());
  userInfoForm.appendChild(createInput("user-info-submit", "submit-btn", "Submit", "submit"));
}

function createSpanwClass(classname, text) {
  let span = document.createElement("span");
  span.classList = classname;
  span.innerHTML = text;
  return span;
}

function createLabelwClass(classname, text) {
  let label = document.createElement("label");
  label.classList = classname;
  label.innerHTML = text;
  return label;
}

function createDivwClass(classList) {
  let divObj = document.createElement("div");

  divObj.classList = classList;
  return divObj;
}

function createBreak() {
  return document.createElement("br");
}

function createInput(classList, name, value = "", type = 'text') {
  let input = document.createElement("input");
  input.value = value;
  input.name = name;
  input.type = type;
  input.classList = classList;
  return input;
}