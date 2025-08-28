export class Prodavnica {
  constructor(naziv, lokacija, telefon, listaProizvoda) {
    this.naziv = naziv;
    this.lokacija = lokacija;
    this.telefon = telefon;
    this.listaProizvoda = listaProizvoda;
  }

  draw(container) {
    const boje = ["green", "red", "blue", "lightgreen"];

    container.innerHTML = "";

    let naslovDiv = document.createElement("div");
    naslovDiv.classList.add("naslovProdDiv");

    const nazivProdavnice = document.createElement("label");
    nazivProdavnice.innerHTML = `Prodavnica: ${this.naziv}`;

    naslovDiv.appendChild(nazivProdavnice);

    let contentProdDiv = document.createElement("div");
    contentProdDiv.classList.add("contentProdDiv");

    this.listaProizvoda.forEach((proizvod, i) => {
      proizvod.draw(contentProdDiv, boje[i], this.naziv);
    });

    container.append(naslovDiv, contentProdDiv);
  }
}
