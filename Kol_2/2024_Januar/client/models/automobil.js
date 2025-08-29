import { iznajmiAuto } from "../utils.js";

export class Automobil {
  constructor(
    automobilID,
    model,
    predjenaKilometraza,
    godiste,
    cenaPoDanu,
    iznajmljen
  ) {
    this.automobilID = automobilID;
    this.model = model;
    this.predjenaKilometraza = predjenaKilometraza;
    this.godiste = godiste;
    this.cenaPoDanu = cenaPoDanu;
    this.iznajmljen = iznajmljen;
    this.autoDiv;
  }

  draw(container) {
    let automobilDiv = document.createElement("div");
    this.autoDiv = automobilDiv;
    automobilDiv.classList.add("automobilDiv");

    let atributi = [
      {
        label: "Model:",
        text: this.model,
        class: `model-${this.automobilID}`,
      },
      {
        label: "Kilometraza:",
        text: this.predjenaKilometraza,
        class: `kilometraza-${this.automobilID}`,
      },
      {
        label: "Godiste;",
        text: this.godiste,
        class: `godiste-${this.automobilID}`,
      },
      {
        label: "Cena po danu:",
        text: this.cenaPoDanu,
        class: `cena-${this.automobilID}`,
      },
      {
        label: `Iznajmljen:`,
        text: this.iznajmljen,
        class: `iznajmljen-${this.automobilID}`,
      },
    ];

    atributi.forEach((atr) => {
      let lbl1 = document.createElement("label");
      lbl1.innerHTML = atr.label;
      automobilDiv.append(lbl1);

      let lbl2 = document.createElement("label");
      lbl2.innerHTML = atr.text;
      lbl2.classList.add(atr.class);
      automobilDiv.append(lbl2);
    });

    let dugme = document.createElement("button");
    dugme.innerHTML = "Iznajmi";
    dugme.classList.add("dugme");
    dugme.classList.add(`iznajmi-${this.automobilID}`);
    dugme.onclick = this.iznajmi;

    if (this.iznajmljen) {
      dugme.disabled = true;
      automobilDiv.style.backgroundColor = "red";
    } else {
      automobilDiv.style.backgroundColor = "lightgreen";
    }

    automobilDiv.append(dugme);
    container.append(automobilDiv);
  }

  iznajmi = async () => {
    const ime = document.querySelector(".inputIme").value;
    const jmbg = document.querySelector(".inputJMBG").value;
    const brojDozvole = document.querySelector(".inputDozvola").value;
    const brojDana = document.querySelector(".inputDani").value;

    // ovo je strasno kako je back napisan... umesto da nadje auto po ID-u on trazi po svim ovde dole atributima
    const model = document.querySelector(
      `.model-${this.automobilID}`
    ).innerHTML;
    const kilometraza = document.querySelector(
      `.kilometraza-${this.automobilID}`
    ).innerHTML;
    const godiste = document.querySelector(
      `.godiste-${this.automobilID}`
    ).innerHTML;
    const cenaPoDanu = document.querySelector(
      `.cena-${this.automobilID}`
    ).innerHTML;

    const odgovor = await iznajmiAuto(
      ime,
      jmbg,
      brojDozvole,
      brojDana,
      model,
      kilometraza,
      godiste,
      cenaPoDanu
    );

    if (!odgovor) return;

    let iznajmljen = document.querySelector(`.iznajmljen-${this.automobilID}`);
    iznajmljen.innerHTML = "true";
    let dugme = document.querySelector(`.iznajmi-${this.automobilID}`);
    dugme.disabled = true;

    this.autoDiv.style.backgroundColor = "red";
  };
}
