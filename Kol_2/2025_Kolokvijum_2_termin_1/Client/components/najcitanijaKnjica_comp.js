import { najcitanijaKnjiga } from "../utils.js";

export class NajcitanijaKnjicaComp {
  constructor(bibliotekaID) {
    this.bibliotekaID = bibliotekaID;
  }

  async draw(container) {
    // naslov
    const naslovNajcitanijaDiv = document.createElement("div");
    naslovNajcitanijaDiv.classList.add("naslovNajcitanijaDiv");
    const naslovLabel = document.createElement("h3");
    naslovLabel.innerHTML = "Najčitanija knjiga";
    naslovNajcitanijaDiv.append(naslovLabel);

    // sam tekst za najcitaniju knjigu
    const najcitanijaDiv = document.createElement("div");
    najcitanijaDiv.classList.add("najcitanijaDiv");
    const najcitanijaLabel = document.createElement("h4");
    najcitanijaLabel.innerHTML = await najcitanijaKnjiga(this.bibliotekaID);
    najcitanijaDiv.append(najcitanijaLabel);

    container.append(naslovNajcitanijaDiv, najcitanijaDiv);
  }
}
