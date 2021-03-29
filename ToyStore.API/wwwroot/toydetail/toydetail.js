"use-strict";
// let url = URL("api/toy");

// // var params = {lat:35.696233, long:139.570431} // or:
// // var params = [['lat', '35.696233'], ['long', '139.570431']]
// let params = ["id", "D6CDD398-D2FC-4FA3-9FB9-87D840C58198"];

// console.log(window.location.search);
const urlParams = new URLSearchParams(window.location.search);
// console.log(urlParams.get("id"));
let parsedId = urlParams.get("id");
// parsedId = parsedId.replace("/[']+/g", "");
parsedId = parsedId.slice(1, -1);
// console.log("parsedid: " + parsedId);

// url.search = new URLSearchParams(params).toString();
const data = {
  id: parsedId,
  // id: "9a54dec8-afc0-427b-8516-fc8dbcb326ef",
};

fetch("../api/toy/detail", {
  method: "POST",
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
  },
  body: JSON.stringify(data),
})
  .then((response) => response.json())
  .then((textjson) => {
    return JSON.parse(textjson);
  })
  .then((itemObj) => {
    console.log(itemObj);
    buildPage(itemObj);
  });

fetch("../api/toy/customers", {
  method: "POST",
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
  },
  body: JSON.stringify(data),
})
  .then((response) => response.json())
  .then((textjson) => {
    return JSON.parse(textjson);
  })
  .then((customerList) => {
    console.log(customerList);
  });

// todo: get the customers that bought it

function buildPage(itemStack) {
  createMainProductImg(itemStack);

  let infoSection = document.querySelector("#info-section");

  buildInfoBeforeSave(itemStack, infoSection);

  if (itemStack.Item.Products != null && itemStack.Item.Products.length > 0) {
    // console.log("productsok");
    displayOfferProductsInDiv(itemStack, infoSection);
  }

  buildInfoAfterSave(itemStack, infoSection);

  let customerSection = document.querySelector("#customer-section");
  if (itemStack.Item.Products != null && itemStack.Item.Products.length > 0) {
    buildProductSection(itemStack, customerSection);
  }
}

function buildProductSection(itemStack, customerSection) {
  let productsSection = document.createElement("section");
  productsSection.classList.add("section");

  let productsHolderDiv = createDivwClass("products-holder");
  const h2 = document.createElement("h2");
  h2.innerHTML = "Toys included";
  h2.classList = "product-title";
  productsHolderDiv.appendChild(h2);
  let carouselDiv = createDivwClass("product-carousel");
  let sliderDiv = createDivwClass("horizontal-slider");
  productsHolderDiv.appendChild(carouselDiv);
  carouselDiv.appendChild(sliderDiv);

  itemStack.Item.Products.forEach((sellable) => {
    const sellableDiv = createDivwClass("product-img-holder");

    sellableDiv.addEventListener("click", function () {
      console.log(sellable);
      sendToDetail(sellable);
    });

    const imgBgDiv = createDivwClass("product-img bg-img");
    imgBgDiv.style = `background-image: url("${sellable.SellableImagePath}")`;
    sellableDiv.appendChild(imgBgDiv);

    const prodInfoDiv = createDivwClass("product-info");
    sellableDiv.appendChild(prodInfoDiv);

    buildInfoBeforeSave({ Item: sellable }, prodInfoDiv);
    buildInfoAfterSave({ Item: sellable }, prodInfoDiv);
    sliderDiv.appendChild(sellableDiv);
    productsSection.appendChild(productsHolderDiv);
  });

  document.body.insertBefore(productsSection, customerSection);
}

function sendToDetail(stack) {
  window.location.href = `../toydetail/toydetail.html?id='${stack.SellableId}'`;
  console.log("clicked: " + stack.SellableName);
}

function buildInfoAfterSave(itemStack, infoSection) {
  let tagTitleSn = createSpanwClass("info-title", "Tags:");
  let tagDiv = createDivwClass("tags-holder");
  itemStack.Item.Tags.forEach((tag) => {
    span = createSpanwClass("tag blue-shadow", tag.TagName);
    tagDiv.appendChild(span);
  });

  infoSection.appendChild(tagTitleSn);
  infoSection.appendChild(document.createElement("br"));
  infoSection.appendChild(tagDiv);
}

function buildInfoBeforeSave(itemStack, infoSection) {
  let nameTitleSn = createSpanwClass("info-title", "Name: ");
  let nameDescSn = createSpanwClass("info-desc", itemStack.Item.SellableName);
  let descTitleSn = createSpanwClass("info-title", "Description: ");
  let descDescSn = createSpanwClass(
    "info-desc",
    itemStack.Item.SellableDescription
  );
  let priceTitleSn = createSpanwClass("info-title", "Price: ");
  let priceDescSn = createSpanwClass(
    "info-desc",
    "$" + itemStack.Item.SellablePrice
  );

  infoSection.appendChild(nameTitleSn);
  infoSection.appendChild(nameDescSn);
  infoSection.appendChild(document.createElement("br"));
  infoSection.appendChild(document.createElement("br"));
  infoSection.appendChild(descTitleSn);
  infoSection.appendChild(descDescSn);
  infoSection.appendChild(document.createElement("br"));
  infoSection.appendChild(document.createElement("br"));
  infoSection.appendChild(priceTitleSn);
  infoSection.appendChild(priceDescSn);
  infoSection.appendChild(document.createElement("br"));
  infoSection.appendChild(document.createElement("br"));
}

function displayOfferProductsInDiv(stack, infoDiv) {
  let saved = 0;
  let price = 0;
  stack.Item.Products.forEach((sellable) => {
    price += sellable.SellablePrice;
  });
  saved = price - stack.Item.SellablePrice;

  let word = createSpanwClass("info-title", "Save: ");
  let savedSp = createSpanwClass("info-desc", "$" + saved);

  let delEl = document.createElement("del");
  delEl.appendChild(savedSp);
  infoDiv.appendChild(word);
  infoDiv.appendChild(delEl);
  infoDiv.appendChild(document.createElement("br"));
  infoDiv.appendChild(document.createElement("br"));
}

function createMainProductImg(itemStack) {
  let imgSection = document.querySelector("#img-section");

  let imgHolderDiv = createDivwClass("main-img-holder");
  let imgBgDiv = createDivwClass("img-container bg-img");
  imgBgDiv.style = `background-image: url("${itemStack.Item.SellableImagePath}")`;
  imgBgDiv.classList = "img-container bg-img";

  imgSection.appendChild(imgHolderDiv);
  imgHolderDiv.appendChild(imgBgDiv);
}

function createDivwClass(classList) {
  let divObj = document.createElement("div");

  divObj.classList = classList;
  return divObj;
}

function createParagraphwClass(classname, text) {
  let p = document.createElement("p");
  p.classList = classname;
  p.innerHTML = text;
  return p;
}

function createSpanwClass(classname, text) {
  let span = document.createElement("span");
  span.classList = classname;
  span.innerHTML = text;
  span.setAttribute("style", "padding-right:5pt");
  return span;
}
