import { prodajProizvod } from "../utils.js";

export class Proizvod {
  constructor(proizvodID, naziv, kategorija, cena, dostupnaKolicina) {
    this.proizvodID = proizvodID;
    this.naziv = naziv;
    this.kategorija = kategorija;
    this.cena = cena;
    this.dostupnaKolicina = dostupnaKolicina;
    this.nazivProdavnice;
  }

  draw(container, boja, nazivProdavnice) {
    this.nazivProdavnice = nazivProdavnice;

    const ceoRedDiv = document.createElement("div");
    ceoRedDiv.classList.add("ceoRedDiv");

    const proizvodDiv = document.createElement("div");
    proizvodDiv.classList.add("proizvodDiv");

    const proizvodInfo = document.createElement("label");
    proizvodInfo.classList.add(`proizvodInfo-${this.naziv}`);
    proizvodInfo.innerHTML = `${this.naziv}: ${this.dostupnaKolicina}`;

    const ceoProizvodDiv = document.createElement("div");
    ceoProizvodDiv.classList.add("ceoProizvodDiv");

    const popunjeniDeoDiv = document.createElement("div");
    popunjeniDeoDiv.classList.add("popunjeniDeoDiv");
    popunjeniDeoDiv.classList.add(`popunjeniDeoDiv-${this.naziv}`);
    popunjeniDeoDiv.style.width = `${this.dostupnaKolicina}%`;
    popunjeniDeoDiv.style.backgroundColor = `${boja}`;

    const optProizvodDiv = document.createElement("div");
    optProizvodDiv.classList.add("optProizvodDiv");

    const lblKol = document.createElement("label");
    lblKol.innerHTML = "Količina:";

    let inputKol = document.createElement("input");
    inputKol.classList.add(`inputKol-${this.naziv}`);
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
    let inputKol = document.querySelector(`.inputKol-${this.naziv}`);
    let kolicina = inputKol.value;

    const novaKolicina = await prodajProizvod(
      this.nazivProdavnice,
      this.naziv,
      kolicina
    );

    if (novaKolicina === -1) return;

    this.dostupnaKolicina = novaKolicina;

    let proizvodInfo = document.querySelector(`.proizvodInfo-${this.naziv}`);
    proizvodInfo.innerHTML = `${this.naziv}: ${novaKolicina}`;

    let popunjeniDeoDiv = document.querySelector(
      `.popunjeniDeoDiv-${this.naziv}`
    );
    popunjeniDeoDiv.style.width = `${this.dostupnaKolicina}%`;

    inputKol.value = "";
  };
}
