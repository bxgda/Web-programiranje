export class Fabrika {
  constructor(fabrikaID, naziv, listaSilosa) {
    this.fabrikaID = fabrikaID;
    this.naziv = naziv;
    this.listaSilosa = listaSilosa;
  }

  draw(container) {
    const naziv = document.createElement("label");
    naziv.innerHTML = this.naziv;
    naziv.classList.add("nazivFabrike");

    const nazivDiv = document.createElement("div");
    nazivDiv.append(naziv);

    const silosiDiv = document.createElement("div");
    silosiDiv.classList.add("sviSilosi");

    this.listaSilosa.forEach((silos) => {
      silos.draw(silosiDiv);
    });

    container.append(nazivDiv, silosiDiv);
  }
}
