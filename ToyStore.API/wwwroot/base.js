const urlParams = new URLSearchParams(window.location.search);
let parsedId = urlParams.get("customer");
console.log("received in url key: ");
console.log(parsedId);

let cache = {};

if (parsedId != null) {
  const token = parsedId.CustomerToken;
  fetch("https://localhost:5001/user/auth/validatetoken", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(token),
    }).then((response) => {
      if (response.status == 200) {
        return response.json();
      } else
        throw Error();
    })
    .then(textjson => JSON.parse(textjson))
    .then((obj) => {
      console.log(obj);
    })
    .catch(() => {});
  cache.CustomerToken = parsedId.CustomerToken;
}

let cookie = document.cookie;
console.log("token: " + cookie);


setupNavLinks();

function setupNavLinks() {
  const loginNav = document.querySelector("#login-nav");
  const regNav = document.querySelector("#register-nav");
  const tagNav = document.querySelector("#tags-nav");
  const homeNav = document.querySelector("#home-nav");
  loginNav.addEventListener("click", function () {
    sendToUrl("https://localhost:5001/userAuth/userAuth.html", {
      from: window.location.pathname
    });
  });
}

function sendToUrl(url, data) {
  const qs = Object.keys(data)
    .map((key) => `${key}=${data[key]}`)
    .join("&");
  console.log(url + "?" + qs);
  window.location.href = url + "?" + qs;
}

function buildNavBar() {
  const navBar = createElement("nav", "nav-bar");
  const navList = createElement("ul", "nav-list");

}

function createElement(tagName, classList, id, innerHTML) {
  const element = document.createElement(tagName);
  element.classList = classList;
  element.id = id;
  element.innerHTML = innerHTML;
  return element;
}