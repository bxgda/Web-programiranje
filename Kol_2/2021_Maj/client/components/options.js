import { promeniPopunjenostSilosa } from "../utils.js";

export class Opcije {
  constructor(listaSilosa, fabrikaID, nazivFabrike) {
    this.listaSilosa = listaSilosa;
    this.fabrikaID = fabrikaID;
    this.nazivFabrike = nazivFabrike;
  }

  draw(container) {
    let lblSilos = document.createElement("label");
    lblSilos.innerHTML = "Silos:";

    let selSilos = document.createElement("select");
    selSilos.classList.add(`selSilos-${this.fabrikaID}`);
    this.listaSilosa.forEach((element) => {
      let optSilos = document.createElement("option");
      optSilos.value = element.oznaka;
      optSilos.innerHTML = element.oznaka;
      selSilos.appendChild(optSilos);
    });

    let lblKolicina = document.createElement("label");
    lblKolicina.innerHTML = "Kolicina:";

    let inputKol = document.createElement("input");
    inputKol.classList.add(`inputKol-${this.fabrikaID}`);
    inputKol.type = "number";

    let dugme = document.createElement("button");
    dugme.innerHTML = "Dodaj u silos";
    dugme.onclick = this.dodajMaterjal;

    container.append(lblSilos, selSilos, lblKolicina, inputKol, dugme);
  }

  dodajMaterjal = async () => {
    let silosIme = document.querySelector(`.selSilos-${this.fabrikaID}`).value;
    let kolicina = document.querySelector(`.inputKol-${this.fabrikaID}`).value;

    const novaKolicina = await promeniPopunjenostSilosa(
      this.nazivFabrike,
      silosIme,
      kolicina
    );

    if (novaKolicina === -1) {
      return;
    }

    // pribavi labelu i popunjeni deo
    let lblPopunjenost = document.querySelector(`.infoPopunjenost-${silosIme}`);
    let popunjeniDeo = document.querySelector(`.popunjeniDeoDiv-${silosIme}`);

    lblPopunjenost.innerHTML = `${novaKolicina}/${
      this.listaSilosa.find((s) => s.oznaka === silosIme).maxKapcitet
    }t`;
    const maxKapcitet = this.listaSilosa.find(
      (s) => s.oznaka === silosIme
    ).maxKapcitet;
    const procenat = (novaKolicina * 100) / maxKapcitet;
    popunjeniDeo.style.height = `${procenat}%`;
  };
}
