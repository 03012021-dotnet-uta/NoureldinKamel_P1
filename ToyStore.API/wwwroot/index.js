"use-strict";
console.log("js working");

// fetch("api/WeatherForecast")
fetch("api/toy")
    .then((response) => response.json())
    .then((textjson) => {
        console.log(textjson);
        DisplayStackList(textjson);
        DisplayOfferList();
    });

fetch("api/toy/tags")
    .then((response) => response.json())
    .then((textjson) => {
        console.log(textjson);
    });
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

var offerList = Array();

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
        ({
            cardDiv,
            imgDiv,
            tagDiv,
            infoDiv,
            img
        } = createMainDivs());

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
        if (stack.Item.Products != undefined) {
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
        console.log("alo?");
        console.log(stack);
        ({
            cardDiv,
            imgDiv,
            tagDiv,
            infoDiv,
            img
        } = createMainDivs());

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
        if (stack.Item.Products != undefined) {
            displayOfferProductsInDiv(stack, infoDiv);
        }
        displayAfterOfferSave(infoDiv, stack, span, tagDiv);
    }
}

function displayAfterOfferSave(infoDiv, stack, span, tagDiv) {
    infoDiv.appendChild(
        createParagraphwClass("white-text", "Left in stock: " + stack.Count)
    );

    infoDiv.appendChild(createParagraphwClass("white-text", "Tags:"));
    stack.Item.TagList.forEach((tag) => {
        span = createSpanwClass("tag", tag.TagName);
        tagDiv.appendChild(span);
    });
    // return span;
}

function displayBeforeOfferSave(
    toyCarouselDiv,
    cardDiv,
    imgDiv,
    infoDiv,
    item,
    tagDiv,
    img
) {
    toyCarouselDiv.appendChild(cardDiv);
    cardDiv.appendChild(imgDiv);
    cardDiv.appendChild(infoDiv);

    infoDiv.appendChild(
        createParagraphwClass("white-text", "Name: " + item.Item.SellableName)
    );
    infoDiv.appendChild(
        createParagraphwClass("white-text", "Price: $" + item.Item.SellablePrice)
    );

    cardDiv.appendChild(tagDiv);

    img.src = item.Item.SellableImagePath;
    imgDiv.appendChild(img);
}

function createMainDivs() {
    let cardDiv = createDivwClass("toy-card round-border");
    let imgDiv = createDivwClass("card-img-container");
    let tagDiv = createDivwClass("card-info-div");
    let infoDiv = createDivwClass("card-info-div");
    let img = document.createElement("img");
    return {
        cardDiv,
        imgDiv,
        tagDiv,
        infoDiv,
        img
    };
}

function displayOfferProductsInDiv(stack, infoDiv) {
    let saveup = 0;
    let price = 0;
    stack.Item.Products.forEach((sellable) => {
        price += sellable.SellablePrice;
    });
    saveup = price - stack.Item.SellablePrice;

    let word = createSpanwClass("white-text", "Save: ");
    let delEl = createSpanwClass("deleted white-text", "$" + saveup);

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

function showOffers() {}