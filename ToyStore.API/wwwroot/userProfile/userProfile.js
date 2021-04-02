const token = window.localStorage.getItem("token");

if (token != null) {
  fetch("https://localhost:5001/user/auth/validateToken", {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      TokenValue: token,
    }),
  })
    .then((response) => {
      if (response.status == 200) {
        return response.json();
      } else throw Error();
    })
    .then((textjson) => JSON.parse(textjson))
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
  window.location.href = "../userAuth/userAuth.html";
}

function buildUserOrders(customer) {
  const userOrdersDiv = document.querySelector(".orders-container");
  console.log("building orders...");
  const scrollDiv = createDivwClass("div-scroll");

  if (customer.FinishedOrders != null && customer.FinishedOrders.length > 0) {
    Array.from(customer.FinishedOrders).forEach((order) => {
      const OrderDiv = createDivwClass("order-div");
      const orderDate = createSpanwClass("item-name", `Date: `);
      const orderDate2 = createSpanwClass(
        "item-name",
        `${order.OrderDate.split("T")[0]}`
      );
      scrollDiv.appendChild(orderDate);
      scrollDiv.appendChild(orderDate2);
      userOrdersDiv.appendChild(scrollDiv);
      scrollDiv.appendChild(OrderDiv);
      const orderNameTitleDiv = createSpanwClass("item-name", `Name`);
      const orderPriceTitleDiv = createSpanwClass("item-name", `Price`);
      const orderCountTitleDiv = createSpanwClass("item-name", `Count`);
      const orderTotalTitleDiv = createSpanwClass("item-name", `Total`);
      OrderDiv.appendChild(orderNameTitleDiv);
      OrderDiv.appendChild(orderPriceTitleDiv);
      OrderDiv.appendChild(orderCountTitleDiv);
      OrderDiv.appendChild(orderTotalTitleDiv);
      let total = 0;
      Array.from(order.cart).forEach((stack) => {
        const orderNameDiv = createSpanwClass(
          "item-name",
          `${stack.Item.SellableName}`
        );
        const orderPriceDiv = createSpanwClass(
          "item-name",
          `$${stack.Item.SellablePrice}`
        );
        const orderCountDiv = createSpanwClass("item-name", `${stack.Count}`);
        total += stack.Count * stack.Item.SellablePrice;
        const orderTotalDiv = createSpanwClass(
          "item-name",
          `$${stack.Count * stack.Item.SellablePrice}`
        );
        OrderDiv.appendChild(orderNameDiv);
        OrderDiv.appendChild(orderPriceDiv);
        OrderDiv.appendChild(orderCountDiv);
        OrderDiv.appendChild(orderTotalDiv);
      });
      OrderDiv.appendChild(createSpanwClass("", ""));
      OrderDiv.appendChild(createSpanwClass("", ""));
      OrderDiv.appendChild(createSpanwClass("item-name", "Total:"));
      OrderDiv.appendChild(createSpanwClass("item-name", `$${total}`));
    });
  } else {
    console.log("no past orders found");
    userOrdersDiv.parentElement.removeChild(userOrdersDiv);
  }
}

function buildUserData(customer) {
  const userInfoForm = document.querySelector(".user-form");
  const fnameSpan = createLabelwClass("info-title", "First Name");
  const fnameInput = createInput("info-title", "fname", customer.FirstName);
  const lnameSpan = createLabelwClass("info-title", "Last Name");
  const lnameInput = createInput("info-title", "lname", customer.LastName);
  const usernameSpan = createLabelwClass("info-title", "Username");
  const usernameInput = createInput(
    "info-title",
    "uesrname",
    customer.CustomerUName
  );
  userInfoForm.appendChild(usernameSpan);
  userInfoForm.appendChild(usernameInput);
  // userInfoForm.appendChild(createBreak());
  userInfoForm.appendChild(fnameSpan);
  userInfoForm.appendChild(fnameInput);
  // userInfoForm.appendChild(createBreak());
  userInfoForm.appendChild(lnameSpan);
  userInfoForm.appendChild(lnameInput);
  // userInfoForm.appendChild(createBreak());
  userInfoForm.appendChild(
    createInput("user-info-submit", "submit-btn", "Submit", "submit")
  );
}

const logbtn = document.querySelector(".logout");
logbtn.addEventListener("click", (event) => {
  event.preventDefault();
  fetch("../user/auth/logout", {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ TokenValue: token }),
  })
    .then((response) => {
      if (response.ok) window.location.href = "../index.html";
      else throw Error();
    })
    .catch(() => {
      // window.location.href = "../index.html";
    });
});

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

function createInput(classList, name, value = "", type = "text") {
  let input = document.createElement("input");
  input.value = value;
  input.name = name;
  input.type = type;
  input.classList = classList;
  return input;
}
