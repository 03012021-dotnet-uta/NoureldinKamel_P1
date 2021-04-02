"use-strict";
console.log("js working");
var offerList = Array();

// fetch("api/WeatherForecast")
fetch("api/toy")
  .then((response) => response.json())
  .then((textjson) => {
    console.log("textjson");
    console.log(textjson);
    DisplayStackList(textjson);
    DisplayOfferList();
  });

fetch("api/toy/tags")
  .then((response) => response.json())
  .then((textjson) => {
    console.log(textjson);
  });

// let customer = getCustomer();
function GetRecommended(customer) {
  // console.log(customer);
  // if (customer != undefined) {
  //   fetch("https://localhost:5001/api/toy/recommended", {
  //     method: "POST",
  //     headers: {
  //       Accept: "application/json",
  //       "Content-Type": "application/json",
  //     },
  //     body: JSON.stringify(customer.CustomerId),
  //   })
  //     .then((response) => {
  //       if (response.status == 200) {
  //         return response.json();
  //       } else throw Error();
  //     })
  //     .then((textjson) => JSON.parse(textjson))
  //     .then((obj) => {
  //       console.log("recommendedstacks: ");
  //       console.log(obj);
  //     })
  //     .catch((error) => {});
  // }
}
// .catch(error => {
//     console.log(error);
// });

// fetch("api/toy",{

// })
//     .then(response => response.json())
//     .then(textjson => {
//         console.log(textjson);
//         DisplayStackList(textjson);
//     })

function DisplayStackList(stackList) {
  let cardDiv = null;
  let imgDiv = null;
  let infoDiv = null;
  let tagDiv = null;
  let img = null;
  let span = null;
  let stack = null;

  let toyCarouselDiv = document.getElementById("toys");

  for (let i = 0; i < stackList.length; i++) {
    stack = stackList[i];
    ({ cardDiv, imgDiv, tagDiv, infoDiv, img } = createMainDivs());

    displayBeforeOfferSave(
      toyCarouselDiv,
      cardDiv,
      imgDiv,
      infoDiv,
      stack,
      tagDiv,
      img
    );
    // console.log(stack.Item.Products);
    if (stack.Item.Products != undefined && stack.Item.Products.length > 0) {
      displayOfferProductsInDiv(stack, infoDiv);
      offerList.push(stack);
    }
    displayAfterOfferSave(infoDiv, stack, span, tagDiv);
  }
}

function DisplayOfferList() {
  let stackList = offerList;
  let cardDiv = null;
  let imgDiv = null;
  let infoDiv = null;
  let tagDiv = null;
  let img = null;
  let span = null;
  let stack = null;

  let toyCarouselDiv = document.getElementById("offers");

  for (let i = 0; i < stackList.length; i++) {
    stack = stackList[i];
    console.log(stack);
    ({ cardDiv, imgDiv, tagDiv, infoDiv, img } = createMainDivs());

    displayBeforeOfferSave(
      toyCarouselDiv,
      cardDiv,
      imgDiv,
      infoDiv,
      stack,
      tagDiv,
      img
    );
    // console.log(stack.Item.Products);
    if (stack.Item.Products != undefined && stack.Item.Products.length > 0) {
      // console.log("item: " + stack.Item.SellableName);
      // console.log("product count: " + stack.Item.Products.length);
      // console.log(stack.Item.Products);
      displayOfferProductsInDiv(stack, infoDiv);
    }
    displayAfterOfferSave(infoDiv, stack, span, tagDiv);
  }
}

function displayAfterOfferSave(infoDiv, stack, span, tagDiv) {
  infoDiv.appendChild(
    createParagraphwClass("blue-text", "Left in stock: " + stack.Count)
  );

  infoDiv.appendChild(createParagraphwClass("blue-text", "Tags:"));
  stack.Item.Tags.forEach((tag) => {
    span = createSpanwClass("tag blue-shadow", tag.TagName);
    tagDiv.appendChild(span);
  });
  // return span;
}

function displayBeforeOfferSave(
  toyCarouselDiv,
  cardDiv,
  imgDiv,
  infoDiv,
  stack,
  tagDiv,
  img
) {
  toyCarouselDiv.appendChild(cardDiv);
  cardDiv.appendChild(imgDiv);
  cardDiv.appendChild(infoDiv);

  infoDiv.appendChild(
    createParagraphwClass("blue-text", "Name: " + stack.Item.SellableName)
  );
  infoDiv.appendChild(
    createParagraphwClass("blue-text", "Price: $" + stack.Item.SellablePrice)
  );

  cardDiv.appendChild(tagDiv);

  cardDiv.addEventListener("click", function () {
    console.log(stack);
    sendToDetail(stack);
  });

  img.src = stack.Item.SellableImagePath;
  imgDiv.appendChild(img);
}

function sendToDetail(stack) {
  window.location.href = `toydetail/toydetail.html?stackId=${stack.SellableStackId}`;
  console.log("clicked: " + stack.Item.SellableName);
}

function createMainDivs() {
  let cardDiv = createDivwClass("toy-card black-shadow round-border");
  let imgDiv = createDivwClass("card-img-container");
  let tagDiv = createDivwClass("card-info-div");
  let infoDiv = createDivwClass("card-info-div");
  let img = document.createElement("img");
  return {
    cardDiv,
    imgDiv,
    tagDiv,
    infoDiv,
    img,
  };
}

function displayOfferProductsInDiv(stack, infoDiv) {
  let saveup = 0;
  let price = 0;
  stack.Item.Products.forEach((sellable) => {
    price += sellable.SellablePrice;
  });
  saveup = price - stack.Item.SellablePrice;

  let word = createSpanwClass("blue-text", "Save: ");
  let delEl = createSpanwClass("deleted blue-text", "$" + saveup);

  infoDiv.appendChild(word);
  infoDiv.appendChild(delEl);
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
