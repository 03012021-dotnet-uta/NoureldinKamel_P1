"use-strict";
// let url = URL("api/toy");

// // var params = {lat:35.696233, long:139.570431} // or:
// // var params = [['lat', '35.696233'], ['long', '139.570431']]
// let params = ["id", "D6CDD398-D2FC-4FA3-9FB9-87D840C58198"];

// console.log(window.location.search);
const urlParamsToys = new URLSearchParams(window.location.search);
// console.log(urlParams.get("id"));
let STACK_ID = "";
let SELLABLE_ID = "";
let parsedToyId = urlParamsToys.get("stackId");
let url = "";
if (parsedToyId == undefined) {
  parsedToyId = urlParamsToys.get("sellableid");
  SELLABLE_ID = parsedToyId;
  url = "../api/toy/anydetail";
} else {
  url = "../api/toy/detail";
  STACK_ID = parsedToyId;
}
// parsedId = parsedId.replace("/[']+/g", "");
console.log("parsedid: " + parsedToyId);

// url.search = new URLSearchParams(params).toString();
const data = {
  id: parsedToyId,
  // id: "9a54dec8-afc0-427b-8516-fc8dbcb326ef",
};

fetch(url, {
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
    STACK_ID = itemObj.Stack.SellableStackId;
    buildPage(itemObj);
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
        buildStackCustomers(customerList, itemObj);
      });
  });

// todo: get the customers that bought it

function buildStackCustomers(customerList, itemObj) {
  //   <div class="customer-info black-shadow">
  //   <span>User: </span>
  //   <span>asdfafds</span>
  //   <span>Amount: </span>
  //   <span>2</span>
  // </div>
  console.log("customerList...");
  console.log(customerList);
  if (customerList.length > 0) {
    const div = document.querySelector("#div-scroll");
    Array.from(customerList).forEach((customer) => {
      const custDiv = createDivwClass("customer-info black-shadow");
      const userSpan = createSpanwClass("", "User");
      const userName = createSpanwClass(
        "",
        `${customer.FirstName} ${customer.LastName}`
      );
      const amountSpan = createSpanwClass("", "Amount Bought");
      let total = 0;
      Array.from(customer.FinishedOrders).forEach((order) => {
        Array.from(order.cart).forEach((stack) => {
          if (itemObj.Stack.Item.SellableId == stack.Item.SellableId)
            total += parseInt(stack.Count);
          console.log("total");
          console.log(total);
        });
      });
      if (total == 0) {
      } else {
        const amountCount = createSpanwClass("", `${total}`);
        custDiv.appendChild(userSpan);
        custDiv.appendChild(userName);
        custDiv.appendChild(amountSpan);
        custDiv.appendChild(amountCount);
        div.appendChild(custDiv);
      }
    });
  }
}

function buildPage(something) {
  let itemStack = something.Stack;
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

  buildAddToCartButton(infoSection, itemStack);

  let alts = something.Alternatives;
  // <div class="div-list">
  // <h2>Same Item can be found in</h2>
  // <div class="div-scroll"></div>
  // </div>

  if (alts.length > 0) {
    let horizontalslider = createDivwClass("horizontal-slider");
    let productcarousel = createDivwClass("product-carousel");
    let h22 = document.createElement("h2");
    h22.innerText = "Same Item can be found in";
    let bigDiv = document.querySelector("#alternatives");
    bigDiv.appendChild(h22);
    productcarousel.appendChild(horizontalslider);
    bigDiv.appendChild(productcarousel);
    Array.from(alts).forEach((stack) => {
      console.log("first stack in alts: ");
      console.log(stack);
      let stackdiv = createDivwClass("product-img-holder");
      horizontalslider.appendChild(stackdiv);

      const imgBgDiv = createDivwClass("product-img bg-img");
      imgBgDiv.style = `background-image: url("${stack.Item.SellableImagePath}")`;
      stackdiv.appendChild(imgBgDiv);

      const prodInfoDiv = createDivwClass("product-info");
      stackdiv.appendChild(prodInfoDiv);

      buildInfoBeforeSave(stack, prodInfoDiv);
      if (stack.Item.Products != null && stack.Item.Products.length > 0) {
        displayOfferProductsInDiv(stack, prodInfoDiv);
      }
      // buildProductSection(stack, prodInfoDiv);
    });
  }
}

function buildAddToCartButton(infoSection, stack) {
  let purchasebutton = document.createElement("input");
  purchasebutton.value = "Purchase";
  purchasebutton.type = "submit";
  purchasebutton.id = "#add-to-cart-btn";
  infoSection.appendChild(document.createElement("br"));
  infoSection.appendChild(document.createElement("br"));
  infoSection.appendChild(purchasebutton);
  purchasebutton.addEventListener("click", () => {
    tryAddToCart(url, STACK_ID, SELLABLE_ID, stack, 1);
  });
}

function buildProductSection(itemStack, customerSection) {
  console.log(itemStack);
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

    buildInfoBeforeSave(
      {
        Item: sellable,
      },
      prodInfoDiv
    );
    buildInfoAfterSave(
      {
        Item: sellable,
      },
      prodInfoDiv
    );
    sliderDiv.appendChild(sellableDiv);
    productsSection.appendChild(productsHolderDiv);
  });

  document.body.insertBefore(productsSection, customerSection);
}

function sendToDetail(item) {
  window.location.href = `../toydetail/toydetail.html?sellableid=${item.SellableId}`;
  console.log("clicked: " + stack.SellableName);
}

function buildInfoAfterSave(itemStack, infoSection) {
  let tagTitleSn = createSpanwClass("info-title", "Tags:");
  let tagDiv = createDivwClass("tags-holder");
  itemStack.Item.Tags.forEach((tag) => {
    const span = createSpanwClass("tag blue-shadow", tag.TagName);
    tagDiv.appendChild(span);
  });

  infoSection.appendChild(tagTitleSn);
  infoSection.appendChild(document.createElement("br"));
  infoSection.appendChild(tagDiv);

  if (itemStack.location != undefined) {
    let fromInfo = createSpanwClass("info-title", "From: ");
    let fromDesc = createSpanwClass(
      "info-desc",
      itemStack.location.LocationName
    );
    infoSection.appendChild(document.createElement("br"));
    infoSection.appendChild(document.createElement("br"));
    infoSection.appendChild(fromInfo);
    infoSection.appendChild(fromDesc);
  }
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
  if (itemStack.Count != undefined) {
    let stockTitleSn = createSpanwClass("info-title", "Left in stock: ");
    let stockDescSn = createSpanwClass("info-desc", itemStack.Count);
    infoSection.appendChild(stockTitleSn);
    infoSection.appendChild(stockDescSn);
    infoSection.appendChild(document.createElement("br"));
    infoSection.appendChild(document.createElement("br"));
  }
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
