import { kupiKartu } from "../utils.js";

export class KartaSelect {
  constructor(projekcijaId) {
    this.projekcijaId = projekcijaId;
  }

  draw(container) {
    const naslovDiv = document.createElement("div");
    naslovDiv.classList.add("naslovDiv");

    const lblNaslov = document.createElement("label");
    lblNaslov.innerHTML = "Kupi kartu";

    naslovDiv.append(lblNaslov);

    const polja = [
      {
        labela: "Red:",
        class: "inputRed",
      },
      {
        labela: "Broj sedista:",
        class: "inputSediste",
      },
      {
        labela: "Cena karte:",
        class: "inputCena",
      },
      {
        labela: "Šifra:",
        class: "inputSifra",
      },
    ];

    const inputDiv = document.createElement("div");
    inputDiv.classList.add("inputDiv");

    polja.forEach((polje) => {
      const lbl = document.createElement("label");
      lbl.innerHTML = polje.labela;

      const input = document.createElement("input");
      input.type = "number";
      input.classList.add(polje.class);

      inputDiv.append(lbl, input);
    });

    const dugmeDiv = document.createElement("div");
    dugmeDiv.classList.add("dugmeDiv");

    const dugme = document.createElement("button");
    dugme.innerHTML = "Kupi kartu";
    dugme.onclick = this.kupiKartu;

    dugmeDiv.append(dugme);

    container.append(naslovDiv, inputDiv, dugmeDiv);
  }

  kupiKartu = async () => {
    const odgovarajuciDiv = document.querySelector(
      `.leviDiv-${this.projekcijaId}`
    );

    const red = odgovarajuciDiv.querySelector(".inputRed");
    const brojSedista = odgovarajuciDiv.querySelector(".inputSediste");
    const cena = odgovarajuciDiv.querySelector(".inputCena");
    const sifra = odgovarajuciDiv.querySelector(".inputSifra");

    // u tekstu ima nesto za cenu da se smanjuje po redovima ali ne pise da li to resava back ili front
    const odgovor = await kupiKartu(
      red.value,
      brojSedista.value,
      cena.value,
      sifra.value
    );

    if (!odgovor) return;

    const sedisteDiv = document.querySelector(
      `.sediste-${red.value}-${brojSedista.value}`
    );
    sedisteDiv.style.backgroundColor = "red";
  };
}
