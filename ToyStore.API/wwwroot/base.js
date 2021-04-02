const urlParams = new URLSearchParams(window.location.search);
let parsedId = urlParams.get("customer");
console.log("received in url key: ");
console.log(parsedId);

let storageToken = localStorage.getItem("token");
console.log("token from storage: " + storageToken);

let CUSTOMER_OBJ = undefined;

if (storageToken != null) {
  fetchCustomerData();
} else {
  buildNavBar();
}

function fetchCustomerData() {
  fetch("https://localhost:5001/user/auth/validateToken", {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      TokenValue: storageToken,
    }),
  })
    .then((response) => {
      if (response.status == 200) {
        return response.json();
      } else throw Error();
    })
    .then((textjson) => JSON.parse(textjson))
    .then((obj) => {
      // hide register and login buttons
      // show user name
      CUSTOMER_OBJ = obj;
      var test = _.cloneDeep(obj);
      console.log("testing...");
      test.FirstName = "adsfasdfas  d";
      console.log(test);
      buildNavBar();
      changeGetStartedToUser(obj);
      buildFloatingCart(obj);
      // window.localStorage.setItem("user", JSON.stringify(obj));

      console.log("from base: ");
      console.log(obj);
    })
    .catch(() => {
      buildNavBar();
      console.log("incorrect token");
    });
}

function tryAddToCart(url, stackId, sellableId, stack, count = 1) {
  console.log("purchasing...");
  // let user = JSON.parse(window.localStorage.getItem("user"));
  if (CUSTOMER_OBJ != undefined) {
    console.log(CUSTOMER_OBJ);
    window.localStorage.setItem("toyId", parsedToyId);
    fetchAddNewStack(stack, count);
    // window.location.href = "../"
  } else {
    window.localStorage.setItem("from", window.location.pathname);
    let args = {};
    args.stackId = stackId;
    // if (url.includes("anydetail")) {
    //   args.sellableid = sellableId
    // } else {
    //   args.stackId = stackId
    // }
    window.localStorage.setItem(
      "args",
      JSON.stringify({
        args,
      })
    );
    window.location.href = "https://localhost:5001/userAuth/userAuth.html";
  }
}

function fetchAddNewStack(stack, count = 1) {
  userOrder = CUSTOMER_OBJ.CurrentOrder;

  newOrder = _.cloneDeep(userOrder);
  console.log("newOrder...");
  console.log(newOrder);
  newOrder.cart.push(stack);
  console.log("newOrder...");
  console.log(newOrder);
  newStack = _.cloneDeep(stack);
  newStack.Count = count;
  // newOrder.Cart.add

  let data = {
    addedStacks: [newStack.SellableStackId],
    token: CUSTOMER_OBJ.CustomerToken,
  };
  // return;
  fetch("https://localhost:5001/user/Cart/order", {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  })
    .then((response) => {
      if (response.ok) response.json();
      else throw new Error();
    })
    .then((text) => {
      console.log(text);
      window.location.href =
        "https://localhost:5001/cartDetail/cartDetail.html";
    })
    .catch(() => {
      console.log("error adding to cart");
    });
}

// CART_OBJ = undefined;

function buildFloatingCart(user) {
  //   <div class="cart-container">
  //   <div class="grid-cart">
  //     <div class="cart">Cart</div>
  //     <div class="cart-items">Items</div>
  //     <div class="item-count">5</div>
  //     <div class="cart-total">Total</div>
  //     <div class="total-price">$33.2</div>
  //   </div>
  // </div>

  if (
    user.CurrentOrder != null &&
    user.CurrentOrder.cart != null &&
    user.CurrentOrder.cart.length > 0
  ) {
    if (
      window.location.href ==
      "https://localhost:5001/cartDetail/cartDetail.html"
    ) {
      setCustomer(CUSTOMER_OBJ);
      return;
    }
    const containerDiv = _createDiv("cart-container");
    const gridDiv = _createDiv("grid-cart");
    const totalDiv = _createDiv("cart-total", "", "Total");
    const countDiv = _createDiv("cart-items", "", "Items");

    document.body.appendChild(containerDiv);
    containerDiv.appendChild(gridDiv);
    gridDiv.appendChild(_createDiv("cart", "", "Cart"));

    let stackCount = 0;
    let cartTotal = 0;

    Array.from(user.CurrentOrder.cart).forEach((stack) => {
      stackCount += stack.Count;
      cartTotal += stack.Item.SellablePrice * stack.Count;
    });

    const total = _createDiv("total-price", "", "$" + cartTotal);
    const count = _createDiv("item-count", "", stackCount);
    gridDiv.appendChild(countDiv);
    gridDiv.appendChild(count);
    gridDiv.appendChild(totalDiv);
    gridDiv.appendChild(total);

    containerDiv.addEventListener("click", (event) => {
      window.location.href =
        "https://localhost:5001/cartDetail/cartDetail.html";
    });
  }
}

function __showErrorPopup(message = "Oops, Something went wrong") {
  const div = _createDiv("error-popup", "", message);
  document.body.appendChild(div);
  setTimeout(() => {
    document.body.removeChild(div);
  }, 5000);
}

function changeGetStartedToUser(user) {
  const getStarted = document.querySelector("#login-nav");
  getStarted.innerHTML = `${user.FirstName} ${user.LastName}`;
  getStarted.addEventListener("click", () => {
    sendToUrl("https://localhost:5001/userProfile/userProfile.html");
  });
}

function setupNavLinks() {
  const loginNav = document.querySelector("#login-nav");
  const regNav = document.querySelector("#register-nav");
  const tagNav = document.querySelector("#tags-nav");
  const homeNav = document.querySelector("#home-nav");
  loginNav.addEventListener("click", function () {
    sendToUrl("https://localhost:5001/userAuth/userAuth.html", {
      from: window.location.pathname,
    });
  });
}

function setupAuthedLinks() {
  const tagNav = document.querySelector("#tags-nav");
  const homeNav = document.querySelector("#home-nav");
  sendToUrl("https://localhost:5001/userAuth/userAuth.html");
}

function sendToUrl(url) {
  // const qs = Object.keys(data)
  //   .map((key) => `${key}=${data[key]}`)
  //   .join("&");
  // console.log(url + "?" + qs);
  // window.location.href = url + "?" + qs;
  window.localStorage.setItem("from", window.location.pathname);
  window.location.href = url;
}

function buildNavBar() {
  const navBar = createElement("nav", "nav-bar");
  const navList = createElement("ul", "nav-list");
  const home = createElement("li", "nav-title", "home-nav", "Toy Store");
  const tags = createElement("li", "nav-left", "tags-nav", "Our Categories");
  const register = createElement("li", "nav-right", "login-nav", "Get started");
  document.body.insertBefore(navBar, document.body.firstChild);
  navBar.appendChild(navList);
  navList.appendChild(home);
  navList.appendChild(tags);
  navList.appendChild(register);
  home.addEventListener("click", () => {
    sendToUrl("https://localhost:5001/index.html");
  });
  register.addEventListener("click", () => {
    sendToUrl("https://localhost:5001/userAuth/userAuth.html");
  });
}

function createElement(tagName, classList, id, innerHTML = "") {
  const element = document.createElement(tagName);
  element.classList = classList;
  element.id = id;
  element.innerHTML = innerHTML;
  return element;
}

function _createDiv(classList = "", id = "", data = "") {
  const div = document.createElement("div");
  div.classList = classList;
  div.id = id;
  div.innerHTML = data;
  return div;
}
