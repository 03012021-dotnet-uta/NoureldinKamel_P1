// console.log("customer: ");
// console.log(CUSTOMER_OBJ);

function setCustomer(customer) {
  console.log("customer: ");
  console.log(customer);

  // buildCartPage(customer);

  let myFunc = () => {
    buildCartPage(customer, locationStacks);
  };

  fetch(
    `https://localhost:5001/user/Cart/locationStacks/${customer.CurrentOrder.OrderLocation.LocationId}`,
    {
      method: "GET",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
    }
  )
    .then((response) => {
      if (response.ok) return response.json();
      else throw Error();
    })
    .then((json) => {
      buildCartPage(customer, JSON.parse(json));
    });

  // fetchCartChange(
  //   ,
  //   "GET",
  //   customer,
  //   "",
  //   myFunc
  // );
}

const cartDiv = document.querySelector(".fancy-container-shadow-border");
let loading = false;

function checknewCount(newCount, input) {
  console.log("new count:");
  console.log(newCount);
  console.log(input);
  if (newCount > 0) {
    return true;
  }
  return false;
}

function checkCanAddOne(input) {
  console.log(input);
  if (input.max > 0) {
    return true;
  }
  return false;
}

function buildCartPage(customer, locationStacks) {
  if (
    customer != undefined &&
    customer.CurrentOrder != undefined &&
    customer.CurrentOrder.cart != undefined &&
    customer.CurrentOrder.cart.length > 0
  ) {
    console.log("locationStacks: ");
    console.log(locationStacks);
    let total = 0;
    Array.from(customer.CurrentOrder.cart).forEach((stack) => {
      const itemDiv = _createDiv("exp-cart-grid");
      cartDiv.appendChild(itemDiv);
      // img
      const itemImg = _createDiv("img-div fancy-container-shadow-border");
      itemImg.style.backgroundImage = `url('${stack.Item.SellableImagePath}')`;
      // itemImg.style = `background-image: url('${stack.Item.SellableImagePath}')`
      itemDiv.appendChild(itemImg);
      // right side info
      const nameDiv = _createDiv(
        "stack-item",
        "",
        `Name: ${stack.Item.SellableName}`
      );
      itemDiv.appendChild(nameDiv);
      const priceDiv = _createDiv(
        "stack-price",
        "",
        `Price: $${stack.Item.SellablePrice}`
      );
      total += stack.Item.SellablePrice * stack.Count;
      itemDiv.appendChild(priceDiv);
      const locationDiv = _createDiv(
        "stack-location",
        "",
        `Location: ${stack.location.LocationName}`
      );
      itemDiv.appendChild(locationDiv);
      // plus and minus for amount
      const inputHolderDiv = _createDiv("input-grid");
      itemDiv.appendChild(inputHolderDiv);
      //plus
      const plusBtnHolder = _createDiv("btn-holder");
      const plusBtn = _createDiv("cart-btn plus");
      inputHolderDiv.appendChild(plusBtnHolder);
      plusBtnHolder.appendChild(plusBtn);
      //input
      const input = document.createElement("input");
      input.type = "number";
      input.disabled = true;
      input.value = stack.Count;
      // set min and max for count
      const min = 1;
      // console.log("stack.location.LocationInventory:");
      // console.log(stack.location.LocationInventory);
      // let list = Array.from(stack.location.LocationInventory).filter(
      let list = Array.from(locationStacks).filter((lstack) => {
        // console.log("lstack:");
        // console.log(lstack.Item.SellableId);
        // console.log("customer stack:");
        // console.log(stack.Item.SellableId);
        return lstack.Item.SellableId == stack.Item.SellableId;
      });

      // console.log("list..");
      // console.log(list);
      // console.log("stack.location.LocationInventory:");
      // console.log(stack.location.LocationInventory);
      const max = list[0].Count;
      input.min = min;
      input.max = max;
      plusBtnHolder.addEventListener("click", (event) => {
        if (!loading) {
          if (!checkCanAddOne(input)) {
            __showErrorPopup(`You can't add more than that`);
            return;
          }
          loading = true;
          plusBtnHolder.classList.add("loading");

          changeStackCount(
            plusBtnHolder,
            customer,
            stack,
            stack.Count + 1,
            input
          );
        }
      });
      inputHolderDiv.appendChild(input);
      //minus
      const minusBtnHolder = _createDiv("btn-holder");
      minusBtnHolder.addEventListener("click", (event) => {
        if (!loading) {
          if (!checknewCount(stack.Count - 1, input)) {
            __showErrorPopup("You cannot have an item with 0 count");
            return;
          }
          loading = true;
          minusBtnHolder.classList.add("loading");
          changeStackCount(
            minusBtnHolder,
            customer,
            stack,
            stack.Count - 1,
            input
          );
        }
      });
      const minusBtn = _createDiv("cart-btn minus");
      inputHolderDiv.appendChild(minusBtnHolder);
      minusBtnHolder.appendChild(minusBtn);
      // remove btn
      const removeBtn = _createDiv("stack-remove", "", "Remove");
      removeBtn.addEventListener("click", (event) => {
        if (!loading) {
          loading = true;
          removeBtn.classList.add("loading");
          removeStack(removeBtn, customer, stack, itemDiv);
        }
      });
      itemDiv.appendChild(removeBtn);
    });

    const totalDiv = _createDiv("black-text", "", `Total: $${total}`);
    cartDiv.appendChild(totalDiv);

    const checkoutBtn = _createDiv("checkout-container", "", "Checkout");
    checkoutBtn.addEventListener("click", () => {
      if (!loading) {
        loading = true;
        checkoutBtn.classList.add("loading");
        checkoutOrder(checkoutBtn, customer);
      }
    });

    cartDiv.appendChild(checkoutBtn);
  } else {
    cartDiv.appendChild(document.createElement("br"));
    cartDiv.appendChild(document.createElement("br"));
    const h1 = document.createElement("h1");
    h1.textContent = "Items you add to your cart will appear here";
    cartDiv.appendChild(h1);
    cartDiv.addEventListener("change", () => {
      console.log("someting changed");
    });
  }
}

function removeStack(btn, customer, stack, itemDiv) {
  console.log("removing...");
  console.log(customer);
  console.log(stack);

  let myFunc = (result) => {
    console.log("response in remove");
    console.log(result);
    if (result == undefined || result == false) {
      btn.classList.remove("loading");
      loading = false;
      return;
    } else {
      itemDiv.parentElement.removeChild(itemDiv);
      btn.classList.remove("loading");
      customer.CurrentOrder.cart.remove(stack);
      loading = false;
    }
  };

  let nnnorder = _.cloneDeep(customer.CurrentOrder);
  console.log("newOrder:");
  console.log(nnnorder);

  let sentStack = _.cloneDeep(stack);
  sentStack.location.LocationInventory = null;

  let orderModel = {
    // // UpdateStack: sentStack,
    // // UpdateCount: -1,
    // // newOrder: nnnorder,
    // removedStacks: [sentStack],
    // // addedStacks: [],
    // // countChangedStacks: [],
    // token: customer.CustomerToken,
    removedStacks: [stack.SellableStackId],
    token: customer.CustomerToken,
  };

  fetchCartChange(
    "https://localhost:5001/user/Cart/order",
    "DELETE",
    customer,
    orderModel,
    myFunc
  );
}

function changeStackCount(btn, customer, stack, count, input) {
  let newOrder = _.cloneDeep(customer.CurrentOrder);
  console.log("changing count...");
  console.log(customer);
  console.log(stack);
  console.log(count);

  let x = new Object();
  x[`${stack.SellableStackId}`] = count;

  let orderModel = {
    newUpdatedStackCount: x,
    token: customer.CustomerToken,
  };

  let myFunc = (result) => {
    console.log("response in remove");
    console.log(result);
    if (result == undefined || result == false) {
      btn.classList.remove("loading");
      loading = false;
      return;
    } else {
      input.value = count;
      let diff = new Number(0);
      diff = parseInt(parseInt(stack.Count) - parseInt(count));
      console.log(diff);
      input.max = parseInt(input.max) + parseInt(diff);
      stack.Count = parseInt(count);
      btn.classList.remove("loading");
      console.log(input);
      loading = false;
    }
  };

  fetchCartChange(
    "https://localhost:5001/user/Cart/order",
    "PATCH",
    customer,
    orderModel,
    myFunc
  );
}

function checkoutOrder(checkoutBtn, customer) {
  console.log("checking out...");
  console.log(customer);
  console.log(checkoutBtn);

  myFunc = () => {
    window.location.href = "../userProfile/userProfile.html";

    checkoutBtn.classList.remove("loading");
    loading = false;
  };

  fetchCartChange(
    "https://localhost:5001/user/Cart/checkout",
    "POST",
    customer,
    customer.CustomerToken,
    myFunc
  );
}

function fetchCartChange(url, methood, customer, orderModel, myFunc) {
  console.log("url");
  console.log(url);
  console.log("orderModel");
  console.log(orderModel);
  let result = undefined;
  let x = JSON.stringify(orderModel) + "";
  console.log(x);
  // return;
  fetch(url, {
    method: methood,
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: x,
  })
    .then((respose) => {
      console.log(respose.json());
      if (!respose.ok) {
        throw Error();
      } else {
        return respose;
      }
    })
    .then((json) => {
      console.log(json);
      result = json;
      myFunc(result);
    })
    .catch(() => {
      console.log("caught error");
    });
}
