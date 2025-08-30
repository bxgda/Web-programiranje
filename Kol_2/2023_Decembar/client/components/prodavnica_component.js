import { ProizvodComponent } from "./proizvod_component.js";

export class ProdavnicaComponent {
  constructor(prodavnica) {
    this.prodavnica = prodavnica;
  }

  draw(container) {
    const boje = ["green", "red", "blue", "lightgreen"];

    container.innerHTML = "";

    let naslovDiv = document.createElement("div");
    naslovDiv.classList.add("naslovProdDiv");

    const nazivProdavnice = document.createElement("label");
    nazivProdavnice.innerHTML = `Prodavnica: ${this.prodavnica.naziv}`;

    naslovDiv.appendChild(nazivProdavnice);

    let contentProdDiv = document.createElement("div");
    contentProdDiv.classList.add("contentProdDiv");

    this.prodavnica.listaProizvoda.forEach((proizvod, i) => {
      let proizvodComp = new ProizvodComponent(proizvod, this.prodavnica.naziv);
      proizvodComp.draw(contentProdDiv, boje[i]);
    });

    container.append(naslovDiv, contentProdDiv);
  }
}
