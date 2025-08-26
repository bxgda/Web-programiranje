export class Silos {
  constructor(silosID, oznaka, maxKapacitet, trenutnaKolicina) {
    this.silosID = silosID;
    this.oznaka = oznaka;
    this.maxKapcitet = maxKapacitet;
    this.trenutnaKolicina = trenutnaKolicina;
  }

  draw(container) {
    const ceoStubDiv = document.createElement("div");
    ceoStubDiv.classList.add("ceoStub");

    const nazivSilosa = document.createElement("label");
    nazivSilosa.innerHTML = this.oznaka;

    const infoPopunjenost = document.createElement("label");
    infoPopunjenost.className = `infoPopunjenost-${this.oznaka}`;
    infoPopunjenost.innerHTML = `${this.trenutnaKolicina}/${this.maxKapcitet}t`;

    const ceoSilosDiv = document.createElement("div");
    ceoSilosDiv.classList.add("ceoSilosDiv");

    const popunjeniDeoDiv = document.createElement("div");
    popunjeniDeoDiv.classList.add("popunjeniDeoDiv");
    popunjeniDeoDiv.classList.add(`popunjeniDeoDiv-${this.oznaka}`);
    const procenat = (this.trenutnaKolicina * 100) / this.maxKapcitet;
    popunjeniDeoDiv.style.height = `${procenat}%`;

    ceoSilosDiv.appendChild(popunjeniDeoDiv);

    ceoStubDiv.append(nazivSilosa, infoPopunjenost, ceoSilosDiv);

    container.appendChild(ceoStubDiv);
  }
}
