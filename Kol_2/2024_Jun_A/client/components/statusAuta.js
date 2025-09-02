import { iznajmiAuto } from "../utils.js";

export class StatusAuta {
  constructor(automobil) {
    this.automobil = automobil;
  }

  draw(container) {
    let automobilDiv = document.createElement("div");
    this.autoDiv = automobilDiv;
    automobilDiv.classList.add("automobilDiv");

    let atributi = [
      {
        label: "Model:",
        text: this.automobil.model,
        class: `model-${this.automobil.automobilID}`,
      },
      {
        label: "Kilometraza:",
        text: this.automobil.predjenaKilometraza,
        class: `kilometraza-${this.automobil.automobilID}`,
      },
      {
        label: "Godiste;",
        text: this.automobil.godiste,
        class: `godiste-${this.automobil.automobilID}`,
      },
      {
        label: "Cena po danu:",
        text: this.automobil.cenaPoDanu,
        class: `cena-${this.automobil.automobilID}`,
      },
      {
        label: `Iznajmljen:`,
        text: this.automobil.iznajmljen,
        class: `iznajmljen-${this.automobil.automobilID}`,
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
    dugme.classList.add(`iznajmi-${this.automobil.automobilID}`);
    dugme.onclick = this.iznajmi;

    if (this.automobil.iznajmljen) {
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
      `.model-${this.automobil.automobilID}`
    ).innerHTML;
    const kilometraza = document.querySelector(
      `.kilometraza-${this.automobil.automobilID}`
    ).innerHTML;
    const godiste = document.querySelector(
      `.godiste-${this.automobil.automobilID}`
    ).innerHTML;
    const cenaPoDanu = document.querySelector(
      `.cena-${this.automobil.automobilID}`
    ).innerHTML;

    console.log({
      ime,
      jmbg,
      brojDozvole,
      brojDana,
      model,
      kilometraza,
      godiste,
      cenaPoDanu,
    });

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

    let iznajmljen = document.querySelector(
      `.iznajmljen-${this.automobil.automobilID}`
    );
    iznajmljen.innerHTML = "true";
    let dugme = document.querySelector(
      `.iznajmi-${this.automobil.automobilID}`
    );
    dugme.disabled = true;

    this.autoDiv.style.backgroundColor = "red";
  };
}
