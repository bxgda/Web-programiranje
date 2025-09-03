import { Opcije } from "./components/options.js";
import { RezervoarInfo } from "./components/rezervoarInfo.js";
import { vratiRezervoare, vratiSveRibe } from "./utils.js";

const leviDiv = document.createElement("div");
leviDiv.classList.add("leviDiv");

const desniDiv = document.createElement("div");
desniDiv.classList.add("desniDiv");

const opcije = new Opcije();
opcije.draw(leviDiv);

const rezervoari = await vratiRezervoare();
const sveRibe = await vratiSveRibe();

rezervoari.forEach((rezervoar) => {
  const rezervoarInfo = new RezervoarInfo(rezervoar, sveRibe);
  rezervoarInfo.draw(desniDiv);
});

document.body.append(leviDiv, desniDiv);
