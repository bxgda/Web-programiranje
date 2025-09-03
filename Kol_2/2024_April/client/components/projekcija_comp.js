import { vratiNaslovProjekcije } from "../utils.js";
import { KartaSelect } from "./kartaSelect.js";
import { SalaComp } from "./sala_comp.js";

export class ProjekcijaComponent {
  constructor(projekcija) {
    this.projekcija = projekcija;
  }

  async draw(container) {
    const ceoDiv = document.createElement("div");
    ceoDiv.classList.add("ceoDivProjekcija");

    const naslovProjekcijeDiv = document.createElement("div");
    naslovProjekcijeDiv.classList.add("naslovProjekcijeDiv");

    const naslov = document.createElement("label");
    naslov.innerHTML = await vratiNaslovProjekcije(this.projekcija.id);

    naslovProjekcijeDiv.append(naslov);

    ceoDiv.append(naslovProjekcijeDiv);

    const projekcijaDiv = document.createElement("div");
    projekcijaDiv.classList.add("projekcijaDiv");

    const leviDiv = document.createElement("div");
    leviDiv.classList.add("leviDiv", `leviDiv-${this.projekcija.id}`);

    const desniDiv = document.createElement("div");
    desniDiv.classList.add("desniDiv", `desniDiv-${this.projekcija.id}`);

    // ja za svaku projekciju imam poseban select u kome ima sifra, sto je glupo
    // jer taj select automatski treba za tu porjekciju da kupi kartu
    // ali nije najsrecnije objasnjen zadatak u blanketu
    const kartaSelect = new KartaSelect(this.projekcija.id);
    kartaSelect.draw(leviDiv);

    const salaComp = new SalaComp(this.projekcija.id);
    await salaComp.draw(desniDiv);

    projekcijaDiv.append(leviDiv, desniDiv);
    ceoDiv.append(projekcijaDiv);
    container.append(ceoDiv);
  }
}
