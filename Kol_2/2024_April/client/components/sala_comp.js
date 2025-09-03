import { Sediste } from "../models/sediste.js";
import { vratiBrojRedova, vratiSedistaReda, zauzetaSedista } from "../utils.js";

export class SalaComp {
  constructor(projekcijaID) {
    this.projekcijaID = projekcijaID;
    this.brojRedova;
    this.brojSedistaURedu;
    this.sedista = [];
  }

  async popuniPodatke(projekcijaID) {
    this.brojRedova = await vratiBrojRedova(projekcijaID);
    this.brojSedistaURedu = await vratiSedistaReda(projekcijaID);
  }

  async draw(container) {
    await this.popuniPodatke(this.projekcijaID);
    this.sedista = await zauzetaSedista(this.projekcijaID);

    // pravimo petlje za redove i broj sedista i ako postoji vec sediste (dodatno u liniji iznad kao zauzeto) onda nista
    // a ako ne postoji pravimo novo i kazemo da je slobodno... ovo sve radimo da bi imali lepo listu svih sedista
    for (let red = 1; red <= this.brojRedova; red++) {
      for (let sediste = 1; sediste <= this.brojSedistaURedu; sediste++) {
        const postoji = this.sedista.some(
          (s) => s.red === red && s.brojSedista === sediste
        );
        if (!postoji) {
          this.sedista.push(new Sediste(red, sediste, false));
        }
      }
    }

    // prolazimo kroz sva sedista i crtamo
    for (let red = 1; red <= this.brojRedova; red++) {
      const redDiv = document.createElement("div");
      redDiv.classList.add("redDiv");

      for (let brojS = 1; brojS <= this.brojSedistaURedu; brojS++) {
        const sedisteObjekat = this.sedista.find(
          (s) =>
            s.red === red && (s.brojSedista === brojS || s.sediste === brojS)
        );

        const sedisteDiv = document.createElement("div");
        sedisteDiv.classList.add(
          "sedisteDiv",
          `sediste-${sedisteObjekat.red}-${sedisteObjekat.brojSedista}`
        );

        const sedisteLabel = document.createElement("label");
        sedisteLabel.innerHTML = `Red: ${sedisteObjekat.red}; Broj: ${sedisteObjekat.brojSedista}`;

        sedisteDiv.append(sedisteLabel);

        if (sedisteObjekat.zauzeto) sedisteDiv.style.backgroundColor = "red";
        else sedisteDiv.style.backgroundColor = "green";

        redDiv.append(sedisteDiv);
      }
      container.append(redDiv);
    }
  }
}
