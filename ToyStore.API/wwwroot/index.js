'use-strict';
console.log("js working");

// fetch("api/WeatherForecast")
fetch("api/toy")
    .then(response => response.json())
    .then(textjson => {
        console.log(textjson);
        DisplayStackList(textjson);
    })
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

    //   <div class="toy-card round-border">
    //     <div class="card-img-container">
    //       <img
    //         src="https://media.istockphoto.com/photos/brown-teddy-bear-isolated-in-front-of-a-white-background-picture-id909772478?k=6&m=909772478&s=612x612&w=0&h=X55jzpsKboa_jUjbEN8eqAn0gjt696ldbeJMEqmNrcU="
    //         alt=""
    //       />
    //     </div>
    //   </div>

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

        cardDiv = createDivwClass("toy-card round-border");
        imgDiv = createDivwClass("card-img-container");
        tagDiv = createDivwClass("card-info-div");
        infoDiv = createDivwClass("card-info-div");
        img = document.createElement("img");

        toyCarouselDiv.appendChild(cardDiv);
        cardDiv.appendChild(imgDiv);
        cardDiv.appendChild(infoDiv);
        infoDiv.appendChild(createParagraphwClass("white-text", "Name: " + stack.Item.SellableName));
        infoDiv.appendChild(createParagraphwClass("white-text", "Price: $" + stack.Item.SellablePrice));

        if (stack.Item.Products != undefined) {
            let saveup = 0;
            let price = 0;
            stack.Item.Products.forEach(sellable => {
                price += sellable.SellablePrice;
            });
            saveup = price - stack.Item.SellablePrice;

            let word = createSpanwClass("white-text", "Save: ")
            let delEl = createSpanwClass("deleted white-text", "$" + saveup);

            infoDiv.appendChild(word);
            infoDiv.appendChild(delEl);
        }

        infoDiv.appendChild(createParagraphwClass("white-text", "Left in stock: " + stack.Count));


        infoDiv.appendChild(createParagraphwClass("white-text", "Tags:"));
        stack.Item.TagList.forEach(tag => {
            span = createSpanwClass("tag", tag.TagName);
            tagDiv.appendChild(span);
        });

        // for (let j = 0; j < stack.TagList.length; j++) {
        //     const element2 = TagList[j];
        //     span = createSpanwClass("", element2);
        //     tagDiv.appendChild(span);
        // }

        cardDiv.appendChild(tagDiv);

        img.src = stack.Item.SellableImagePath;
        imgDiv.appendChild(img);
    }
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