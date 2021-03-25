console.log("js working");

// fetch("api/WeatherForecast")
fetch("api/toy")
    .then(response => response.json())
    .then(textjson => {
        console.log(textjson);
        DisplayStackList(textjson);
    })
    .catch(error => {
        console.log(error);
    });

function DisplayStackList(stackList) {

    //     <div class="toy-card round-border">
    //     <div class="card-img-container">
    //       <img
    //         src="https://media.istockphoto.com/photos/brown-teddy-bear-isolated-in-front-of-a-white-background-picture-id909772478?k=6&m=909772478&s=612x612&w=0&h=X55jzpsKboa_jUjbEN8eqAn0gjt696ldbeJMEqmNrcU="
    //         alt=""
    //       />
    //     </div>
    //   </div>

}