// let url = URL("api/toy");

// // var params = {lat:35.696233, long:139.570431} // or:
// // var params = [['lat', '35.696233'], ['long', '139.570431']]
// let params = ["id", "D6CDD398-D2FC-4FA3-9FB9-87D840C58198"];

// url.search = new URLSearchParams(params).toString();
const data = {
  // id: "D6CDD398-D2FC-4FA3-9FB9-87D840C58198",
  id: "9a54dec8-afc0-427b-8516-fc8dbcb326ef",
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

// todo: get the customers that bought it