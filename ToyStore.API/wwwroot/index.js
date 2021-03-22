console.log("js working");

fetch("WeatherForecast")
    .then(response => response.json())
    .then(textjson => {
        console.log(textjson);
    })
    .catch(error => {
        console.log(error);
    });