console.log("js working");

// fetch("api/WeatherForecast")
fetch("api/toy")
    .then(response => response.json())
    .then(textjson => {
        console.log(textjson);
    })
    .catch(error => {
        console.log(error);
    });