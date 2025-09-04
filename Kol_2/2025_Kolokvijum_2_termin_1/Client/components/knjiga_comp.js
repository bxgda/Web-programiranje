import { dodajKnjigu } from "../utils.js";

export class KnjigaComp {
  constructor(bibliotekaID) {
    this.bibliotekaID = bibliotekaID;
  }

  draw(container) {
    const naslovDiv = document.createElement("div");
    naslovDiv.classList.add("naslovDiv");

    const naslovLabel = document.createElement("h3");
    naslovLabel.innerHTML = "Knjiga";
    naslovDiv.append(naslovLabel);

    container.append(naslovDiv);

    const poljaDiv = document.createElement("div");
    poljaDiv.classList.add("poljaDiv");

    const polja = [
      {
        label: "Naslov:",
        type: "text",
        class: "inputNaslov",
      },
      {
        label: "Autor:",
        type: "text",
        class: "inputAutor",
      },
      {
        label: "Godina izdanja:",
        type: "number",
        class: "inputGodina",
      },
      {
        label: "izdavac:",
        type: "text",
        class: "inputIzdavac",
      },
      {
        label: "Broj:",
        type: "text",
        class: "inputBroj",
      },
    ];

    polja.forEach((polje) => {
      const label = document.createElement("label");
      label.textContent = polje.label;

      const input = document.createElement("input");
      input.type = polje.type;
      input.classList.add(polje.class);

      poljaDiv.append(label, input);
    });

    const dugme = document.createElement("button");
    dugme.innerHTML = "Dodaj";
    dugme.onclick = this.dodajKnjigu;
    dugme.classList.add("dugmeDodajKnjigu");
    poljaDiv.append(dugme);

    container.append(poljaDiv);
  }

  dodajKnjigu = async () => {
    const odgovarajuciDiv = document.querySelector(
      `.leviDiv-${this.bibliotekaID}`
    );
    const inputNaslov = odgovarajuciDiv.querySelector(".inputNaslov");
    const inputAutor = odgovarajuciDiv.querySelector(".inputAutor");
    const inputGodina = odgovarajuciDiv.querySelector(".inputGodina");
    const inputIzdavac = odgovarajuciDiv.querySelector(".inputIzdavac");
    const inputBroj = odgovarajuciDiv.querySelector(".inputBroj");

    await dodajKnjigu(
      this.bibliotekaID,
      inputNaslov.value,
      inputAutor.value,
      parseInt(inputGodina.value),
      inputIzdavac.value,
      inputBroj.value
    );

    inputNaslov.value = "";
    inputAutor.value = "";
    inputGodina.value = "";
    inputIzdavac.value = "";
    inputBroj.value = "";

    const srednjiDiv = document.querySelector(
      `.sredniDiv-${this.bibliotekaID}`
    );
    const input = srednjiDiv.querySelector(".inputNaziv");
    const select = srednjiDiv.querySelector(".selectRezultat");
    const akcijaDiv = srednjiDiv.querySelector(".akcijaDiv");
    input.value = "";
    select.innerHTML = "";
    const dugme = akcijaDiv.querySelector(".dugmeAkcija");
    dugme.innerHTML = "izdaj/vrati";
    dugme.onclick = null;
  };
}
