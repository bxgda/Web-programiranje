import { Opcije } from "./components/options.js";
import { ProdavnicaComponent } from "./components/prodavnica_component.js";
import { Prodavnica } from "./models/prodavnica.js";

export class Application {
  constructor(prodavnica) {
    this.prodavnica = prodavnica;
  }

  draw(container) {
    let appDiv = document.createElement("div");
    appDiv.classList.add("appDiv");

    let prodavnicaDiv = document.createElement("div");
    prodavnicaDiv.classList.add("prodavnicaDiv");

    let opcijeDiv = document.createElement("div");
    opcijeDiv.classList.add("opcijeDiv");

    appDiv.append(opcijeDiv, prodavnicaDiv);
    container.appendChild(appDiv);

    let opcija = new Opcije(this.prodavnica.naziv, prodavnicaDiv);
    opcija.draw(opcijeDiv);

    let prodavnica = new Prodavnica(
      this.prodavnica.naziv,
      this.prodavnica.lokacija,
      this.prodavnica.telefon,
      this.prodavnica.listaProizvoda
    );
    let prodavnicaComp = new ProdavnicaComponent(prodavnica);
    prodavnicaComp.draw(prodavnicaDiv);
  }
}
