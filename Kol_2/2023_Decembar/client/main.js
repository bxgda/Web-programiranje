import { vratiProdavnice } from "./utils.js";
import { Application } from "./application.js";

let prodavnice = await vratiProdavnice();

prodavnice.forEach((prod) => {
  let app = new Application(prod);
  app.draw(document.body);
});
