import { prodajProizvod } from "../utils.js";

export class ProizvodComponent {
  constructor(proizvod, nazivProdavnice) {
    this.proizvod = proizvod;
    this.nazivProdavnice = nazivProdavnice;
  }

  draw(container, boja) {
    const ceoRedDiv = document.createElement("div");
    ceoRedDiv.classList.add("ceoRedDiv");

    const proizvodDiv = document.createElement("div");
    proizvodDiv.classList.add("proizvodDiv");

    const proizvodInfo = document.createElement("label");
    proizvodInfo.classList.add(`proizvodInfo-${this.proizvod.naziv}`);
    proizvodInfo.innerHTML = `${this.proizvod.naziv}: ${this.proizvod.dostupnaKolicina}`;

    const ceoProizvodDiv = document.createElement("div");
    ceoProizvodDiv.classList.add("ceoProizvodDiv");

    const popunjeniDeoDiv = document.createElement("div");
    popunjeniDeoDiv.classList.add("popunjeniDeoDiv");
    popunjeniDeoDiv.classList.add(`popunjeniDeoDiv-${this.proizvod.naziv}`);
    popunjeniDeoDiv.style.width = `${this.proizvod.dostupnaKolicina}%`;
    popunjeniDeoDiv.style.backgroundColor = `${boja}`;

    const optProizvodDiv = document.createElement("div");
    optProizvodDiv.classList.add("optProizvodDiv");

    const lblKol = document.createElement("label");
    lblKol.innerHTML = "Količina:";

    let inputKol = document.createElement("input");
    inputKol.classList.add(`inputKol-${this.proizvod.naziv}`);
    inputKol.classList.add("inputKol");
    inputKol.type = "number";

    let dugme = document.createElement("button");
    dugme.classList.add("dugmeOpt");
    dugme.innerHTML = "Prodaj";
    dugme.onclick = this.prodaj;

    optProizvodDiv.append(lblKol, inputKol, dugme);
    ceoProizvodDiv.append(popunjeniDeoDiv);
    proizvodDiv.append(proizvodInfo, ceoProizvodDiv);
    ceoRedDiv.append(proizvodDiv, optProizvodDiv);
    container.append(ceoRedDiv);
  }

  prodaj = async () => {
    let inputKol = document.querySelector(`.inputKol-${this.proizvod.naziv}`);
    let kolicina = inputKol.value;

    const novaKolicina = await prodajProizvod(
      this.nazivProdavnice,
      this.proizvod.naziv,
      kolicina
    );

    if (novaKolicina === -1) return;

    this.proizvod.dostupnaKolicina = novaKolicina;

    let proizvodInfo = document.querySelector(
      `.proizvodInfo-${this.proizvod.naziv}`
    );
    proizvodInfo.innerHTML = `${this.proizvod.naziv}: ${novaKolicina}`;

    let popunjeniDeoDiv = document.querySelector(
      `.popunjeniDeoDiv-${this.proizvod.naziv}`
    );
    popunjeniDeoDiv.style.width = `${this.proizvod.dostupnaKolicina}%`;

    inputKol.value = "";
  };
}
