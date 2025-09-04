import { IzdavanjeVracanjeComp } from "./izdavanjeVracanje_comp.js";
import { KnjigaComp } from "./knjiga_comp.js";
import { NajcitanijaKnjicaComp } from "./najcitanijaKnjica_comp.js";

export class BibliotekaComp {
  constructor(biblioteka) {
    this.biblioteka = biblioteka;
  }

  async draw(container) {
    // celina za jednu biblioteku (naslov i sve ispod)
    const bibliotekaDiv = document.createElement("div");
    bibliotekaDiv.classList.add("bibliotekaDiv");

    // div za naslov
    const naslovDiv = document.createElement("div");
    naslovDiv.classList.add("naslovBibliotekeDiv");

    const bibliotekaNaslov = document.createElement("h2");
    bibliotekaNaslov.innerHTML = this.biblioteka.ime;

    naslovDiv.append(bibliotekaNaslov);

    // ceo donji deo
    const ceoDiv = document.createElement("div");
    ceoDiv.classList.add("ceoDiv");

    // levi deo i opcije za dodavnje knjige
    const leviDiv = document.createElement("div");
    leviDiv.classList.add("leviDiv", `leviDiv-${this.biblioteka.id}`);

    const knjigaComp = new KnjigaComp(this.biblioteka.id);
    knjigaComp.draw(leviDiv);

    // srednji deo i opcije za izdavanje knjige
    const sredniDiv = document.createElement("div");
    sredniDiv.classList.add("srednjiDiv", `sredniDiv-${this.biblioteka.id}`);

    const izdavanjeVracanjeComp = new IzdavanjeVracanjeComp(this.biblioteka.id);
    izdavanjeVracanjeComp.draw(sredniDiv);

    // desni deo i prikaz najcitanije knjige
    const desniDiv = document.createElement("div");
    desniDiv.classList.add("desniDiv", `desniDiv-${this.biblioteka.id}`);

    const najcitanijaKnjicaComp = new NajcitanijaKnjicaComp(this.biblioteka.id);
    await najcitanijaKnjicaComp.draw(desniDiv);

    ceoDiv.append(leviDiv, sredniDiv, desniDiv);
    bibliotekaDiv.append(naslovDiv, ceoDiv);
    container.append(bibliotekaDiv);
  }
}
