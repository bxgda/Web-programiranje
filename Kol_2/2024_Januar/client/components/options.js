import { filtrirajAutomobile } from "../utils.js";

export class Options {
  constructor(listaAutomobila) {
    this.listaAutomobila = listaAutomobila;
  }

  draw(container) {
    let gornjiDiv = document.createElement("div");
    gornjiDiv.classList.add("gornjiDiv");

    let donjiDiv = document.createElement("div");
    donjiDiv.classList.add("donjiDiv");

    const polja1 = [
      {
        label: "Ime i prezime:",
        type: "text",
        class: "inputIme",
      },
      {
        label: "JMBG",
        type: "text",
        class: "inputJMBG",
      },
      {
        label: "Broj vozacke dozvole:",
        type: "text",
        class: "inputDozvola",
      },
      {
        label: "Broj dana",
        type: "number",
        class: "inputDani",
      },
    ];

    polja1.forEach((polje) => {
      let lbl = document.createElement("label");
      lbl.innerHTML = polje.label;
      gornjiDiv.append(lbl);

      let input = document.createElement("input");
      input.type = polje.type;
      input.classList.add(polje.class);
      gornjiDiv.append(input);
    });

    let polja2 = [
      {
        label: "Predjena kilometraza:",
        type: "number",
        class: "inputpredjenaKilometraza",
      },
      {
        label: "Godiste:",
        type: "number",
        class: "inputGodiste",
      },
    ];

    polja2.forEach((polje) => {
      let lbl = document.createElement("label");
      lbl.innerHTML = polje.label;
      donjiDiv.append(lbl);

      let input = document.createElement("input");
      input.type = polje.type;
      input.classList.add(polje.class);
      donjiDiv.append(input);
    });

    let lblModel = document.createElement("label");
    lblModel.innerHTML = "Model:";

    let selModel = document.createElement("select");
    selModel.classList.add("selModel");
    this.listaAutomobila.forEach((auto) => {
      let optModel = document.createElement("option");
      optModel.value = auto.model;
      optModel.innerHTML = auto.model;
      selModel.appendChild(optModel);
    });

    let dugme = document.createElement("button");
    dugme.innerHTML = "Filtritraj prikaz";
    dugme.classList.add("dugme");
    dugme.onclick = this.filtriraj;

    donjiDiv.append(lblModel, selModel, dugme);

    container.append(gornjiDiv, donjiDiv);
  }

  filtriraj = async () => {
    let kilometraza = document.querySelector(".inputpredjenaKilometraza").value;
    let godiste = document.querySelector(".inputGodiste").value;
    let model = document.querySelector(".selModel").value;

    var filtriraniAutomobili = await filtrirajAutomobile(
      kilometraza,
      godiste,
      model
    );

    const desnidiv = document.querySelector(".desniDiv");
    desnidiv.innerHTML = "";

    filtriraniAutomobili.forEach((auto) => {
      auto.draw(desnidiv);
    });
  };
}
