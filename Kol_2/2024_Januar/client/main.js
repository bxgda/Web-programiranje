import { Options } from "./components/options.js";
import { StatusAuta } from "./components/statusAuta.js";
import { vratiAutomobile } from "./utils.js";

let automobili = await vratiAutomobile();

let leviDiv = document.createElement("div");
leviDiv.classList.add("leviDiv");

let desniDiv = document.createElement("div");
desniDiv.classList.add("desniDiv");

let opcije = new Options(automobili);
opcije.draw(leviDiv);

automobili.forEach((auto) => {
  let statusAut = new StatusAuta(auto);
  statusAut.draw(desniDiv);
});

document.body.append(leviDiv, desniDiv);
