import { Opcije } from "./components/opcije.js";
import { SobaStatus } from "./components/sobaStatus.js";
import { vratiClanoveSobe, vratiKorisnike, vratiSobe } from "./utils.js";

const leviDiv = document.createElement("div");
leviDiv.classList.add("leviDiv");

const desniDiv = document.createElement("div");
desniDiv.classList.add("desniDiv");

const korisnici = await vratiKorisnike();

const opcije = new Opcije(korisnici);
opcije.draw(leviDiv);

const sobe = await vratiSobe();

sobe.forEach(async (soba) => {
  const clanoviSobe = await vratiClanoveSobe(soba.id);
  const sobaStatus = new SobaStatus(soba, clanoviSobe);
  sobaStatus.draw(desniDiv);
});

document.body.append(leviDiv, desniDiv);
