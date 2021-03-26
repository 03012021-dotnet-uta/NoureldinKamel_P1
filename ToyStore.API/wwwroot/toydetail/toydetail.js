// let url = URL("api/toy");

// // var params = {lat:35.696233, long:139.570431} // or:
// // var params = [['lat', '35.696233'], ['long', '139.570431']]
// let params = ["id", "D6CDD398-D2FC-4FA3-9FB9-87D840C58198"];

// url.search = new URLSearchParams(params).toString();
const data = {
  id: "D6CDD398-D2FC-4FA3-9FB9-87D840C58198",
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
  .then((textjson) => {
    console.log(textjson);
  });
